using LaserAmazer.Render;

namespace LaserAmazer.Gui
{
    public class Text : UIElement
    {

        private GameFont label;
        private float xOffset, yOffset, size;
        private Alignment align;

        /**
         * New Text UIElement
         * @param xOffset
         * @param yOffset
         * @param str
         * @param f
         * @param size
         */
        public Text(float xOffset, float yOffset, string str, FloatColor f, float size)
        {
            label = new GameFont(str, f);
            this.xOffset = xOffset / 10f;
            this.yOffset = yOffset / 10f;
            this.size = size / 10f;
        }

        /**
         * 
         * @param xOffset
         * @param yOffset
         * @param align
         * @param str
         * @param f
         * @param size
         */
        public Text(float xOffset, float yOffset, Alignment align, string str, FloatColor f, float size)
        {
            yOffset /= 20f;
            size /= 10f;

            switch (align)
            {
                case Alignment.LEFT:
                    xOffset = -1.5f;
                    break;
                case Alignment.CENTER:
                    xOffset = -0.05f / (0.3f / size);
                    xOffset *= str.Length;
                    break;
                case Alignment.RIGHT:
                    float characterShift = str.Length * 2f + 1f;
                    xOffset = 1.5f - ((str.Length + str.Length / characterShift) / 10f);
                    break;
            }

            label = new GameFont(str, f);
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.size = size;
            this.align = align;
        }

        public void Render()
        {
            label.RenderString(label.getRenderString(), xOffset, yOffset, size);
        }

        /**
         * Change the label rendered by the text
         * @param f
         */
        public void SetLabel(GameFont f)
        {
            label = f;
            string str = label.getRenderString();

            switch (align)
            {
                case Alignment.LEFT:
                    xOffset = -1.5f;
                    break;
                case Alignment.CENTER:
                    xOffset = -0.05f / (0.3f / size);
                    xOffset *= str.Length;
                    break;
                case Alignment.RIGHT:
                    float characterShift = str.Length * 2f + 1f;
                    xOffset = 1.5f - ((str.Length + str.Length / characterShift) / 10f);
                    break;
            }
        }

        public void SetLabelstring(string str)
        {
            label.SetRenderString(str);
        }

    }
}