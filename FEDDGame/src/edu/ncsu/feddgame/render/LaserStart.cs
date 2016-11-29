
public class LaserStart : Model {
	
	private LaserWrapper laser;
	
	public LaserStart(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, LaserWrapper laser) : base(vertices, tCoords, indices, xOffset, yOffset, 4, GameTexture.START){
		this.laser = laser;
	}
	
	public new void render() {
		base.render();
		laser.render();
	}
	
	public void reflect() {
		laser.runReflections();
	}
	
	public void setLaser(LaserWrapper l) {
		this.laser = l;
	}
	
	public new void rotate(float angle) {
		base.rotate(angle);
		
		if (laser != null) 
			laser.rotateStart(angle, base.xOffset, base.yOffset);
	}

}