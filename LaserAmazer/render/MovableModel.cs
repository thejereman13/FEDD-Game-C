using LaserAmazer.Gui;
using System;

namespace LaserAmazer.Render
{
    public class MovableModel : Model, IClickable
    {

        public float[] xCoords;     //Top Left, Top Right, Bottom Right, Bottom Left
        public float[] yCoords;

        private Action callback = null;
        public MovableModel(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, int sides) : base(vertices, tCoords, indices, sides, "moveablebox.png")
        {
            AdjustOffset(xOffset, yOffset);
        }
        public MovableModel(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, int sides, String tex) : base(vertices, tCoords, indices, sides, tex)
        {
            AdjustOffset(xOffset, yOffset);
        }

        public void SetCallback(Action r)
        {
            callback = r;
        }

        private void AdjustOffset(float xOffset, float yOffset)
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

            SplitCoords(coords);
            base.SetVertices(coords);
            base.xOffset = xOffset;
            base.yOffset = yOffset;
        }

        private void SplitCoords(float[] coords)
        {
            xCoords = new float[sideCount];
            yCoords = new float[sideCount];
            for (int i = 0; i < sideCount; i++)
            {
                xCoords[i] = coords[i * 3];
            }
            for (int i = 0; i < sideCount; i++)
            {
                yCoords[i] = coords[i * 3 + 1];
            }
        }

        /**
         * Returns whether the button was clicked based on mouse coordinates passed
         * @param xPos
         * @param yPos
         * @return
         */
        public bool CheckClick(float xPos, float yPos)
        {
            SplitCoords(base.vertices);
            if (UIUtils.Pnpoly(xCoords, yCoords, xPos * GameInstance.window.ratio, yPos))
            {
                if (callback != null)
                    callback.Invoke();
                return true;
            }

            return false;
        }

        /**
         * Translates the model to the coordinates provided
         * @param xPos
         * @param yPos
         */
        public void FollowCursor(float xPos, float yPos)
        {
            if (xPos > 10)
                xPos = 10;
            if (xPos < -10)
                xPos = -10;
            if (yPos > 10)
                yPos = 10;
            if (yPos < -10)
                yPos = -10;
            AdjustOffset(xPos * GameInstance.window.ratio, yPos);
        }

    }
}