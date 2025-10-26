using Silk.NET.OpenGL;
using Silk.NET.Maths;

namespace L3_dlim2227__3
{
    internal class GlCube
    {
        public uint Vao { get; }
        public uint Vertices { get; }
        public uint Colors { get; }
        public uint Indices { get; }
        public uint IndexArrayLength { get; }
        public Matrix4X4<float> ModelMatrix { get; set; } = Matrix4X4<float>.Identity;

        public double CubeScale { get; private set; } = 1;
        
        private GL Gl;

        public int x;
        public int y;
        public int z;
        private GlCube(uint vao, uint vertices, uint colors, uint indeces, uint indexArrayLength, GL gl, int x, int y, int z)
        {
            this.Vao = vao;
            this.Vertices = vertices;
            this.Colors = colors;
            this.Indices = indeces;
            this.IndexArrayLength = indexArrayLength;
            this.Gl = gl;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static unsafe GlCube CreateCubeWithFaceColors(GL Gl, float[] face1Color, float[] face2Color, float[] face3Color, float[] face4Color, float[] face5Color, float[] face6Color, int offsetX, int offsetY, int offsetZ)
        {
            uint vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);

            // counter clockwise is front facing
            float[] vertexArray = new float[] {
                -0.1f, 0.1f, 0.1f,
                0.1f, 0.1f, 0.1f,
                0.1f, 0.1f, -0.1f,
                -0.1f, 0.1f, -0.1f,

                -0.1f, 0.1f, 0.1f,
                -0.1f, -0.1f, 0.1f,
                0.1f, -0.1f, 0.1f,
                0.1f, 0.1f, 0.1f,

                -0.1f, 0.1f, 0.1f,
                -0.1f, 0.1f, -0.1f,
                -0.1f, -0.1f, -0.1f,
                -0.1f, -0.1f, 0.1f,

                -0.1f, -0.1f, 0.1f,
                0.1f, -0.1f, 0.1f,
                0.1f, -0.1f, -0.1f,
                -0.1f, -0.1f, -0.1f,

                0.1f, 0.1f, -0.1f,
                -0.1f, 0.1f, -0.1f,
                -0.1f, -0.1f, -0.1f,
                0.1f, -0.1f, -0.1f,

                0.1f, 0.1f, 0.1f,
                0.1f, 0.1f, -0.1f,
                0.1f, -0.1f, -0.1f,
                0.1f, -0.1f, 0.1f,
            };
            
            

            
            for (int i = 0; i < vertexArray.Length; i++)
            {
                if (i % 3 == 0) // x coordinate
                {
                    vertexArray[i] += (offsetX * 0.23f);
                }
                else if (i % 3 == 1) // y coordinate
                {
                    vertexArray[i] += (offsetY * 0.23f);
                }
                else if (i % 3 == 2) // z coordinate
                {
                    vertexArray[i] += (offsetZ * 0.23f);
                }
            }

            float[] colorArray = new float[] {
                face1Color[0], face1Color[1], face1Color[2], face1Color[3],
                face1Color[0], face1Color[1], face1Color[2], face1Color[3],
                face1Color[0], face1Color[1], face1Color[2], face1Color[3],
                face1Color[0], face1Color[1], face1Color[2], face1Color[3],
                
                face2Color[0], face2Color[1], face2Color[2], face2Color[3],
                face2Color[0], face2Color[1], face2Color[2], face2Color[3],
                face2Color[0], face2Color[1], face2Color[2], face2Color[3],
                face2Color[0], face2Color[1], face2Color[2], face2Color[3],
                
                face3Color[0], face3Color[1], face3Color[2], face3Color[3],
                face3Color[0], face3Color[1], face3Color[2], face3Color[3],
                face3Color[0], face3Color[1], face3Color[2], face3Color[3],
                face3Color[0], face3Color[1], face3Color[2], face3Color[3],
                
                face4Color[0], face4Color[1], face4Color[2], face4Color[3],
                face4Color[0], face4Color[1], face4Color[2], face4Color[3],
                face4Color[0], face4Color[1], face4Color[2], face4Color[3],
                face4Color[0], face4Color[1], face4Color[2], face4Color[3],
                
                face5Color[0], face5Color[1], face5Color[2], face5Color[3],
                face5Color[0], face5Color[1], face5Color[2], face5Color[3],
                face5Color[0], face5Color[1], face5Color[2], face5Color[3],
                face5Color[0], face5Color[1], face5Color[2], face5Color[3],
                
                face6Color[0], face6Color[1], face6Color[2], face6Color[3],
                face6Color[0], face6Color[1], face6Color[2], face6Color[3],
                face6Color[0], face6Color[1], face6Color[2], face6Color[3],
                face6Color[0], face6Color[1], face6Color[2], face6Color[3],
            };
            
            uint[] indexArray = new uint[] {
                0, 1, 2,
                0, 2, 3,

                4, 5, 6,
                4, 6, 7,

                8, 9, 10,
                10, 11, 8,

                12, 14, 13,
                12, 15, 14,

                17, 16, 19,
                17, 19, 18,

                20, 22, 21,
                20, 23, 22
            };

            uint vertices = Gl.GenBuffer();
            Gl.BindBuffer(GLEnum.ArrayBuffer, vertices);
            Gl.BufferData(GLEnum.ArrayBuffer, (ReadOnlySpan<float>)vertexArray.AsSpan(), GLEnum.StaticDraw);
            Gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, null);
            Gl.EnableVertexAttribArray(0);

