
using System;

public class Level10 : Level {

	private MovingBox[] movingBoxes;
	
	public Level10() : base("Level 10"){
	}
	
	public override void renderObjects() {
		base.renderObjects();
		
		// Inner walls
		CreateModel.createWall(-9.7f, 9.4f, .5f, 1f); // Wall to fill in gaps around LaserStop
		CreateModel.createWall(-8.3f, 9.4f, .5f, 1f); // Wall to fill in gaps around LaserStop
		
		CreateModel.createWall(-8f, 2f, .25f, 18f);
		CreateModel.createWall(2f, -2f, .25f, 16f);
		CreateModel.createWall(6f, -4f, .25f, 9f);
		CreateModel.createWall(6f, 7.5f, .25f, 8f);
		
		CreateModel.createWall(-3f, 5f, 6f, .25f);
		CreateModel.createWall(-3f, 0f, 6f, .25f);
		
		// Laser start/stop
		LaserStart start = CreateModel.createLaserStart(9.3f, 9.3f, 3, -10);
		laserWrappers.Add(start);
		
		CreateModel.createLaserStop(-9f, 9.3f);
		
		// MovingBoxes
		MovingBox movingBox1 = new MovingBox(2.85f, 2f, 123, 1f, 0f);
		movingBox1.rotate(45);
		
		MovingBox movingBox2 = new MovingBox(0f, 2f, 123, 0f, 1f);
		
		movingBoxes = new MovingBox[] { movingBox1, movingBox2 };
		
		CreateModel.createMovableBox(4.05f, -5.925f);
		CreateModel.createMovableTrapezoid(-.9f, 5.8f, 1.5f, 1, 1);
		CreateModel.createBox(4f, 0f);
		
		Model tri1 = CreateModel.createTriangle(-4f, -4f, -1f, -2f);
		tri1.rotate(17);
		
		CreateModel.createMovableTriangle(-1f, 1f, 1f, 1f);
		
		Model model;
		for (int i = 0; i < 7; i++) {
			int x = randomInt(1, 9);
			int y = randomInt(2, 8);
			
			model = CreateModel.createMovableTriangle(x, y, 1, 1);
			randomRotate(model);
		}
		
		model = CreateModel.createMovableTriangle(-6f, -8f, 1f, 1f);
		model.rotate(75);
		
		isRendered = true;
	}
	
	public override void logicLoop() {
		if (isRendered) {
			base.logicLoop();
			
			foreach (MovingBox mBox in movingBoxes) {
				if (mBox != null)
					mBox.logicLoop();
			}
		}
	}

}
