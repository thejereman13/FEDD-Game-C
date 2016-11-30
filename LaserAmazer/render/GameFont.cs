using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace LaserAmazer.render
{
    public class GameFont
    {

        private int fontTexture;
        private FloatColor color;
        private string renderStr;

        private readonly static int GRID_SIZE = 16;

        public GameFont(string str, FloatColor color)
        {
            this.renderStr = str;
            this.color = color;

            try
            {
                setUpTextures();
            }
            catch (Exception e)
            {
            }
        }

        private void setUpTextures()
        {
            fontTexture = GL.GenTexture(); // Create new texture for the bitmap font

            // Bind the texture object to the GL_TEXTURE_2D target, specifying that it will be a 2D texture
            GL.BindTexture(TextureTarget.Texture2D, fontTexture);

            /* Use TWL's utility classes to load the png file
            PNGDecoder decoder = new PNGDecoder(getClass().getResourceAsStream("/font.png"));
            ByteBuffer buffer = BufferUtils.createByteBuffer(4 * decoder.getWidth() * decoder.getHeight());
            decoder.decode(buffer, decoder.getWidth() * 4, PNGDecoder.Format.RGBA);
            buffer.flip();
            */

            Bitmap bmp = new Bitmap("res/font.png");
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Load the previously loaded texture data into the texture object.
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, bmp_data.Scan0);
            bmp.UnlockBits(bmp_data);

            GL.BindTexture(TextureTarget.Texture2D, 0); // Unbind the texture
        }

        public void renderString(string str, float x, float y, float characterWidth)
        {
            GameInstance.shader.unbind();

            float ratio = GameInstance.window.ratio;
            float characterHeight = 0.52f * characterWidth; // Automatically calculate the height from aspect ratio
            characterWidth /= ratio;
            x /= ratio;

            GL.PushAttrib(AttribMask.TextureBit | AttribMask.EnableBit | AttribMask.ColorBufferBit);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, fontTexture);

            // Enable linear texture filtering for smoothed results.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            // Enable additive blending. This means that the colors will be added to already existing colors in the
            // frame buffer. In practice, this makes the black parts of the texture become invisible.
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

            GL.PushMatrix(); // Store the current model-view matrix

            // Offset all subsequent (at least up until 'GL.PopMatrix') vertex coordinates.
            GL.Translate(x, y, 0);

            GL.Color4(color.red(), color.green(), color.blue(), 1f); // Set Font color

            GL.Begin(BeginMode.Quads);

            // Iterate over all the characters in the string.
            for (int i = 0; i < str.Length; i++)
            {
                // Get the ASCII-code of the character by type-casting to integer.
                int asciiCode = (int)str.ToCharArray()[i];

                // There are 16 cells in a texture, and a texture coordinate ranges from 0.0 to 1.0.
                float cellSize = 1.0f / GRID_SIZE;

                // The cell's x-coordinate is the greatest integer smaller than remainder of the ASCII-code divided by the
                // amount of cells on the x-axis, times the cell size.
                float cellX = ((int)asciiCode % GRID_SIZE) * cellSize;
                // The cell's y-coordinate is the greatest integer smaller than the ASCII-code divided by the amount of
                // cells on the y-axis.
                float cellY = ((int)asciiCode / GRID_SIZE) * cellSize;

                GL.TexCoord2(cellX, cellY + cellSize);
                GL.Vertex2(i * characterWidth / 3, y);
                GL.TexCoord2(cellX + cellSize, cellY + cellSize);
                GL.Vertex2(i * characterWidth / 3 + characterWidth / 2, y);
                GL.TexCoord2(cellX + cellSize, cellY);
                GL.Vertex2(i * characterWidth / 3 + characterWidth / 2, y + characterHeight);
                GL.TexCoord2(cellX, cellY);
                GL.Vertex2(i * characterWidth / 3, y + characterHeight);
            }

            GL.End();
            GL.PopMatrix();
            GL.PopAttrib();
            GameInstance.shader.bind();
        }

        public void renderString(string str, Alignment align, float y, float characterWidth)
        {
            float x = 0;

            // Moderately arbitrary algorithms to get the desired text placement outcome
            switch (align)
            {
                case Alignment.LEFT:
                    x = -1.5f;
                    break;
                case Alignment.CENTER:
                    x = -0.05f / (0.3f / characterWidth);
                    x *= str.Length;
                    break;
                case Alignment.RIGHT:
                    float characterShift = str.Length * 2f + 1f;
                    x = 1.5f - ((str.Length + str.Length / characterShift) / 10f);
                    break;
            }

            renderString(str, x, y, characterWidth);
        }

        public int getFontTexture()
        {
            return fontTexture;
        }

        public string getRenderString()
        {
            return renderStr;
        }
        public void setRenderString(string str)
        {
            renderStr = str;
        }

        public void setColor(FloatColor color)
        {
            this.color = color;
        }

    }
}