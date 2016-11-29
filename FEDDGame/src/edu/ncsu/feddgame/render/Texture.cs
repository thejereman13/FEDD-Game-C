
public class Texture {
	
	private int texture, width, height;
	
	/**
	 * Instantiates a new Texture from an image file.
	 * @param String path
	 */
	public Texture(string path) {
		BufferedImage image;

		try {
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
			
			texture = glGenTextures();
			glBindTexture(GL_TEXTURE_2D, texture);
			glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
			glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
			glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width, height, 0, GL_RGBA, GL_UNSIGNED_BYTE, pixels);
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	protected void finalize() throws Throwable {
		glDeleteTextures(texture);
		super.finalize();
	}
	
	/**
	 * Creates the texture within LWJGL
	 * @param int sampler
	 */
	public void bind(int sampler) {
		if (sampler >= 0 && sampler <= 31) {
			glActiveTexture(GL_TEXTURE0 + sampler);
			glBindTexture(GL_TEXTURE_2D, texture);
		}
	}
	
	/**
	 * Removes the texture from LWJGL
	 */
	public void unbind() {
		glBindTexture(GL_TEXTURE_2D, 0);
	}
	
}
