
public class Level9 : Level {

	private Model spinBox = null;
	private MovingBox[] movingBoxes;
	
	public Level9() : base("Level 9") {
	}
	
	public override void renderObjects() {
		base.renderObjects();
		
		// Walls
		{		
			// Vertical walls between moving boxes
			for (int i = -8; i <= 8; i += 2) {
				CreateModel.createWall(i, -3f, .25f, 5f);
				CreateModel.createWall(i, 3f, .25f, 5f);
				
				if (i == -8 || i == 3)
					i += 3;
			}
			
			// Around LaserStop
			CreateModel.createWall(-1f, -8.5f, .25f, 2.5f);
			CreateModel.createWall(1f, -8.5f, .25f, 2.5f);
		}
		
		// Laser start/stop
		LaserStart laserStart = CreateModel.createLaserStart(-9.3f, 0f, 3);
		laserStart.rotate(90);
		laserWrappers.Add(laserStart);
		
		LaserStop laserStop = CreateModel.createLaserStop(0f, -9.5f);
		laserStop.rotate(180);
		
		// Moveables
		Model model;
		for (int i = 0; i < 6; i++) {
			int x = randomInt(0, 9);
			int y = randomInt(0, 8);
			
			model = CreateModel.createMovableTriangle(x, y, 1f, 1f);
			randomRotate(model);
		}
		
		model = CreateModel.createMovableBox(0f, 7f);
		model.rotate(25);
		
		model = CreateModel.createMovableBox(3f, 7f);
		model.rotate(165);
		
		model = CreateModel.createMovableBox(6f, 7f);
		model.rotate(243);
		
		// Unmoveable Models
		float velocity = 0.7f;
		int movingBoxCount = 5;
		MovingBox movingBox;
		movingBoxes = new MovingBox[movingBoxCount];
		for (int i = 0; i < movingBoxCount; i++) {
			velocity = (float) randomInt(0, 1) + 0.5f;
			movingBox = new MovingBox(-4f + 2f * i, 0f, 40 + (i * 3), 0f, i % 2 == 0 ? velocity : -velocity);
			movingBox.rotate(45);
			movingBoxes[i] = movingBox;
		}
		
		spinBox = CreateModel.createBox(-4f, -7f, 1.4f);
		
		Model[] triangles = {
				CreateModel.createTriangle(-0.88f, -2f, -1f, -1f),
				CreateModel.createTriangle(0.88f, -3f, -1.3f, -1.3f),
				CreateModel.createTriangle(-0.88f, -4f, -1f, -1f)
		};

		for (int i = 0; i < triangles.Length; i++) {
			if (i % 2 == 0) {
				triangles[i].rotate(-45);
			} else {
				triangles[i].rotate(135);
			}
		}
		
		model = CreateModel.createBox(0f, -6.5f, 1f);
		model.rotate(65);
		
		isRendered = true;
	}


	public override void logicLoop() {
		if (isRendered) {
			base.logicLoop();
			
			foreach (MovingBox mBox in movingBoxes) {
				if (mBox != null)
					mBox.logicLoop();
			}
			
			spinBox.rotate(1);
		}	
	}

}
