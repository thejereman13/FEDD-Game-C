using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

public class Texture {
	
	private int texture, width, height;
	
	/**
	 * Instantiates a new Texture from an image file.
	 * @param String path
	 */
	public Texture(string path) {
		//BufferedImage image;

		try {
			/*
			image = ImageIO.read(getClass().getResourceAsStream("/textures/" + path));
			width = image.getWidth();
			height = image.getHeight();
			
			int[] pixelsRaw = new int[width * height * 4];
			pixelsRaw = image.getRGB(0, 0, width, height, null, 0, width);
			
			ByteBuffer pixels = BufferUtils.createByteBuffer(width * height * 4);
			
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					int pixel = pixelsRaw[i * width + j];
					pixels.put((byte) ((pixel >> 16) & 0xFF)); // Red
					pixels.put((byte) ((pixel >> 8) & 0xFF)); // Green
					pixels.put((byte) (pixel & 0xFF)); // Blue
					pixels.put((byte) ((pixel >> 24) & 0xFF)); // Alpha
				}
			}
			
			pixels.flip();
			*/

			Bitmap bmp = new Bitmap("res/textures/" + path);
			BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			texture = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, texture);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, bmp_data.Scan0);
		} catch (Exception e) {
		}
	}
	
	protected void finalize() {
		GL.DeleteTexture(texture);
	}
	
	/**
	 * Creates the texture within LWJGL
	 * @param int sampler
	 */
	public void bind(int sampler) {
		if (sampler >= 0 && sampler <= 31) {
			GL.ActiveTexture(TextureUnit.Texture0 + sampler);
			GL.BindTexture(TextureTarget.Texture2D, texture);
		}
	}
	
	/**
	 * Removes the texture from LWJGL
	 */
	public void unbind() {
		GL.BindTexture(TextureTarget.Texture2D, 0);
	}
	
}
