
public class Level3 : Level {

	private MovingBox movingBox;
	
	public Level3() : base("Level 3"){
	}

	public override void renderObjects() {
		base.renderObjects();
		
		// Inner bounds
		CreateModel.createWall(-4f, 2f, 12f, .25f);
		CreateModel.createWall(2f, -1.37f, .25f, 7f);
		CreateModel.createWall(-7.7f, 6f, 5f, .25f);
		
		// Unmoveables
		CreateModel.createBox(-4f, 5f, 1f);
		
		laserWrappers.Add(CreateModel.createLaserStart(-10f, -1f, 2, -45));
		
		Model laserStop = CreateModel.createLaserStop(-9.7f, 7f);
		laserStop.rotate(90);

		movingBox = new MovingBox(2.85f, 2.6f, 123, 0f, 1f);
		
		Model model;
		for (int i = 0; i < 7; i++) {
			int x = randomInt(1, 9);
			int y = randomInt(2, 8);
			
			model = CreateModel.createMovableBox(x, y);
			randomRotate(model);
		}
		
		isRendered = true;
	}
	
	public override void logicLoop() {
		if (isRendered) {
			base.logicLoop();
			
			movingBox.logicLoop();
		}
	}

}
