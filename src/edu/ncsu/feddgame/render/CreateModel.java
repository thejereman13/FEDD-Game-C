package edu.ncsu.feddgame.render;

import org.joml.Vector2d;

import edu.ncsu.feddgame.GameInstance;
import edu.ncsu.feddgame.LaserWrapper;

public class CreateModel {
	
	/**
	 * Creates a Box model and stores it in the objectManager of the GameInstance
	 * @param xOffset
	 * @param yOffset
	 */
	public static Model createBox(float xOffset,float yOffset, float size){
		// Vertices for a quadrilateral
		float[] vertices = new float[] {
			-size/2f + xOffset, size/2f + yOffset, 0, // TOP LEFT - 0
			size/2f + xOffset, size/2f + yOffset, 0, // TOP RIGHT - 1
			size/2f + xOffset, -size/2f + yOffset, 0, // BOTTOM RIGHT - 2
			-size/2f + xOffset, -size/2f + yOffset, 0, // BOTTOM LEFT - 3
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
		return GameInstance.objectManager.addModel(new Model(vertices, texture, indices)); 	//Add the model to the objectManager
	}
	public static Model createBox(float xOffset, float yOffset){
		return createBox(xOffset, yOffset, 1); 	//default size of 1
	}
	
	/**
	 * Creates a trapezoid with the bases on top and bottom, and a vertical height between them
	 * @param xOffset
	 * @param yOffset
	 * @param topBase
	 * @param bottomBase
	 * @param height
	 * @return
	 */
	public static Model createTrapezoid(float xOffset, float yOffset, float topBase, float bottomBase, float height){
		// Vertices for a trapezoid
		float[] vertices = new float[] {
			-topBase/2f + xOffset, height/2f + yOffset, 0, // TOP LEFT - 0
			topBase/2f + xOffset, height/2f + yOffset, 0, // TOP RIGHT - 1
			bottomBase/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM RIGHT - 2
			-bottomBase/2f + xOffset, -height/2f + yOffset, 0, // BOTTOM LEFT - 3
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
		return GameInstance.objectManager.addModel(new Model(vertices, texture, indices)); 	//Add the model to the objectManager
	}
	
	public static Wall createWall(float xOffset, float yOffset, float width, float height){
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
		Wall m = new Wall(vertices, texture, indices);
		GameInstance.objectManager.addModel(m);
		return m;
	}
	
	private static LaserModel createLaser(float begX, float begY, double angle, float length){
		
		float[] texture = new float[] {
			0, 0,
			1, 0,
			1, 1,
			0, 1,
		};
		
		int[] indices = new int[] {
				0, 1, 2,
				2, 3, 0
		};
		
		return new LaserModel(texture, indices, begX, begY, (float)angle, length); 	//Add the model to the objectManager
	}
	
	public static LaserWrapper newLaser(float begX, float begY, double angle, float length){
		return new LaserWrapper(createLaser(begX, begY, angle, length));
	}
	
	/**
	 * Creates a LaserModel object and passes it to the lasers list of the objectManager
	 * This is used for all reflected lasers to avoid calculating redundant reflections on lasers
	 * @param begX
	 * @param begY
	 * @param vect
	 * @return
	 */
	public static LaserModel createReflectedLaser(float begX, float begY, Vector2d vect){
		
		float[] texture = new float[] {
			0, 0,
			1, 0,
			1, 1,
			0, 1,
		};
		
		int[] indices = new int[] {
				0, 1, 2,
				2, 3, 0
		};
		
		return new LaserModel(texture, indices, begX, begY, vect); 	//Return the new Laser
	}
	
	public static LaserStart createLaserStart(float xOffset, float yOffset, int side, double angle){
		float width = 1, height = 1;
		float x = 0, y = 0;
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
			switch (side){
			case 0:
				x = xOffset - width/2f;
				y = yOffset;
				break;
			case 1:
				x = xOffset;
				y = yOffset + height/2f;
				break;
			case 2:
				x = xOffset + width/2f;
				y = yOffset;
			case 3:
				x = xOffset;
				y = yOffset - height/2f;
				break;
			}
		LaserStart l = new LaserStart(vertices, texture, indices, newLaser(x, y, angle, 1));
		GameInstance.objectManager.addModel(l);
		return l;
	}
	
	public static LaserStop createLaserStop(float xOffset, float yOffset){
		int size = 1;
		// Vertices for a quadrilateral
		float[] vertices = new float[] {
			-size/2f + xOffset, size/2f + yOffset, 0, // TOP LEFT - 0
			size/2f + xOffset, size/2f + yOffset, 0, // TOP RIGHT - 1
			size/2f + xOffset, -size/2f + yOffset, 0, // BOTTOM RIGHT - 2
			-size/2f + xOffset, -size/2f + yOffset, 0, // BOTTOM LEFT - 3
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
		LaserStop l = new LaserStop(vertices, texture, indices);
		GameInstance.objectManager.addModel(l);
		return l;
				
	}

}