using System.Collections.Generic;
using System;

namespace LaserAmazer.gui
{
    public class Button : Model, UIElement, IClickable
    {

        private List<Action> callbacks = new List<Action>();

        protected float[] xCoords, yCoords;
        private GameFont label;
        public float height, width;

        /**
         * New Button extension from Model with Runnable object for execution on click events
         * @param coords
         * @param tCoords
         * @param indices
         * @param r
         */
        public Button(float[] coords, float[] tCoords, int[] indices, Action r, float xOffset, float yOffset, float height, float width) : base(coords, tCoords, indices, 4, GameTexture.BUTTON)
        {
            callbacks.Add(r);
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.height = height;
            this.width = width;
            label = new GameFont("", new FloatColor(0, 0, 0));
            xCoords = new float[] { coords[0], coords[3], coords[6], coords[9] };
            yCoords = new float[] { coords[1], coords[4], coords[7], coords[10] };
        }

        /**
         * 
         * @param coords
         * @param tCoords
         * @param indices
         * @param r
         * @param f
         * @param xOffset
         * @param yOffset
         * @param height
         * @param width
         */
        public Button(float[] coords, float[] tCoords, int[] indices, Action r, GameFont f, float xOffset, float yOffset, float height, float width) : base(coords, tCoords, indices, 4, GameTexture.BUTTON)
        {
            callbacks.Add(r);
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.height = height;
            this.width = width;
            this.label = f;
            xCoords = new float[] { coords[0], coords[3], coords[6], coords[9] };
            yCoords = new float[] { coords[1], coords[4], coords[7], coords[10] };
        }

        public void addCallback(Action r)
        {
            callbacks.Add(r);
        }

        /**
         * Returns a callback runnable previously declared
         * @return
         */
        public Action getCallback(int index)
        {
            return callbacks[index];
        }

        /**
         * Returns whether the button was clicked based on mouse coordinates passed
         * @param xPos
         * @param yPos
         * @return
         */
        public bool checkClick(float xPos, float yPos)
        {
            if (UIUtils.pnpoly(xCoords, yCoords, xPos * GameInstance.window.ratio, yPos))
            {
                if (callbacks != null)
                {
                    foreach (Action r in callbacks)
                    {
                        r.Invoke();
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public new void render()
        {
            base.render();
            float w = width / (float)label.getRenderString().Length / 3.5f;
            if (.52f * w > .13f * height)
                w = .13f * height / .52f;
            label.renderString(label.getRenderString(), xCoords[0] / 10, (yOffset - w * 2.6f) / 20, w);
        }

        public new void move(float x, float y, float z)
        {
            base.move(x, y, z);
            yOffset += y; // Manually set the new coordinates after move, needed for font movement
            xCoords = new float[] { base.vertices[0], base.vertices[3], base.vertices[6], base.vertices[9] };
            yCoords = new float[] { base.vertices[1], base.vertices[4], base.vertices[7], base.vertices[10] };
        }

        public void setCallback(Action r) { }

        /**
         * Change the label rendered by the text
         * @param f
         */
        public void setLabel(GameFont f)
        {
            this.label = f;
        }

    }
}