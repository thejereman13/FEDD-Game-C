
using System;
using System.Collections.Generic;
using System.Threading;
using OpenTK.Graphics.OpenGL;

public class Window {

	long window;
	private float mouseX, mouseY;
	public float ratio;
	public int refreshRate, width, height;
	private bool fullscreen;
	public bool mouseHeld = false;
	private string title;
	public static bool isClicked = false;
	public static bool ctrlHeld = false;
	//private GLFWVidMode vidMode;
	private Text times;
	
	private List<UIElement> elementList = new List<UIElement>();
	
	public Window () : this(800, 800, "FEDD Game", false){
	}
	
	/**
	 * 
	 * @param width
	 * @param height
	 * @param title
	 * @param fullscreen
	 */
	public Window (int width, int height, string title, bool fullscreen) {
		this.width = width;
		this.height = height;
		this.ratio = (float) width / (float) height;
		this.title = title;
		this.fullscreen = fullscreen;
		createWindow();
	}
	
	/**
	 * Creates the window with the parameters
	 * of the local variables.
	 */
	private void createWindow() {
		/*
		long monitor = glfwGetPrimaryMonitor();
		window = glfwCreateWindow(width, height, title, fullscreen ? monitor : 0, 0);

		if (window == 0)
			throw new IllegalStateException("Failed to create window.");
		vidMode = glfwGetVideoMode(monitor);
		refreshRate = vidMode.refreshRate();
		
		glfwSetWindowPos(window, (vidMode.width() - width) / 2, (vidMode.height() - height) / 2); // Show window in center of screen
		glfwShowWindow(window);

		setWindowIcon();
		setKeyCallback();
		setWindowSizeCallback();
		setMouseButtonCallback();
		setMousePosCallback();
		glfwMakeContextCurrent(window);
		glfwSwapInterval(1); 	// Set Vsync (swap the double buffer from drawn to displayed every refresh cycle)
		*/
	}
	
	/**
	 * Adds all specified elements to the Window's array and scene
	 */
	public void addElements() {
		//Add items here that need to be rendered every frame on all screens
		if (GameInstance.getCurrentLevel() is Level && GameInstance.showTimer){
			times = new Text(10.25f, -2f, "Time: ", GameColor.TEAL, 1);
			elementList.Add(times);
		}
		if (GameInstance.getCurrentLevel() is Level)
			elementList.Add(new Text(10.2f, -1.5f, GameInstance.getCurrentLevel().getName(), GameColor.YELLOW, 1.4f));
	}
	public void updateTime(){
		if (GameInstance.getCurrentLevel() is Level)
			times.setLabelstring("Time: " + (int)((Level)GameInstance.getCurrentLevel()).getElapsedTime());
	}
	
	public void clearElements() {
		elementList.Clear();
		elementList.TrimExcess();
	}
	
	public void centerWindow() {
		//glfwSetWindowPos(window, (vidMode.width() - width) / 2, (vidMode.height() - height) / 2); // Show window in center of screen
	}
	
	/**
	 * @return close flag of the window
	 */
	public bool shouldClose() {
		return false;
		//return glfwWindowShouldClose(window);
	}
	
	/**
	 * Swaps the front and back buffers of the window
	 */
	public void swapBuffers() {
		//glfwSwapBuffers(window);
	}
	
	/**
	 * Sets the error callback for the game
	 */
	public static void setCallbacks() {
		//glfwSetErrorCallback(GLFWErrorCallback.createPrint(System.err));
	}

	/**
	 * Sets the key callback to be checked
	 * when glfwPollEvents is called
	 */
	private void setKeyCallback() {
		/*
		// Set key listener
		glfwSetKeyCallback(window, (window, key, scancode, action, mods) => {
			// On Escape, set the window to close
			if (key == GLFW_KEY_ESCAPE && action == GLFW_RELEASE) {
				glfwSetWindowShouldClose(window, true);
			}
				
			if (key == GLFW_KEY_LEFT_CONTROL) {
				if (action == GLFW_PRESS) {
					ctrlHeld = true;
					
					System.out.println("Mouse: " + mouseX + ", " + mouseY);
				} else if (action == GLFW_RELEASE) {
					ctrlHeld = false;
				}
			}
			
			if (key == GLFW_KEY_TAB && action == GLFW_RELEASE) {
				if (GameInstance.demoMode){
					GameInstance.setLevel(GameInstance.scenes.size() - 3);
				}else{
					GameInstance.setLevel(GameInstance.scenes.size() - 2);
				}
			}
			
			if (key == GLFW_KEY_LEFT_SHIFT) {
				if (action == GLFW_PRESS) {
					shiftHeld = true;
				} else if (action == GLFW_RELEASE) {
					shiftHeld = false;
				}
			}
			if (GameInstance.demoMode)
				if (key == GLFW_KEY_LEFT_ALT && action == GLFW_RELEASE) {
					if (GameInstance.levNum != GameInstance.scenes.size() - 2) {
						GameInstance.setLevel(GameInstance.scenes.size() - 2);
					}else{
						GameInstance.setLevel(0);
					}
				}
			
			if (key == GLFW_KEY_SPACE && action == GLFW_RELEASE) {
				State state = GameInstance.getState();
				
				if (state.equals(State.LEVEL_COMPLETE)) {
					GameInstance.setState(State.NEXT_LEVEL);
				}
			}	
		});
		*/
	}
	
