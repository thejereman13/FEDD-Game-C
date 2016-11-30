using OpenTK.Graphics.OpenGL4;
using System;

namespace LaserAmazer.render
{
    public class Model
    {

        protected int drawCount;
        static int vertexId, textureId, indexId;
        public int sideCount;
        protected bool generated = false;
        public float[] vertices, tCoords;
        protected int[] indices;
        protected Texture tex;
        protected string texStr;
        protected float xOffset, yOffset;
        protected float[] newv;

        private string defaultTexString = "bgtile.png";
        /**
         * Create and bind the vertices, texture coordinates, and indices to the graphics shader.
         * @param vertices
         * @param tCoords
         * @param indices
         * @param texture
         */
        public Model(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, int sideNum, string texture)
        {
            this.indices = indices;
            this.tCoords = tCoords;
            this.vertices = vertices;
            this.sideCount = sideNum;
            drawCount = indices.Length;
            this.texStr = texture;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        public Model(float[] vertices, float[] tCoords, int[] indices, int sideNum, string texture)
        {
            this.indices = indices;
            this.tCoords = tCoords;
            this.vertices = vertices;
            this.sideCount = sideNum;
            drawCount = indices.Length;
            this.texStr = texture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="tCoords"></param>
        /// <param name="indices"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="sideNum"></param>
        public Model(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, int sideNum)
        {
            this.indices = indices;
            this.tCoords = tCoords;
            this.vertices = vertices;
            this.sideCount = sideNum;
            drawCount = indices.Length;
            this.texStr = defaultTexString;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="tCoords"></param>
        /// <param name="indices"></param>
        /// <param name="sideNum"></param>
        public Model(float[] vertices, float[] tCoords, int[] indices, int sideNum)
        {
            this.indices = indices;
            this.tCoords = tCoords;
            this.vertices = vertices;
            this.sideCount = sideNum;
            drawCount = indices.Length;
            this.texStr = defaultTexString;
        }

        protected void finalize()
        {
            GL.DeleteBuffer(vertexId);
            GL.DeleteBuffer(textureId);
            GL.DeleteBuffer(indexId);
        }

        /**
         * Render all of the vertices on screen.
         */
        public void render()
        {
            if (!generated)
            {
                tex = new Texture(texStr);
                vertexId = GL.GenBuffer();
                textureId = GL.GenBuffer();
                indexId = GL.GenBuffer();
                generated = true;
            }

            newv = (float[])vertices.Clone();
            //System.out.println(ratio);
            for (int i = 0; i < this.sideCount; i++)
            {
                newv[i * 3] /= GameInstance.window.ratio;
            }

            //updateBuffer(vert, newv);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexId);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * newv.Length), newv, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, textureId);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * tCoords.Length), tCoords, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(float) * indices.Length), indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            tex.bind(0);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexId);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, textureId);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexId);
            GL.DrawElements(PrimitiveType.Triangles, drawCount, DrawElementsType.UnsignedInt, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            tex.unbind();
        }

        /**
         * @param data
         * @return buffer array of vertex or texture coordinate data
         * in the correct orientation.
         *
        protected FloatBuffer createBuffer(float[] data) {
            FloatBuffer buffer = BufferUtils.createFloatBuffer(data.length);
            buffer.put(data);
            buffer.flip();
            
            return buffer;
        }
        protected void updateBuffer(FloatBuffer b, float[] data) {
            b.clear();
            b.put(data);
            b.flip();
        }
        */


        /// <summary>
        /// Adjusts the model in each spatial direction by the amount passed
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void move(float x, float y, float z)
        {
            for (int i = 0; i < this.sideCount; i++)
            {
                this.vertices[i * 3] += x;
            }
            for (int i = 0; i < this.sideCount; i++)
            {
                this.vertices[i * 3 + 1] += y;
            }
            for (int i = 0; i < this.sideCount; i++)
            {
                this.vertices[i * 3 + 2] += z;
            }
        }

        public void setVertices(float[] vertices)
        {
            this.vertices = vertices;
        }

        /// <summary>
        /// Rotates the model by the angle specified in degrees
        /// </summary>
        /// <param name="angle"></param>
        public void rotate(float angle)
        {
            angle *= ((float)Math.PI / 180f);
            for (int i = 0; i < sideCount; i++)
            {
                float newX = (float)(xOffset + (this.vertices[i * 3] - xOffset) * (float)Math.Cos(angle) - (this.vertices[i * 3 + 1] - yOffset) * (float)Math.Sin(angle));
                float newY = (float)(yOffset + (this.vertices[i * 3] - xOffset) * (float)Math.Sin(angle) + (this.vertices[i * 3 + 1] - yOffset) * (float)Math.Cos(angle));
                vertices[i * 3] = newX;
                vertices[i * 3 + 1] = newY;
            }
        }

    }
}