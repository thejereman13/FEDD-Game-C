package edu.ncsu.feddgame;

import static org.lwjgl.glfw.GLFW.GLFW_RESIZABLE;
import static org.lwjgl.glfw.GLFW.GLFW_TRUE;
import static org.lwjgl.glfw.GLFW.GLFW_VISIBLE;
import static org.lwjgl.glfw.GLFW.glfwInit;
import static org.lwjgl.glfw.GLFW.glfwWindowHint;
import static org.lwjgl.opengl.GL11.GL_COLOR_BUFFER_BIT;
import static org.lwjgl.opengl.GL11.GL_TEXTURE_2D;
import static org.lwjgl.opengl.GL11.glClear;
import static org.lwjgl.opengl.GL11.glEnable;

import org.joml.Matrix4f;
import org.joml.Vector3f;
import org.lwjgl.opengl.GL;

import edu.ncsu.feddgame.level.ILevel;
import edu.ncsu.feddgame.level.TestLevel;
import edu.ncsu.feddgame.render.Camera;
import edu.ncsu.feddgame.render.FloatColor;
import edu.ncsu.feddgame.render.Font;
import edu.ncsu.feddgame.render.Shader;
import edu.ncsu.feddgame.render.Texture;

public class GameInstance {
	
	public static Window window;
	public static ObjectManager objectManager;
	boolean canRender;
	public ILevel level;
	public static Shader shader;
	
	private static State state;
	private volatile boolean gameState; // Boolean value to pause logic Thread when state != GAME
	
	// Game begins here
	public GameInstance() {
		try { 	// Run and quit on error
			setup();
			renderLoop();
		} finally {
			//glfwTerminate();
			//System.exit(1);
		}
	}
	
	private void setup() { 	// Setup all the window settings
		//Window.setCallbacks();
		
		objectManager = new ObjectManager();
		
		if (!glfwInit()) 	//Throw exception if glfw fails to initialize
			throw new IllegalStateException("Can not initialize GLFW");

		glfwWindowHint(GLFW_RESIZABLE, GLFW_TRUE); 	// Set window resizable and visible (set at defaults right now)
		glfwWindowHint(GLFW_VISIBLE, GLFW_TRUE);
		
		window = new Window(800, 800, "Laser Amazer", false);
		
		state = State.MAIN_MENU; // Set the game state
		
		level = new TestLevel();
	}
	
	private void renderLoop() { 	// Render Loop
		GL.createCapabilities();
		
		//TODO Should probably throw exception and exit here if window is null
		
		//TODO: Move this to it's own class, and have Fonts instantiated with location info
		// Set up main menu text
		Font menuTitle = new Font("Laser Amazer", new FloatColor(255, 25, 0));
		Font menuItem = new Font("Start Game", new FloatColor(25, 255, 0));
		Font startGame = new Font("Press Space to Start Game", new FloatColor(0, 25, 255));
		
		Camera camera = new Camera(window.getWidth(), window.getHeight());
		
		glEnable(GL_TEXTURE_2D);
		
		level.renderObjects(); 	//Add all objects to the scene from the level class
		objectManager.updateModels();
		window.addElements();
		
		shader = new Shader("shader");
		Texture tex = new Texture("bound.png");
		
		Matrix4f scale = new Matrix4f()
				.translate(new Vector3f(100, 0, 0))
				.scale(40);
		Matrix4f target = new Matrix4f();
		
		camera.setPosition(new Vector3f(-100, 0, 0));
		
		double frameCap = 1.0 / 60; // 60 FPS
		
		double frameTime = 0;
		@SuppressWarnings("unused")
		int frames = 0;
		
		double time = getTime();
		double unprocessed = 0;
		
		new Thread(() -> logicLoop()).start(); 	//Run the logic in a separate thread
		
		while (!window.shouldClose()) { 	// Poll window while window isn't about to close
			canRender = false;
			
			// Control frames per second
			{
				double timeNow = getTime();
				double elapsed = timeNow - time;
				unprocessed += elapsed;
				frameTime += elapsed;
				time = timeNow;
			}
			
			// Run all non-render related tasks
			while (unprocessed >= frameCap) {
				unprocessed -= frameCap;
				canRender = true;
				target = scale;
				
				window.update();
				
				if (frameTime >= 1.0) {
					frameTime = 0;
					// Display FPS counter in console
					//System.out.println("FPS: " + frames);
					frames = 0;
				}
			}
			
			// Render when scene changes
			if (canRender) {
				glClear(GL_COLOR_BUFFER_BIT);
				//glClearColor(1, 1, 1, 1);
				
				if (state.equals(State.CREDITS)) {
					gameState = false;
				} else if (state.equals(State.GAME)) {
					gameState = true;
					shader.bind();
					shader.setUniform("sampler", 0);
					shader.setUniform("projection", camera.getProjection().mul(target));
					tex.bind(0);
					objectManager.renderAll();
					level.renderLoop();
					window.renderElements();
				} else if (state.equals(State.MAIN_MENU)) {
					gameState = false;
					shader.unbind();
					menuTitle.renderString(menuTitle.getRenderString(), 16, -0.82f, 0.3f, 0.4f);
					menuItem.renderString("> Start Game", 16, -0.64f, 0.1f, 0.3f);
					menuItem.renderString("> How to Play", 16, -0.64f, 0.02f, 0.3f);
					menuItem.renderString("> Credits", 16, -0.64f, -0.06f, 0.3f);
					startGame.renderString("(Press Space.)", 16, -0.72f, -0.45f, 0.3f);
				}
				
				window.swapBuffers(); // Swap the render buffers
				frames++;
			}
			
		}
	}
	
	private void logicLoop() {	
		long timing = Math.round(1f / 60 * 1000f); 	//Get the number of milliseconds between frames based on 60 times a second
		
		while (!window.shouldClose()){
			if (!gameState) continue;
			
			double timeNow = getTime(); 	//Get time at the start of the loop
			
			level.logicLoop(); 	//Run the logic necessary for the level
			
			{
				long sleeptime = timing - (long)(getTime() - timeNow); 	//Sync the game loop to update at the refresh rate
				//System.out.println(sleeptime);
				try {
					Thread.sleep(sleeptime);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}	
				
		}
	}
	
	/**
	 * @return System time in seconds
	 */
	public double getTime() {
		return (double) System.nanoTime() / (double) 1000000000L;
	}
	
	public static void setState(State s) {
		state = s;
	}
	
	public static State getState() {
		return state;
	}
	
}
