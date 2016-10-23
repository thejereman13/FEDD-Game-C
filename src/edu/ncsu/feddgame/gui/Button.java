package edu.ncsu.feddgame.gui;

import java.util.ArrayList;

import edu.ncsu.feddgame.render.FloatColor;
import edu.ncsu.feddgame.render.Font;
import edu.ncsu.feddgame.render.Model;

public class Button extends Model implements UIElement, IClickable{
	
	ArrayList<Runnable> callbacks = new ArrayList<Runnable>();
	protected float [] xCoords, yCoords;
	Font label;
	public float xOffset, yOffset, height, width;
	/**
	 * New Button extension from Model with Runnable object for execution on click events
	 * @param coords
	 * @param tCoords
	 * @param indices
	 * @param r
	 */
	public Button(float[] coords, float[] tCoords, int[] indices, Runnable r, float xOffset, float yOffset, float height, float width){
		super(coords, tCoords, indices, 4, "bgtile.png");
		callbacks.add(r);
		this.xOffset = xOffset;
		this.yOffset = yOffset;
		this.height = height;
		this.width = width;
		label = new Font("", new FloatColor(0, 0, 0));
		xCoords = new float[]{coords[0], coords[3], coords[6], coords[9]};
		yCoords = new float[]{coords[1], coords[4], coords[7], coords[10]};
	}
	
	public Button(float[] coords, float[] tCoords, int[] indices, Runnable r, Font f, float xOffset, float yOffset, float height, float width){
		super(coords, tCoords, indices, 4, "bgtile.png");
		callbacks.add(r);
		this.xOffset = xOffset;
		this.yOffset = yOffset;
		this.height = height;
		this.width = width;
		this.label = f;
		xCoords = new float[]{coords[0], coords[3], coords[6], coords[9]};
		yCoords = new float[]{coords[1], coords[4], coords[7], coords[10]};
	}

	public void addCallback(Runnable r){
		callbacks.add(r);
	}
	/**
	 * Returns a callback runnable previously declared
	 * @return
	 */
	public Runnable getCallback(int index){
		return callbacks.get(index);
	}
	@Override
	/**
	 * Returns whether the button was clicked based on mouse coordinates passed
	 * @param xPos
	 * @param yPos
	 * @return
	 */
	public Boolean checkClick(float xPos, float yPos) {
		if (UIUtils.pnpoly(xCoords, yCoords, xPos, yPos)){
			if (callbacks != null)
				for(Runnable r : callbacks){
					r.run();
				}
			return true;
		}else{
			return false;
		}
	}
	
	@Override
	public void render(){
		super.render();
		float w = width /(float)label.getRenderString().length() / 3.5f;
		label.renderString(label.getRenderString(), 16, xCoords[0] / 10, (yOffset - w * 2f) / 20, w);
	}
	
	@Override
	public void move(float x, float y, float z){
		super.move(x, y, z);
		yOffset += y; 	//Manually set the new coordinates after move, needed for font movement
		xCoords = new float[]{super.vertices[0], super.vertices[3], super.vertices[6], super.vertices[9]};
		yCoords = new float[]{super.vertices[1], super.vertices[4], super.vertices[7], super.vertices[10]};
	}

	@Override
	public void setCallback(Runnable r) {
		// TODO Auto-generated method stub
		
	}

}
