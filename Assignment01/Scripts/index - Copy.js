
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


function initialize()
{

    // Initial Shader Programs
    VSHADER_SOURCE =
    'attribute vec4 a_Position;\n' +
    'void main() {\n' +
    '  gl_Position = a_Position;\n' +
    '}\n';

    FSHADER_SOURCE =
    'void main() {\n' +
    '  gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0);\n' +
    '}\n';

    MaterializeElements();
    //
    InitializeContext();
    //
    initVertexBuffers(GLLEFT, LeftCanvas);
    initVertexBuffers(GLRIGHT, RightCanvas);
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

}

function initVertexBuffers(glContext, canvas) {
  var vertices = new Float32Array([
    0, 0.5, 
    -0.5, -0.5,  
    0.5, -0.5
  ]);
  var n = 3; // The number of vertices

  // Create a buffer object
  var vertexBuffer = glContext.createBuffer();
  if (!vertexBuffer) {
    console.log('Failed to create the buffer object');
    return -1;
  }

  // Bind the buffer object to target
  glContext.bindBuffer(glContext.ARRAY_BUFFER, vertexBuffer);
  // Write date into the buffer object
  glContext.bufferData(glContext.ARRAY_BUFFER, vertices, glContext.STATIC_DRAW);

  var a_Position = glContext.getAttribLocation(glContext.program, 'a_Position');
  if (a_Position < 0) {
    console.log('Failed to get the storage location of a_Position');
    return -1;
  }

  // Assign the buffer object to a_Position variable
  glContext.vertexAttribPointer(a_Position, 2, glContext.FLOAT, false, 0, 0);

  // Enable the assignment to a_Position variable
  glContext.enableVertexAttribArray(a_Position);

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
    Main.style.backgroundColor = "blue";

    // Heade
    Header.id = "Header";
    Header.style.height = "100px";
    Header.style.width = window.innerWidth.toString() + "px";
    Header.style.backgroundColor = "blue";

    // Footer
    Footer.id = "Main";
    Footer.style.height = "200px";
    Footer.style.width = window.innerWidth.toString() + "px";
    Footer.style.backgroundColor = "blue";


    // Setup Containers for Canvas
    // Main Container
    DivCenter.id = "MainDiv";
    DivCenter.style.height = "700px";
    DivCenter.style.width = window.innerWidth.toString() + "px";
    DivCenter.style.backgroundColor = "grey";

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

function Main2()
{

    Update();
    requestAnimationFrame(Main2);


}

function Update()
{


    Draw(GLLEFT);
    Draw(GLRIGHT);

}

function Draw(glContext)
{

    // Specify the color for clearing <canvas>
    glContext.clearColor(0, 0, 0, 1);

    // Clear <canvas>
    glContext.clear(glContext.COLOR_BUFFER_BIT);

    // Draw the rectangle
    glContext.drawArrays(glContext.TRIANGLES, 0, 3);

}