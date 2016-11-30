using LaserAmazer.Math;
using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level7 : Level
    {

        private MovingBox movingBox;

        public Level7() : base("Level 7")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Inner bounds
            CreateModel.CreateWall(0f, -5f, 8f, .25f);
            CreateModel.CreateWall(5f, -9f, .25f, 1.5f);
            CreateModel.CreateWall(7f, -8.5f, .25f, 2.5f);

            CreateModel.CreateWall(0f, 7f, 8f, .25f);
            CreateModel.CreateWall(4f, 4f, 5f, .25f);
            CreateModel.CreateWall(-4f, 4f, 5f, .25f);

            CreateModel.CreateWall(5.8f, 0f, 8f, .25f);
            CreateModel.CreateWall(-5.8f, -2f, 8f, .25f);

            // Laser start/stop
            LaserStart laserStart = CreateModel.CreateLaserStart(6f, 9f, 3);
            laserStart.Rotate(-35);
            laserWrappers.Add(laserStart);

            LaserStop laserStop = CreateModel.CreateLaserStop(6f, -9.5f);
            laserStop.Rotate(180.1f);

            // Moveables
            CreateModel.CreateMovableBox(4.05f, -5.925f);
            CreateModel.CreateMovableTrapezoid(-4f, 5f, 1.5f, 1f, 1f);

            CreateModel.CreateMovableTriangle(4f, 5f, 1f, 1f);
            CreateModel.CreateMovableTriangle(0f, 5f, 1f, 1f);

            Model model;
            for (int i = 0; i < 3; i++)
            {
                int x = MathExtension.RandomInt(-3, 9);
                int y = MathExtension.RandomInt(-3, 8);

                model = CreateModel.CreateMovableBox(x, y);
                RandomRotate(model);
            }

            // Moving Models
            movingBox = new MovingBox(-5f, 3f, 180, 0.5f, -0.5f);

            // Stationary Models
            Model triangle = CreateModel.CreateTriangle(3.9f, -9f, 1.5f, 2f);
            triangle.Rotate(-90);

            triangle = CreateModel.CreateTriangle(8.1f, -9f, 2f, 1.5f);
            triangle.Rotate(180);

            triangle = CreateModel.CreateTriangle(9.25f, -9.25f, 1f, 1f);
            triangle.Rotate(-90);

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