	/**
	 * Sets the size callback for the window
	 */
	private void setWindowSizeCallback(int width, int height) { 	//Resize listener
			GL.Viewport(0,0,width, height); 	//Reset the viewport to the correct size
			this.width = width;
			this.height = height;
			this.ratio = (float)width / (float)height;
	}
	
	/**
	 * Sets the mouse callback to be checked
	 * when glfwPollEvents is called
	 */
	private void setMouseButtonCallback() {
		/*
		// Mouse click listener
		glfwSetMouseButtonCallback(window, (window, button, action, mods) => {
			if (button == GLFW_MOUSE_BUTTON_LEFT) { 	//If left mouse button
				if (action == GLFW_RELEASE) { 	//Set a bool variable based on state of mouse (GLFW won't poll mouse state again if already pressed, need to manually store state)
					mouseHeld = false;
				} else if (action == GLFW_PRESS && GameInstance.gameState) {
					mouseHeld = true;
					
					// Iterate over all models in the scene
					for (int j = GameInstance.objectManager.getModels().size()-1; j >= 0; j--) {
						Model b = GameInstance.objectManager.getModel(j);
						
						if (b is MovableModel) {
							MovableModel m = (MovableModel) b;
							if (m.checkClick(mouseX, mouseY)){ 	//If the object is movable and is the one clicked
								new Thread(() => { 	//Start a new thread to move it while the mouse is being held
									while (mouseHeld) {
										try {
											//TODO: The game breaks without some random code before the followCursor call
											//Thread.Sleep(0);
										} catch (Exception e) {}
										if (GameInstance.gameState) { 	
											m.followCursor(mouseX, mouseY);
										} else {
											return;
										}
									}
								}).Start();
								break;
							}
						}
					}
					
					foreach (UIElement e in elementList) {
						if (e is IClickable) {
							((IClickable) e).checkClick(mouseX, mouseY);
						}
					}
					
					GameInstance.getCurrentLevel().checkClick(mouseX, mouseY);
				}
			}
		});
		*/
	}
	
	private void MousePosCallback(float xPos, float yPos) {
			float[] newC = UIUtils.convertToWorldspace((float)xPos, (float)yPos, this.width, this.height);
			this.mouseX = newC[0];
			this.mouseY = newC[1];
	}
	
	/**
	 * @return width of the game window
	 */
	public int getWidth() {
		return width;
	}
	
	/**
	 * @return height of the game window
	 */
	public int getHeight() {
		return width;
	}

	/**
	 * @return true if the window is fullscreen
	 */
	public bool isFullscreen() {
		return fullscreen;
	}
	
	/**
	 * Updates input array and polls GLFW events
	 */
	public void update() {
		//glfwPollEvents();
	}
	
	/**
	 * Renders all UI elements in elementList
	 */
	public void renderElements() {
		foreach (UIElement e in elementList)
			e.render();
	}
	
	/**
	 * Sets the taskbar and window icon for the game
	 */
	private void setWindowIcon() {
		/*
		GLFWImage image = GLFWImage.malloc();
		image.set(32, 32, loadIcon("/icon.png"));
		GLFWImage.Buffer images = GLFWImage.malloc(1);
		images.put(0, image);

		glfwSetWindowIcon(window, images);

		images.free();
		image.free();
		*/
	}
	
	/**
	 * @param path
	 * @return PNG image as a ByteBuffer
	 */
	 /*
	private ByteBuffer loadIcon(string path) {
		PNGDecoder dec = null;
		ByteBuffer buf = null;
		
		try {
		    dec = new PNGDecoder(getClass().getResourceAsStream(path));
		    int width = dec.getWidth();
		    int height = dec.getHeight();
		    buf = BufferUtils.createByteBuffer(width * height * 4);
		    dec.decode(buf, width * 4, PNGDecoder.Format.RGBA);
		    buf.flip();
		} catch (IOException e) {
		    e.printStackTrace();
		}
		
		return buf;
    }
	*/
	
}
