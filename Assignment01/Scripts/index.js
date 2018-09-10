
// HTML Layout
var Header;
var Footer;
var Main;

// GL Canvas
var LeftCanvas;
var RightCanvas;

// GL Context
var GLLEFT;
var GLRIGHT;

// Shader Source
var VSHADER_SOURCE;
var FSHADER_SOURCE;

// Vertice Count
var Leftcount;
var RightCount;

function initialize()
{

    // Initial Shader Programs
     VSHADER_SOURCE =
    'attribute vec4 a_Position;\n' +
    'attribute vec4 a_Color;\n' +
    'attribute vec4 a_Normal;\n' +
    'uniform mat4 u_MvpMatrix;\n' +
    'uniform mat4 u_ModelMatrix;\n' +   // Model matrix
    'uniform mat4 u_NormalMatrix;\n' +  // Transformation matrix of the normal
    'uniform vec3 u_LightColor;\n' +    // Light color
    'uniform vec3 u_LightPosition;\n' + // Position of the light source (in the world coordinate system)
    'uniform vec3 u_AmbientLight;\n' +  // Ambient light color
    'varying vec4 v_Color;\n' +
    'void main() {\n' +
    '  gl_Position = u_MvpMatrix * a_Position;\n' +
       // Recalculate the normal based on the model matrix and make its length 1.
    '  vec3 normal = normalize(vec3(u_NormalMatrix * a_Normal));\n' +
       // Calculate world coordinate of vertex
    '  vec4 vertexPosition = u_ModelMatrix * a_Position;\n' +
       // Calculate the light direction and make it 1.0 in length
    '  vec3 lightDirection = normalize(u_LightPosition - vec3(vertexPosition));\n' +
       // The dot product of the light direction and the normal
    '  float nDotL = max(dot(lightDirection, normal), 0.0);\n' +
       // Calculate the color due to diffuse reflection
    '  vec3 diffuse = u_LightColor * a_Color.rgb * nDotL;\n' +
       // Calculate the color due to ambient reflection
    '  vec3 ambient = u_AmbientLight * a_Color.rgb;\n' +
       //  Add the surface colors due to diffuse reflection and ambient reflection
    '  v_Color = vec4(diffuse + ambient, a_Color.a);\n' + 
    '}\n';
  
    // Fragment shader program
     FSHADER_SOURCE =
    '#ifdef GL_ES\n' +
    'precision mediump float;\n' +
    '#endif\n' +
    'varying vec4 v_Color;\n' +
    'void main() {\n' +
    '  gl_FragColor = v_Color;\n' +
    '}\n';

    Angle = 0;
    Distance = 100;
    GoingBack = false;

    MaterializeElements();
    //
    InitializeContext();
    //
    Main2();

}

function InitializeContext()
{

    GLLEFT = getWebGLContext(LeftCanvas);
    GLRIGHT = getWebGLContext(RightCanvas);
    if (!GLLEFT && !GLRIGHT) 
    {
        
        if(!GLLEFT)
        {

            console.log("LEFT GLCONTEXT FAILED");

        }
        if(!GLRIGHT)
        {

             console.log("RIGHT GLCONTEXT FAILED");

        }

        return;
    }

    // Initialize Shaders
    if (!initShaders(GLLEFT, VSHADER_SOURCE, FSHADER_SOURCE)) 
    {
        console.log('Left Failed to intialize shaders.');
        return;
    }

    if (!initShaders(GLRIGHT, VSHADER_SOURCE, FSHADER_SOURCE)) 
    {
        console.log('Right Failed to intialize shaders.');
        return;
    }

    Leftcount = initVertexBuffers(GLLEFT);
    if (Leftcount < 0) {
      console.log('Failed to set the vertex information');
      return;
    }

    initStuff(GLLEFT, LeftCanvas);

    RightCount = initVertexBuffers(GLRIGHT);
    if (RightCount < 0) {
      console.log('Failed to set the vertex information');
      return;
    }

    initStuff(GLRIGHT, RightCanvas);

}