            uint colors = Gl.GenBuffer();
            Gl.BindBuffer(GLEnum.ArrayBuffer, colors);
            Gl.BufferData(GLEnum.ArrayBuffer, (ReadOnlySpan<float>)colorArray.AsSpan(), GLEnum.StaticDraw);
            Gl.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, null);
            Gl.EnableVertexAttribArray(1);

            uint indices = Gl.GenBuffer();
            Gl.BindBuffer(GLEnum.ElementArrayBuffer, indices);
            Gl.BufferData(GLEnum.ElementArrayBuffer, (ReadOnlySpan<uint>)indexArray.AsSpan(), GLEnum.StaticDraw);

            // release array buffer
            Gl.BindBuffer(GLEnum.ArrayBuffer, 0);
            uint indexArrayLength = (uint)indexArray.Length;

            //var translationMatrix = Matrix4X4.CreateTranslation(new Vector3D<float>(offsetX, offsetY, offsetZ));
            //var modelMatrix = translationMatrix;
            
            return new GlCube(vao, vertices, colors, indices, indexArrayLength, Gl, offsetX, offsetY, offsetZ);
        }
        
        internal void ApplyRotation(float angle)
        {
            ModelMatrix = ModelMatrix * Matrix4X4.CreateRotationY(angle) * ModelMatrix;
        }

        public void SetModelMatrix(Matrix4X4<float> modelMatrix)
        {
            ModelMatrix = modelMatrix;
        }
        internal void ReleaseGlCube()
        {
            // always unbound the vertex buffer first, so no halfway results are displayed by accident
            Gl.DeleteBuffer(Vertices);
            Gl.DeleteBuffer(Colors);
            Gl.DeleteBuffer(Indices);
            Gl.DeleteVertexArray(Vao);
        }
        public void ApplyRotationX(float angle)
        {
            Matrix4X4<float> rotationMatrix = Matrix4X4.CreateRotationX(angle);
            ModelMatrix = ModelMatrix * rotationMatrix;
        }

        public void ApplyRotationY(float angle)
        {
            Matrix4X4<float> rotationMatrix = Matrix4X4.CreateRotationY(angle);
            ModelMatrix = ModelMatrix * rotationMatrix;
        }

        public void ApplyRotationZ(float angle)
        {
            Matrix4X4<float> rotationMatrix = Matrix4X4.CreateRotationZ(angle);
            ModelMatrix = ModelMatrix * rotationMatrix;
        }

        public void ResetModelMatrix()
        {
            ModelMatrix = Matrix4X4<float>.Identity;
        }
        
    }
}
