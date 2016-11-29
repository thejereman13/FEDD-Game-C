
public class Level1 : Level {
	
	private Model m;
	
	public Level1() : base("Level 1") {
	}

	public override void renderObjects() {
		base.renderObjects();
		
		// Inner bounds
		CreateModel.createWall(0, 1f, .25f, 18f);
		
		// Laser start/stop
		laserWrappers.Add(CreateModel.createLaserStart(-10f, -1f, 2, -45));
		CreateModel.createLaserStop(7f, 9.9f);
		
		m = CreateModel.createMovableBox(4, 0);
		m.rotate(-30);
		
		CreateModel.createMovableBox(2, 4);
		
		m = CreateModel.createBox(7, 4, 1); // Stationary box
		
		isRendered = true;
	}
	
	public override void logicLoop() {
		if (isRendered) {
			base.logicLoop();
			
			m.rotate(1);
		}
	}

}
