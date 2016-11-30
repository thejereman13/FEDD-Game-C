using System;

namespace LaserAmazer.render
{
    public class MovableModel : Model, IClickable
    {

        public float[] xCoords;     //Top Left, Top Right, Bottom Right, Bottom Left
        public float[] yCoords;

        private Action callback = null;
        public MovableModel(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, int sides) : base(vertices, tCoords, indices, sides, "moveablebox.png")
        {
            adjustOffset(xOffset, yOffset);
        }
        public MovableModel(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, int sides, String tex) : base(vertices, tCoords, indices, sides, tex)
        {
            adjustOffset(xOffset, yOffset);
        }

        public void setCallback(Action r)
        {
            this.callback = r;

        }

        private void adjustOffset(float xOffset, float yOffset)
        {
            float[] coords = new float[3 * base.sideCount];
            for (int i = 0; i < this.sideCount; i++)
            {
                coords[i * 3] = base.vertices[i * 3] + xOffset - base.xOffset;
            }
            for (int i = 0; i < this.sideCount; i++)
            {
                coords[i * 3 + 1] = base.vertices[i * 3 + 1] + yOffset - base.yOffset;
            }
            for (int i = 0; i < this.sideCount; i++)
            {
                coords[i * 3 + 2] = 0;
            }

            splitCoords(coords);
            base.setVertices(coords);
            base.xOffset = xOffset;
            base.yOffset = yOffset;
        }

        private void splitCoords(float[] coords)
        {
            this.xCoords = new float[sideCount];
            this.yCoords = new float[sideCount];
            for (int i = 0; i < this.sideCount; i++)
            {
                this.xCoords[i] = coords[i * 3];
            }
            for (int i = 0; i < this.sideCount; i++)
            {
                this.yCoords[i] = coords[i * 3 + 1];
            }
        }

        /**
         * Returns whether the button was clicked based on mouse coordinates passed
         * @param xPos
         * @param yPos
         * @return
         */
        public bool checkClick(float xPos, float yPos)
        {
            splitCoords(base.vertices);
            if (UIUtils.pnpoly(xCoords, yCoords, xPos * GameInstance.window.ratio, yPos))
            {
                if (callback != null)
                    callback.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Translates the model to the coordinates provided
         * @param xPos
         * @param yPos
         */
        public void followCursor(float xPos, float yPos)
        {
            if (xPos > 10)
                xPos = 10;
            if (xPos < -10)
                xPos = -10;
            if (yPos > 10)
                yPos = 10;
            if (yPos < -10)
                yPos = -10;
            adjustOffset(xPos * GameInstance.window.ratio, yPos);
        }

    }
}