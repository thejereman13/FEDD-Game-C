
using System;

public class DemoLevel : Level{

	private MovingBox movingBox;
	private Model box1, box2, box3, box4;
	
	public DemoLevel() : base("Demo Level"){
	}
	
	public override void renderObjects(){
		base.renderObjects();
		
		laserWrappers.Add(CreateModel.createLaserStart(-10f, 5f, 2, -45));
		laserWrappers.Add(CreateModel.createLaserStart(2f, -10f, 1, 38));
		
		movingBox = new MovingBox(2, 0, 240, 120, .5f, 0);
		movingBox.rotate(45);
		CreateModel.createTriangle(-5.5f, 1f, 2f, 1f);
		box1 = CreateModel.createBox(-2, 8, 2);
		box1.rotate(30);
		box2 = CreateModel.createBox(0, 5.5f,1.5f);
		box2.rotate(40);
		box3 = CreateModel.createBox(7, -1);
		box4 = CreateModel.createBox(-7f, -1);
		isRendered = true;
	}

	public new void logicLoop() {
		if (isRendered) {
			base.logicLoop();
			box3.rotate(.5f);
			box4.rotate(.3f);
			movingBox.logicLoop();
		}
	}

}
