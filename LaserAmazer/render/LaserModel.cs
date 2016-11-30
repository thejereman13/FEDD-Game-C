using OpenTK;
using System;

namespace LaserAmazer.Render
{
    public class LaserModel : Model
    {

        private static readonly float LASER_WIDTH = .2f; // Width of the laser rendered

        private static Vector2d originV = new Vector2d(10, 0);
        private float angle; // Angle in radians
        private float[] coords = new float[2];

        public Vector2d vect;
        public int xDir = 0, yDir = 0;

        /**
         * New LaserModel given angle and length
         * @param vertices
         * @param tCoords
         * @param indices
         * @param x0
         * @param y0
         * @param x1
         * @param y1
         */
        public LaserModel(float[] tCoords, int[] indices, float x0, float y0, float angle, float length) : base(GetVertices(x0, y0, angle, length, LASER_WIDTH), tCoords, indices, 4, GameTexture.LASER)
        {
            this.angle = angle;
            this.vect = new Vector2d(length * System.Math.Cos(angle), length * System.Math.Sin(angle));
            coords[0] = x0;
            coords[1] = y0;

            DetermineDirection();
        }

        /**
         * New LaserModel given a vector
         * @param tCoords
         * @param indices
         * @param x0
         * @param y0
         * @param vect
         */
        public LaserModel(float[] tCoords, int[] indices, float x0, float y0, Vector2d vect) : base(GetVertices(x0, y0, FindAngle(vect), (float)vect.Length, LASER_WIDTH), tCoords, indices, 4, GameTexture.LASER)
        {
            this.angle = FindAngle(vect);
            this.vect = vect;
            coords[0] = x0;
            coords[1] = y0;

            DetermineDirection();
        }

        /**
         * Returns the coordinates for vertices of a rectangle extruded from the line passed of width w
         * @param begX
         * @param begY
         * @param endX
         * @param endY
         * @param LASER_WIDTH
         * @return
         */
        private static float[] GetVertices(float begX, float begY, float angle, float length, float width)
        {
            length += .05f;

            float endX = length * (float)System.Math.Cos(angle) + begX;
            float endY = length * (float)System.Math.Sin(angle) + begY;
            float dy = endY - begY;
            float dx = endX - begX;
            float xS = (width * dy / length) / 2;
            float yS = (width * dx / length) / 2;

            float[] vertices = new float[] {
                begX - xS, begY + yS, 0f,
                begX + xS, begY - yS, 0f,
                endX + xS, endY - yS, 0f,
                endX - xS, endY + yS, 0f,
        };

            return vertices;
        }

        /**
         * @return float value of the angle of the laser line
         */
        public float GetAngle()
        {
            return angle;
        }

        private static float FindAngle(Vector2d vec)
        {
            return (float)System.Math.Atan2(vec.Y, vec.X);
        }

        /**
         * Sets the initial angle of the laser
         * @param a
         */
        public void SetAngle(float a)
        {
            this.angle = a;
            this.vect = new Vector2d(.1f * System.Math.Cos(a), .1f * System.Math.Sin(a));
            DetermineDirection();
        }

        /**
         * Sets a value of 1 for positive, -1 for negative for the vector directions of the laser
         */
        private void DetermineDirection()
        {
            // Y
            if (System.Math.Sin(angle) >= 0)
            {
                yDir = 1;
            }
            else
            {
                yDir = -1;
            }

            // X
            if (System.Math.Cos(angle) >= 0)
            {
                xDir = 1;
            }
            else
            {
                xDir = -1;
            }
        }

        public void SetLength(float length)
        {
            this.vertices = GetVertices(coords[0], coords[1], angle, length, LASER_WIDTH);
        }

        public void SetVector(Vector2d vect)
        {
            this.vect = vect;
            this.angle = FindAngle(vect);
        }

        /**
         * Returns the coordinates of the line from which the laser is generated
         * @return
         */
        public float[] GetCoords()
        {
            return coords;
        }

        /**
         * Sets the starting coordinates of the laser
         * @param f
         */
        public void SetCoords(float[] f)
        {
            coords = f;
        }

        /* TODO: Update
        public new void Render() {

            if (!renderGens) {
                tex = new Texture(texStr);
                vertexId = glGenBuffers();
                textureId = GL.GenBuffers();
                indexId = GL.GenBuffers();
                renderGens = true;
                coors = createBuffer(tCoords);
                indec = BufferUtils.createIntBuffer(indices.length);
                indec.put(indices);
                indec.flip();
            }

            newv = vertices.clone();
            for (int i = 0; i < this.sideCount; i++){
                newv[i * 3] /= GameInstance.window.ratio;
            }
            vert = createBuffer(newv);

            GL.BindBuffer(GL_ARRAY_BUFFER, vertexId);
            GL.BufferData(GL_ARRAY_BUFFER, vert, GL_STATIC_DRAW);


            GL.BindBuffer(GL_ARRAY_BUFFER, textureId);
            GL.BufferData(GL_ARRAY_BUFFER, coors, GL_STATIC_DRAW);

            GL.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexId);



            GL.BufferData(GL_ELEMENT_ARRAY_BUFFER, indec, GL_STATIC_DRAW);

            GL.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
            GL.BindBuffer(GL_ARRAY_BUFFER, 0);
            tex.bind(0);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(GL_ARRAY_BUFFER, vertexId);
            GL.VertexAttribPointer(0, 3, GL_FLOAT, false, 0, 0);

            GL.BindBuffer(GL_ARRAY_BUFFER, textureId);
            GL.VertexAttribPointer(1, 2, GL_FLOAT, false, 0, 0);

            GL.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexId);
            GL.DrawElements(GL_TRIANGLES, drawCount, GL_UNSIGNED_INT, 0);

            GL.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
            GL.BindBuffer(GL_ARRAY_BUFFER, 0);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            tex.unbind();

        }
        */

    }
}