function initStuff(gl, canvas)
{

    // Set the clear color and enable the depth test
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.enable(gl.DEPTH_TEST);
  
    // Get the storage locations of uniform variables and so on
    var u_ModelMatrix = gl.getUniformLocation(gl.program, 'u_ModelMatrix');
    var u_MvpMatrix = gl.getUniformLocation(gl.program, 'u_MvpMatrix');
    var u_NormalMatrix = gl.getUniformLocation(gl.program, 'u_NormalMatrix');
    var u_LightColor = gl.getUniformLocation(gl.program, 'u_LightColor');
    var u_LightPosition = gl.getUniformLocation(gl.program, 'u_LightPosition');
    var u_AmbientLight = gl.getUniformLocation(gl.program, 'u_AmbientLight');
    if (!u_MvpMatrix || !u_NormalMatrix || !u_LightColor || !u_LightPosition || !u_AmbientLight) { 
      console.log('Failed to get the storage location');
      return;
    }
  
    // Set the light color (white)
    gl.uniform3f(u_LightColor, 1.0, 1.0, 1.0);
    // Set the light direction (in the world coordinate)
    gl.uniform3f(u_LightPosition, 2.3, 4.0, 3.5);
    // Set the ambient light
    gl.uniform3f(u_AmbientLight, 0.2, 0.2, 0.2);
  
    var modelMatrix = new Matrix4();  // Model matrix
    var mvpMatrix = new Matrix4();    // Model view projection matrix
    var normalMatrix = new Matrix4(); // Transformation matrix for normals
  
    // Calculate the model matrix
    modelMatrix.setRotate(Angle, 0, 1, 0); // Rotate around the y-axis
    // Pass the model matrix to u_ModelMatrix
    gl.uniformMatrix4fv(u_ModelMatrix, false, modelMatrix.elements);
  
    
    // Pass the model view projection matrix to u_MvpMatrix
    mvpMatrix.setPerspective(Distance, canvas.width/canvas.height, 1, 100);
    mvpMatrix.lookAt(6, 6, 14, 0, 0, 0, 0, 1, 0);
    mvpMatrix.multiply(modelMatrix);
    gl.uniformMatrix4fv(u_MvpMatrix, false, mvpMatrix.elements);
  
    // Pass the matrix to transform the normal based on the model matrix to u_NormalMatrix
    normalMatrix.setInverseOf(modelMatrix);
    normalMatrix.transpose();
    gl.uniformMatrix4fv(u_NormalMatrix, false, normalMatrix.elements);

}

