package edu.ncsu.feddgame.gui;

import edu.ncsu.feddgame.render.Font;

public class CreateUI {
	
	/**
	 * Creates a new button object and adds it to the window view
	 * @param xOffset
	 * @param yOffset
	 * @param height
	 * @param width
	 * @param r
	 */
	public static Button createButton(float xOffset, float yOffset, float width, float height, Runnable r, Font f){
		// Vertices for a trapezoid
		float[] vertices = new float[] {
			-width/2f + xOffset, height/2f + yOffset, 0, // TOP LEFT - 0
			width/2f + xOffset, height/2f + yOffset, 0, // TOP RIGHT - 1
			width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM RIGHT - 2
			-width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM LEFT - 3
		};
		
		float[] texture = new float[] {
			0, 0, // TOP LEFT
			1, 0, // TOP RIGHT
			1, 1, // BOTTOM RIGHT
			0, 1, // BOTTOM LEFT
		};
		
		int[] indices = new int[] {
				0, 1, 2,
				2, 3, 0
		};
		return new Button(vertices, texture, indices, r, f, xOffset, yOffset, height, width);
	}
	
	public static Button createButton(float xOffset, float yOffset, float width, float height, Runnable r){
		// Vertices for a trapezoid
				float[] vertices = new float[] {
					-width/2f + xOffset, height/2f + yOffset, 0, // TOP LEFT - 0
					width/2f + xOffset, height/2f + yOffset, 0, // TOP RIGHT - 1
					width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM RIGHT - 2
					-width/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM LEFT - 3
				};
				
				float[] texture = new float[] {
					0, 0, // TOP LEFT
					1, 0, // TOP RIGHT
					1, 1, // BOTTOM RIGHT
					0, 1, // BOTTOM LEFT
				};
				
				int[] indices = new int[] {
						0, 1, 2,
						2, 3, 0
				};
				return new Button(vertices, texture, indices, r, xOffset, yOffset, height, width);
	}

}
