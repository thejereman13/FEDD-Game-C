using LaserAmazer.Math;
using LaserAmazer.Render;
using System;

namespace LaserAmazer.Level
{
    public class DemoLevel : Level
    {

        private MovingBox movingBox;
        private Model box1, box2, box3, box4;

        public DemoLevel() : base("Demo Level")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            laserWrappers.Add(CreateModel.CreateLaserStart(-10f, 5f, 2, (float)MathExtension.ToRadians(-45)));
            laserWrappers.Add(CreateModel.CreateLaserStart(2f, -10f, 1, (float)MathExtension.ToRadians(38)));

            movingBox = new MovingBox(2, 0, 240, 120, .5f, 0);
            movingBox.Rotate((float)MathExtension.ToRadians(45));
            CreateModel.CreateTriangle(-5.5f, 1f, 2f, 1f);
            box1 = CreateModel.CreateBox(-2, 8, 2);
            box1.Rotate((float)MathExtension.ToRadians(30));
            box2 = CreateModel.CreateBox(0, 5.5f, 1.5f);
            box2.Rotate((float)MathExtension.ToRadians(40));
            box3 = CreateModel.CreateBox(7, -1);
            box4 = CreateModel.CreateBox(-7f, -1);
            isRendered = true;
        }

        public new void LogicLoop()
        {
            if (isRendered)
            {
                base.LogicLoop();
                box3.Rotate((float)MathExtension.ToRadians(.5));
                box4.Rotate((float)MathExtension.ToRadians(.3));
                movingBox.LogicLoop();
            }
        }

    }
}