function initVertexBuffers(gl) 
{

    var vertices = new Float32Array([
       2.0, 2.0, 2.0,  -2.0, 2.0, 2.0,  -2.0,-2.0, 2.0,   2.0,-2.0, 2.0, // v0-v1-v2-v3 front
       2.0, 2.0, 2.0,   2.0,-2.0, 2.0,   2.0,-2.0,-2.0,   2.0, 2.0,-2.0, // v0-v3-v4-v5 right
       2.0, 2.0, 2.0,   2.0, 2.0,-2.0,  -2.0, 2.0,-2.0,  -2.0, 2.0, 2.0, // v0-v5-v6-v1 up
      -2.0, 2.0, 2.0,  -2.0, 2.0,-2.0,  -2.0,-2.0,-2.0,  -2.0,-2.0, 2.0, // v1-v6-v7-v2 left
      -2.0,-2.0,-2.0,   2.0,-2.0,-2.0,   2.0,-2.0, 2.0,  -2.0,-2.0, 2.0, // v7-v4-v3-v2 down
       2.0,-2.0,-2.0,  -2.0,-2.0,-2.0,  -2.0, 2.0,-2.0,   2.0, 2.0,-2.0  // v4-v7-v6-v5 back
    ]);
  
    

    // Colors
    var colors = new Float32Array([
      0, 1, 0,   1, 0, 1,   0, 0, 1,  1, 1, 1,     // v0-v1-v2-v3 front
      0, 1, 0,   1, 0, 1,   0, 0, 1,  1, 1, 1,     // v0-v3-v4-v5 right
      0, 1, 0,   1, 0, 1,   0, 0, 1,  1, 1, 1,     // v0-v5-v6-v1 up
      0, 1, 0,   1, 0, 1,   0, 0, 1,  1, 1, 1,     // v1-v6-v7-v2 left
      0, 1, 0,   1, 0, 1,   0, 0, 1,  1, 1, 1,     // v7-v4-v3-v2 down
      0, 1, 0,   1, 0, 1,   0, 0, 1,  1, 1, 1    // v4-v7-v6-v5 back
   ]);
  
    // Normal
    var normals = new Float32Array([
      0.0, 0.0, 1.0,   0.0, 0.0, 1.0,   0.0, 0.0, 1.0,   0.0, 0.0, 1.0,  // v0-v1-v2-v3 front
      1.0, 0.0, 0.0,   1.0, 0.0, 0.0,   1.0, 0.0, 0.0,   1.0, 0.0, 0.0,  // v0-v3-v4-v5 right
      0.0, 1.0, 0.0,   0.0, 1.0, 0.0,   0.0, 1.0, 0.0,   0.0, 1.0, 0.0,  // v0-v5-v6-v1 up
     -1.0, 0.0, 0.0,  -1.0, 0.0, 0.0,  -1.0, 0.0, 0.0,  -1.0, 0.0, 0.0,  // v1-v6-v7-v2 left
      0.0,-1.0, 0.0,   0.0,-1.0, 0.0,   0.0,-1.0, 0.0,   0.0,-1.0, 0.0,  // v7-v4-v3-v2 down
      0.0, 0.0,-1.0,   0.0, 0.0,-1.0,   0.0, 0.0,-1.0,   0.0, 0.0,-1.0   // v4-v7-v6-v5 back
    ]);
  
    // Indices of the vertices
    var indices = new Uint8Array([
       0, 1, 2,   0, 2, 3,    // front
       4, 5, 6,   4, 6, 7,    // right
       8, 9,10,   8,10,11,    // up
      12,13,14,  12,14,15,    // left
      16,17,18,  16,18,19,    // down
      20,21,22,  20,22,23     // back
   ]);
  
    // Write the vertex property to buffers (coordinates, colors and normals)
    if (!initArrayBuffer(gl, 'a_Position', vertices, 3, gl.FLOAT)) return -1;
    if (!initArrayBuffer(gl, 'a_Color', colors, 3, gl.FLOAT)) return -1;
    if (!initArrayBuffer(gl, 'a_Normal', normals, 3, gl.FLOAT)) return -1;
  
    // Unbind the buffer object
    gl.bindBuffer(gl.ARRAY_BUFFER, null);
  
    // Write the indices to the buffer object
    var indexBuffer = gl.createBuffer();
    if (!indexBuffer) {
      console.log('Failed to create the buffer object');
      return false;
    }
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, indices, gl.STATIC_DRAW);
  
    return indices.length;
}

function initArrayBuffer(gl, attribute, data, num, type) 
{
    // Create a buffer object
    var buffer = gl.createBuffer();
    if (!buffer) {
      console.log('Failed to create the buffer object');
      return false;
    }
    // Write date into the buffer object
    gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
    gl.bufferData(gl.ARRAY_BUFFER, data, gl.STATIC_DRAW);
    // Assign the buffer object to the attribute variable
    var a_attribute = gl.getAttribLocation(gl.program, attribute);
    if (a_attribute < 0) {
      console.log('Failed to get the storage location of ' + attribute);
      return false;
    }
    gl.vertexAttribPointer(a_attribute, num, type, false, 0, 0);
    // Enable the assignment of the buffer object to the attribute variable
    gl.enableVertexAttribArray(a_attribute);
  
    return true;
}

