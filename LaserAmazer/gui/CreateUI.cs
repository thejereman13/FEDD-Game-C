using LaserAmazer.Render;
using System;

namespace LaserAmazer.Gui
{
    public class CreateUI
    {

        /**
         * Creates a new button object and adds it to the window view
         * @param xOffset
         * @param yOffset
         * @param height
         * @param width
         * @param r
         */
        public static Button CreateButton(float xOffset, float yOffset, float width, float height, Action r, GameFont f)
        {
            // Vertices for a button
            float[] vertices = new float[] {
                -width/2f + xOffset, height/2f + yOffset, 0, // TOP LEFT - 0
				width/2f + xOffset, height/2f + yOffset, 0, // TOP RIGHT - 1
				width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM RIGHT - 2
				-width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM LEFT - 3
		    };

            float[] texture = new float[] {
                0, 0, // TOP LEFT
				1, 0, // TOP RIGHT
				1, 1, // BOTTOM RIGHT
				0, 1, // BOTTOM LEFT
		    };

            int[] indices = new int[] {
                0, 1, 2,
                2, 3, 0
            };

            return new Button(vertices, texture, indices, r, f, xOffset, yOffset, height, width);
        }

        /**
        * 
        * @param xOffset
        * @param yOffset
        * @param width
        * @param height
        * @param r
        * @return
        */
        public static Button createButton(float xOffset, float yOffset, float width, float height, Action r)
        {
            // Vertices for a button
            float[] vertices = new float[] {
                -width/2f + xOffset, height/2f + yOffset, 0, // TOP LEFT - 0
				width/2f + xOffset, height/2f + yOffset, 0, // TOP RIGHT - 1
				width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM RIGHT - 2
				-width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM LEFT - 3
		    };

            float[] texture = new float[] {
                0, 0, // TOP LEFT
				1, 0, // TOP RIGHT
				1, 1, // BOTTOM RIGHT
				0, 1, // BOTTOM LEFT
		    };

            int[] indices = new int[] {
                0, 1, 2,
                2, 3, 0
            };

            return new Button(vertices, texture, indices, r, xOffset, yOffset, height, width);
        }

        /**
         * New Dropdown with dimensions specified, adds buttons corresponding to each element in the Font and Runnable arrays
         *  of same width and height as the parent object
         * @param xOffset
         * @param yOffset
         * @param width
         * @param height
         * @param f
         * @param fs
         * @param rs
         * @return Dropdown
         */
        public static Dropdown createDropdown(float xOffset, float yOffset, float width, float height, GameFont f, GameFont[] fs, Action[] rs)
        {
            Dropdown d = createDropdown(xOffset, yOffset, width, height, f);

            for (int i = 0; i < rs.Length; i++)
            {
                d.AddButton(CreateButton(xOffset, yOffset, width, height, rs[i], fs[i]));
            }

            return d;
        }

        public static Dropdown createDropdown(float xOffset, float yOffset, float width, float height, GameFont f)
        {
            float[] vertices = new float[] {
                -width/2f + xOffset, height/2f + yOffset, 0, // TOP LEFT - 0
				width/2f + xOffset, height/2f + yOffset, 0, // TOP RIGHT - 1
				width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM RIGHT - 2
				-width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM LEFT - 3
		    };

            float[] texture = new float[] {
                0, 0, // TOP LEFT
				1, 0, // TOP RIGHT
				1, 1, // BOTTOM RIGHT
				0, 1, // BOTTOM LEFT
		    };

            int[] indices = new int[] {
                0, 1, 2,
                2, 3, 0
            };

            return new Dropdown(vertices, texture, indices, f, xOffset, yOffset, height, width);
        }

    }
}