using LaserAmazer.Math;
using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level3 : Level
    {

        private MovingBox movingBox;

        public Level3() : base("Level 3")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Inner bounds
            CreateModel.CreateWall(-4f, 2f, 12f, .25f);
            CreateModel.CreateWall(2f, -1.37f, .25f, 7f);
            CreateModel.CreateWall(-7.7f, 6f, 5f, .25f);

            // Unmoveables
            CreateModel.CreateBox(-4f, 5f, 1f);

            laserWrappers.Add(CreateModel.CreateLaserStart(-10f, -1f, 2, (float)MathExtension.ToRadians(-45)));

            Model laserStop = CreateModel.CreateLaserStop(-9.7f, 7f);
            laserStop.Rotate((float)MathExtension.ToRadians(90));

            movingBox = new MovingBox(2.85f, 2.6f, 123, 0f, 1f);

            Model model;
            for (int i = 0; i < 7; i++)
            {
                int x = MathExtension.RandomInt(1, 9);
                int y = MathExtension.RandomInt(2, 8);

                model = CreateModel.CreateMovableBox(x, y);
                RandomRotate(model);
            }

            isRendered = true;
        }

        public override void LogicLoop()
        {
            if (isRendered)
            {
                base.LogicLoop();

                movingBox.LogicLoop();
            }
        }

    }
}