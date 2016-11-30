using OpenTK.Graphics.ES11;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

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
                Bitmap image = Bitmap.FromStream(assembly.GetManifestResourceStream("/textures/" + path));
                width = image.Width;
                height = image.Height;

                int[] pixelsRaw = new int[width * height * 4];
                ImageConverter converter = new ImageConverter();
                pixelsRaw = (int[])converter.ConvertTo(image, typeof(int[]));

                pixelsRaw = getRGB(image, 0, 0, width, height, null, 0, width);

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

        public static void getRGB(this Bitmap image, int startX, int startY, int w, int h, int[] rgbArray, int offset, int scansize)
        {
            const int PixelWidth = 3;
            const PixelFormat PixelFormat = PixelFormat.Format24bppRgb;

            // En garde!
            if (image == null) throw new ArgumentNullException("image");
            if (rgbArray == null) throw new ArgumentNullException("rgbArray");
            if (startX < 0 || startX + w > image.Width) throw new ArgumentOutOfRangeException("startX");
            if (startY < 0 || startY + h > image.Height) throw new ArgumentOutOfRangeException("startY");
            if (w < 0 || w > scansize || w > image.Width) throw new ArgumentOutOfRangeException("w");
            if (h < 0 || (rgbArray.Length < offset + h * scansize) || h > image.Height) throw new ArgumentOutOfRangeException("h");

            BitmapData data = image.LockBits(new Rectangle(startX, startY, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat);
            try
            {
                byte[] pixelData = new Byte[data.Stride];
                for (int scanline = 0; scanline < data.Height; scanline++)
                {
                    Marshal.Copy(data.Scan0 + (scanline * data.Stride), pixelData, 0, data.Stride);
                    for (int pixeloffset = 0; pixeloffset < data.Width; pixeloffset++)
                    {
                        // PixelFormat.Format32bppRgb means the data is stored
                        // in memory as BGR. We want RGB, so we must do some 
                        // bit-shuffling.
                        rgbArray[offset + (scanline * scansize) + pixeloffset] =
                            (pixelData[pixeloffset * PixelWidth + 2] << 16) +   // R 
                            (pixelData[pixeloffset * PixelWidth + 1] << 8) +    // G
                            pixelData[pixeloffset * PixelWidth];                // B
                    }
                }
            }
            finally
            {
                image.UnlockBits(data);
            }
        }


    }
}