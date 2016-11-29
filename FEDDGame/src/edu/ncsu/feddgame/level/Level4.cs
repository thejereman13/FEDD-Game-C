
public class Level4 : Level {

	public Level4() : base("Level 4"){
	}
	
	public override void renderObjects() {
		base.renderObjects();
		
		// Inner bounds
		CreateModel.createWall(-6.3f, -2f, 8f, .25f);
		
		// Laser start/stop
		laserWrappers.Add(CreateModel.createLaserStart(0f, 9f, 3));
		LaserStop laserStop = CreateModel.createLaserStop(0f, -9f);
		laserStop.rotate(180.1f);
		
		// Moveables
		CreateModel.createMovableBox(4.05f, -5.925f);
		CreateModel.createMovableTrapezoid(-4f, 5f, 1.5f, 1, 1);
		
		CreateModel.createMovableTriangle(4f, 1f, 3f, 1.4f);
		CreateModel.createMovableTriangle(0f, 2f, 1f, 1f);
		
		// Stationary Models
		CreateModel.createBox(-4f, 2f);
		
		Model model = CreateModel.createBox(0f, 0f);
		model.rotate(45);
		
		model = CreateModel.createTriangle(-4f, -4f, -1f, -2f);
		model.rotate(17);
	}

}
