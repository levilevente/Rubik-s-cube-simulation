using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace L3_dlim2227__3
{
    internal static class Program
    {
        private static CameraDescriptor cameraDescriptor = new();

        private static CubeArrangementModel cubeArrangementModel = new();

        private static IWindow window;

        private static GL Gl;

        private static uint program;
        
        private static float rotationSpeed = MathF.PI / 2.0f;
        
        private static GlCube[] cubes = new GlCube[27];
        
        private const string ModelMatrixVariableName = "uModel";
        private const string ViewMatrixVariableName = "uView";
        private const string ProjectionMatrixVariableName = "uProjection";

        private static readonly string VertexShaderSource = @"
        #version 330 core
        layout (location = 0) in vec3 vPos;
		layout (location = 1) in vec4 vCol;

        uniform mat4 uModel;
        uniform mat4 uView;
        uniform mat4 uProjection;

		out vec4 outCol;
        
        void main()
        {
			outCol = vCol;
            gl_Position = uProjection*uView*uModel*vec4(vPos.x, vPos.y, vPos.z, 1.0);
        }
        ";


        private static readonly string FragmentShaderSource = @"
        #version 330 core
        out vec4 FragColor;

		in vec4 outCol;

        void main()
        {
            FragColor = outCol;
        }
        ";

        static void Main(string[] args)
        {
            WindowOptions windowOptions = WindowOptions.Default;
            windowOptions.Title = "Lab2-3";
            windowOptions.Size = new Vector2D<int>(500, 500);

            // on some systems there is no depth buffer by default, so we need to make sure one is created
            windowOptions.PreferredDepthBufferBits = 24;

            window = Window.Create(windowOptions);

            window.Load += Window_Load;
            window.Update += Window_Update;
            window.Render += Window_Render;
            window.Closing += Window_Closing;

            window.Run();
        }

        private static void Window_Load()
        {
            //Console.WriteLine("Load");

            // set up input handling
            IInputContext inputContext = window.CreateInput();
            foreach (var keyboard in inputContext.Keyboards)
            {
                keyboard.KeyDown += Keyboard_KeyDown;
            }

            Gl = window.CreateOpenGL();
            Gl.ClearColor(System.Drawing.Color.White);

            SetUpObjects();

            LinkProgram();
            
            cubeArrangementModel.SetCubes(cubes);

            Gl.Enable(EnableCap.CullFace);

            Gl.Enable(EnableCap.DepthTest);
            Gl.DepthFunc(DepthFunction.Lequal);
        }

        private static void LinkProgram()
        {
            uint vshader = Gl.CreateShader(ShaderType.VertexShader);
            uint fshader = Gl.CreateShader(ShaderType.FragmentShader);

            Gl.ShaderSource(vshader, VertexShaderSource);
            Gl.CompileShader(vshader);
            Gl.GetShader(vshader, ShaderParameterName.CompileStatus, out int vStatus);
            if (vStatus != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + Gl.GetShaderInfoLog(vshader));

            Gl.ShaderSource(fshader, FragmentShaderSource);
            Gl.CompileShader(fshader);

            program = Gl.CreateProgram();
            Gl.AttachShader(program, vshader);
            Gl.AttachShader(program, fshader);
            Gl.LinkProgram(program);
            Gl.GetProgram(program, GLEnum.LinkStatus, out var status);
            if (status == 0)
            {
                Console.WriteLine($"Error linking shader {Gl.GetProgramInfoLog(program)}");
            }
            Gl.DetachShader(program, vshader);
            Gl.DetachShader(program, fshader);
            Gl.DeleteShader(vshader);
            Gl.DeleteShader(fshader);
        }

        private static async void Keyboard_KeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            switch (key)
            {
                case Key.Left:
                    cameraDescriptor.RotateLeft();
                    break;
                case Key.Right:
                    cameraDescriptor.RotateRight();
                    break;
                case Key.Up:
                    cameraDescriptor.RotateUp();
                    break;
                case Key.Down:
                    cameraDescriptor.RotateDown();
                    break;
                case Key.W:
                    cameraDescriptor.MoveForward();
                    break;
                case Key.A:
                    cameraDescriptor.MoveLeft();
                    break;
                case Key.S:
                    cameraDescriptor.MoveBackward();
                    break;
                case Key.D:
                    cameraDescriptor.MoveRight();
                    break;
                case Key.Tab:
                    cubeArrangementModel.AnimationEnabeld = !cubeArrangementModel.AnimationEnabeld;
                    break;
                case Key.Z:
                    cubeArrangementModel.RotateZ1Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.X:
                    cubeArrangementModel.RotateZ2Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.C:
                    cubeArrangementModel.RotateZ3Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.F:
                    cubeArrangementModel.RotateZ1Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.G:
                    cubeArrangementModel.RotateZ2Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.H:
                    cubeArrangementModel.RotateZ3Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.U:
                    cubeArrangementModel.RotateY1Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.I:
                    cubeArrangementModel.RotateY2Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.O:
                    cubeArrangementModel.RotateY3Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.J:
                    cubeArrangementModel.RotateY1Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.K:
                    cubeArrangementModel.RotateY2Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.L:
                    cubeArrangementModel.RotateY3Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.Number1:
                    cubeArrangementModel.RotateX1Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.Number2:
                    cubeArrangementModel.RotateX2Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.Number3:
                    cubeArrangementModel.RotateX3Forward(CubeArrangementModel.Direction.Forward);
                    break;
                case Key.Number4:
                    cubeArrangementModel.RotateX1Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.Number5:
                    cubeArrangementModel.RotateX2Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.Number6:
                    cubeArrangementModel.RotateX3Forward(CubeArrangementModel.Direction.Backward);
                    break;
                case Key.R:
                    await cubeArrangementModel.ShuffleAsync();
                    break;
            }
        }

        
        private static void Window_Update(double deltaTime)
        {
            //Console.WriteLine($"Update after {deltaTime} [s].");
            // multithreaded
            // make sure it is threadsafe
            // NO GL calls
            cubeArrangementModel.AdvanceTime(deltaTime);
        }

        private static unsafe void Window_Render(double deltaTime)
        {
            //Console.WriteLine($"Render after {deltaTime} [s].");
            // GL here
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);
            Gl.UseProgram(program);
            SetViewMatrix();
            SetProjectionMatrix();
            DrawRubicsCube();
        }

        private static unsafe void DrawRubicsCube()
        {
            for (int i = 0; i < 27; i++)
            {
                Matrix4X4<float> modelMatrix = cubes[i].ModelMatrix;
                var modelMatrixForCenterCube = modelMatrix * Matrix4X4.CreateScale((float)cubeArrangementModel.CenterCubeScale);
                SetModelMatrix(modelMatrixForCenterCube);
                
                // Draw the cube
                Gl.BindVertexArray(cubes[i].Vao);
                Gl.DrawElements(GLEnum.Triangles, cubes[i].IndexArrayLength, GLEnum.UnsignedInt, null);
                Gl.BindVertexArray(0);
            }
        }

        private static unsafe void SetModelMatrix(Matrix4X4<float> modelMatrix)
        {
            int location = Gl.GetUniformLocation(program, ModelMatrixVariableName);
            if (location == -1)
            {
                throw new Exception($"{ModelMatrixVariableName} uniform not found on shader.");
            }

            Gl.UniformMatrix4(location, 1, false, (float*)&modelMatrix);
            CheckError();
        }

        private static unsafe void SetUpObjects()
        {
            int i = 0;
            int type = 0;
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        int offsetX = l;
                        int offsetY = k;
                        int offsetZ = j;
                        float[] face1Color = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
                        float[] face2Color = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
                        float[] face3Color = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
                        float[] face4Color = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
                        float[] face5Color = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
                        float[] face6Color = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
                        if (j == 1)
                        {
                            face2Color = [1.0f, 1.0f, 1.0f, 1.0f];
                        }

                        if (j == -1)
                        {
                            face5Color = [0.957f, 1f, 0f, 1.0f];
                        }

                        if (k == 1)
                        {
                            face1Color = [1.0f, 0.486f, 0f, 1.0f];
                        }

                        if (k == -1)
                        {
                            face4Color = [1.0f, 0.0f, 0.0f, 1.0f];
                        }

                        if (l == 1)
                        {
                            face6Color = [0.0f, 0.0f, 1.0f, 1.0f];
                        }

                        if (l == -1)
                        {
                            face3Color = [0.0f, 1.0f, 0.0f, 1.0f];
                        }


                    // hatulrol jon elore lentrol
                    // the cubos on the right side
                    cubes[i] = GlCube.CreateCubeWithFaceColors(Gl, face1Color, face2Color, face3Color,
                        face4Color, face5Color, face6Color, offsetX, offsetY, offsetZ);
                        i++;
                    }
                }
            }
            
        }
        
        private static void Window_Closing()
        {
            for (int i = 0; i < 27; i++)
            {
                cubes[i].ReleaseGlCube();
            }
        }

        private static unsafe void SetProjectionMatrix()
        {
            var projectionMatrix = Matrix4X4.CreatePerspectiveFieldOfView<float>((float)Math.PI / 4f, 1024f / 768f, 0.1f, 100);
            int location = Gl.GetUniformLocation(program, ProjectionMatrixVariableName);

            if (location == -1)
            {
                throw new Exception($"{ViewMatrixVariableName} uniform not found on shader.");
            }

            Gl.UniformMatrix4(location, 1, false, (float*)&projectionMatrix);
            CheckError();
        }

        private static unsafe void SetViewMatrix()
        {
            var viewMatrix = Matrix4X4.CreateLookAt(cameraDescriptor.Position, cameraDescriptor.Target, cameraDescriptor.UpVector);
            int location = Gl.GetUniformLocation(program, ViewMatrixVariableName);

            if (location == -1)
            {
                throw new Exception($"{ViewMatrixVariableName} uniform not found on shader.");
            }

            Gl.UniformMatrix4(location, 1, false, (float*)&viewMatrix);
            CheckError();
        }

        public static void CheckError()
        {
            var error = (ErrorCode)Gl.GetError();
            if (error != ErrorCode.NoError)
                throw new Exception("GL.GetError() returned " + error.ToString());
        }
    }
}