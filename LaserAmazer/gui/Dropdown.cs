using LaserAmazer.Render;
using System.Collections.Generic;

namespace LaserAmazer.Gui
{
    public class Dropdown : Button
    {

        private LinkedList<Button> options = new LinkedList<Button>();
        private bool open = false;
        private float length = 0;

        public Dropdown(float[] coords, float[] tCoords, int[] indices, GameFont f, float xOffset, float yOffset, float height, float width) : base(coords, tCoords, indices, null, f, xOffset, yOffset, height, width)
        {
        }

        /**
         * Adds the button passed and attaches another callback for this dropdown
         * @param b
         */
        public void AddButton(Button b)
        {
            b.AddCallback(() =>
            {
                ChoiceClicked();
            });

            length -= (b.height + .05f);
            b.Move(0, length, 0);
            options.AddLast(b);
        }

        public void addButtons(Button[] b)
        {
            foreach (Button but in b)
            {
                but.AddCallback(() =>
                {
                    ChoiceClicked();
                });

                length -= (but.height / 2f + .05f);
                but.Move(0, length, 0);
                options.AddLast(but);
            }
        }

        public new bool CheckClick(float xPos, float yPos)
        {
            if (open)
            {
                // Check click on all buttons in the dropdown first
                foreach (Button b in options)
                {
                    b.CheckClick(xPos, yPos);
                }
            }

            // Then check if the dropdown was clicked
            if (UIUtils.Pnpoly(base.xCoords, base.yCoords, xPos * GameInstance.window.ratio, yPos))
            {
                ChoiceClicked();
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Toggles whether the Dropdown is open.
         */
        public void ChoiceClicked()
        {
            open = !open;
        }

        /**
         * Renders everything necessary for a Dropdown menu.
         */
        public new void Render()
        {
            base.Render();

            if (open)
            {
                foreach (Button b in options)
                {
                    b.Render();
                }
            }
        }

    }
}