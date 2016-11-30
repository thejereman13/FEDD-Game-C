using OpenTK.Graphics.ES11;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace LaserAmazer.Render
{
    public class Texture
    {

        private int texture, width, height;

        /**
         * Instantiates a new Texture from an image file.
         * @param String path
         */
        public Texture(string path)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            try
            {
                Image image = Image.FromStream(assembly.GetManifestResourceStream("/textures/" + path));
                width = image.Width;
                height = image.Height;

                int[] pixelsRaw = new int[width * height * 4];
                pixelsRaw = image.getRGB(0, 0, width, height, null, 0, width);

                ByteBuffer pixels = BufferUtils.createByteBuffer(width * height * 4);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        int pixel = pixelsRaw[i * width + j];
                        pixels.put((byte)((pixel >> 16) & 0xFF)); // Red
                        pixels.put((byte)((pixel >> 8) & 0xFF)); // Green
                        pixels.put((byte)(pixel & 0xFF)); // Blue
                        pixels.put((byte)((pixel >> 24) & 0xFF)); // Alpha
                    }
                }

                pixels.flip();

                texture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) All.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) All.Nearest);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            }
            catch (IOException ex)
            {
                Console.Write(ex.ToString());
            }
        }

        protected void Finalize()
        {
            GL.DeleteTexture(texture);
            //base.Finalize();
        }

        /**
         * Creates the texture within LWJGL
         * @param int sampler
         */
        public void Bind(int sampler)
        {
            if (sampler >= 0 && sampler <= 31)
            {
                GL.ActiveTexture(GL_TEXTURE0 + sampler);
                GL.BindTexture(TextureTarget.Texture2D, texture);
            }
        }

        /**
         * Removes the texture from LWJGL
         */
        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

    }
}