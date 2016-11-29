
public class Level6 : Level {

	public Level6() : base("Level 6") {
	}

	public override void renderObjects() {
		base.renderObjects();
		
		// Inner bounds
		CreateModel.createWall(0f, -5f, 8f, .25f);
		CreateModel.createWall(-1f, -9f, .25f, 1.5f);
		CreateModel.createWall(1f, -9f, .25f, 1.5f);
		
		// Laser start/stop
		LaserStart laserStart = CreateModel.createLaserStart(-9f, 9f, 3);
		laserStart.rotate(45);
		laserWrappers.Add(laserStart);
		
		LaserStop laserStop = CreateModel.createLaserStop(0f, -9.5f);
		laserStop.rotate(180.1f);
		
		// Moveables
		CreateModel.createMovableBox(4.05f, -5.925f);
		CreateModel.createMovableTrapezoid(-4f, 5f, 1.5f, 1f, 1f);
		CreateModel.createMovableTrapezoid(-4f, 8f, 1.5f, 1, 1f);
		
		CreateModel.createMovableTriangle(4f, 5f, 1f, 1f);
		CreateModel.createMovableTriangle(0f, 5f, 1f, 1f);
		
		// Stationary Models
		Model box;
		for (int i = 0; i < 2; i++) {
			for (int j = 0 + i; j < 20 - i * 2; j += 2) {
				box = CreateModel.createBox(-9f + j, i * 2);
				box.rotate(45);
			}
		}
	}

}
