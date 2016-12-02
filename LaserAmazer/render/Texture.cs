using OpenTK.Graphics.ES20;
using System.Drawing;

namespace LaserAmazer.Render
{
    public class Texture
    {

        private int texture;

        /**
         * Instantiates a new Texture from an image file.
         * @param String path
         */
        public Texture(string path)
        {
            LoadTexture(path);
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
                GL.ActiveTexture(TextureUnit.Texture0 + sampler);
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

        public void LoadTexture(string file)
        {
            Bitmap bitmap = new Bitmap(file);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D((All)TextureTarget.Texture2D, 0, (All)PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                (All)OpenTK.Graphics.OpenGL.PixelFormat.Bgra, (All)PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

    }
}