function MaterializeElements()
{

    // Keep Elements Local
    var DivCenter;
    var LeftCanvasDiv;
    var RightCanvasDiv;

    // Create Elements
    DivCenter = document.createElement('div');
    LeftCanvasDiv = document.createElement('div');
    RightCanvasDiv = document.createElement('div');
    LeftCanvas = document.createElement('canvas');
    RightCanvas = document.createElement('canvas');

    //
    Header = document.createElement('Header');
    Footer = document.createElement('Footer');
    Main = document.createElement('Main');


    // Configure Elements

    // Main
    Main.id = "Main";
    Main.style.height = "800px";
    Main.style.width = window.innerWidth.toString() + "px";
    Main.style.backgroundColor = "white";

    // Headr
    Header.id = "Header";
    Header.style.height = "100px";
    Header.style.width = window.innerWidth.toString() + "px";
    Header.style.backgroundColor = "white";

    // Footer
    Footer.id = "Main";
    Footer.style.height = "200px";
    Footer.style.width = window.innerWidth.toString() + "px";
    Footer.style.backgroundColor = "white";


    // Setup Containers for Canvas
    // Main Container
    DivCenter.id = "MainDiv";
    DivCenter.style.height = "700px";
    DivCenter.style.width = window.innerWidth.toString() + "px";
    DivCenter.style.backgroundColor = "black";

    // Left and Right Containers
    // Left
    LeftCanvasDiv.id = "LeftDiv";
    LeftCanvasDiv.style.position = "absolute";
    LeftCanvasDiv.style.left = "15%";
    LeftCanvasDiv.style.right = "62.5%";
    LeftCanvasDiv.style.top = "25%";
    LeftCanvasDiv.style.height = "400px";
    LeftCanvasDiv.style.width = "400px";
    LeftCanvasDiv.style.backgroundColor = "white";
    LeftCanvasDiv.style.border = "5px solid";

    // Right
    RightCanvasDiv.id = "RightDiv";
    RightCanvasDiv.style.position = "absolute";
    RightCanvasDiv.style.left = "62.5%";
    RightCanvasDiv.style.right = "20%";
    RightCanvasDiv.style.top = "25%";
    RightCanvasDiv.style.margin = "auto";
    RightCanvasDiv.style.height = "400px";
    RightCanvasDiv.style.width = "400px";
    RightCanvasDiv.style.backgroundColor = "white";
    RightCanvasDiv.style.border = "5px solid";

    // 
    document.body.appendChild(Header);
    document.body.appendChild(Main);
    document.body.appendChild(Footer);

    document.getElementById("Main").appendChild(DivCenter);
    document.getElementById("MainDiv").appendChild(LeftCanvasDiv);
    document.getElementById("MainDiv").appendChild(RightCanvasDiv);

    // Setup Canvases
    // LeftCanvas
	LeftCanvas.id = 'LeftCanvas';
	LeftCanvas.style.width  = "400px";
	LeftCanvas.style.height = "400px";

    // LeftCanvas
	RightCanvas.id = 'RightCanvas';
	RightCanvas.style.width  = "400px";
	RightCanvas.style.height = "400px";

    document.getElementById("LeftDiv").appendChild(LeftCanvas);
    document.getElementById("RightDiv").appendChild(RightCanvas);

}

var Angle;
var Distance;
var GoingBack;

function Main2()
{

    Angle++;
    if (GoingBack == false)
    {

        Distance--;
        if (Distance <= 30)
        {

            GoingBack = true;

        }

    }
    else
    {

        Distance++;
        if (Distance >= 100)
        {

            GoingBack = false;

        }


    }

    Update();
    InitializeContext();
    requestAnimationFrame(Main2);


}

function Update()
{

    Draw(GLLEFT, Leftcount);
    Draw(GLRIGHT, RightCount);

}

function Draw(glContext, n)
{

    // Specify the color for clearing <canvas>
    glContext.clearColor(0, 0, 0, 1);

    // Clear <canvas>
    // Clear color and depth buffer
    glContext.clear(glContext.COLOR_BUFFER_BIT | glContext.DEPTH_BUFFER_BIT);
  
    // Draw the cube
    glContext.drawElements(glContext.TRIANGLES, n, glContext.UNSIGNED_BYTE, 0);

}