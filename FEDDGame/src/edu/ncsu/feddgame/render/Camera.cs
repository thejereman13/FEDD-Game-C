using OpenTK;

/**
 * Enables the in-game view to be  moved around.
 */

public class Camera {

	private Vector3d position;
	private Matrix4d projection;
	
	public Camera(int width, int height) {
		position = new Vector3d(0, 0, 0);
		//projection = new Matrix4d().setOrtho2D(-width / 2, width / 2, -height / 2, height / 2);
	}
	
	public void setPosition(Vector3d position) {
		this.position = position;
	}
	
	public Vector3d getPosition() {
		return position;
	}
	
	public Matrix4d getProjection() {
		return new Matrix4d();
		//return projection.translate(position, new Matrix4d());
	}
	
}
