using LaserAmazer.Math;
using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level8 : Level
    {

        public Level8() : base("Level 8")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Walls
            {
                // Inner bounds
                CreateModel.CreateWall(-8f, 5f, .25f, 5f);
                CreateModel.CreateWall(-2f, 5f, .25f, 5f);
                CreateModel.CreateWall(-5f, 2.4f, 6.25f, .25f);

                CreateModel.CreateWall(8f, 5f, .25f, 5f);
                CreateModel.CreateWall(2f, 5f, .25f, 5f);
                CreateModel.CreateWall(5f, 2.4f, 6.25f, .25f);

                CreateModel.CreateWall(0f, 0f, 4f, .25f);

                // Around LaserStop
                CreateModel.CreateWall(-1f, -8.5f, .25f, 2.5f);
                CreateModel.CreateWall(1f, -8.5f, .25f, 2.5f);
            }

            // Laser start/stop
            LaserStart laserStart = CreateModel.CreateLaserStart(3f, 9.3f, 3);
            laserWrappers.Add(laserStart);

            LaserStop laserStop = CreateModel.CreateLaserStop(0f, -9.5f);
            laserStop.Rotate(180.1f);

            // Moveables
            Model model;
            for (int i = 0; i < 4; i++)
            {
                int x = MathExtension.RandomInt(-3, 9);
                int y = MathExtension.RandomInt(-3, 8);

                model = CreateModel.CreateMovableBox(x, y);
                RandomRotate(model);
            }

            for (int i = 0; i < 6; i++)
            {
                int x = MathExtension.RandomInt(-3, 9);
                int y = MathExtension.RandomInt(-3, 8);

                model = CreateModel.CreateMovableTriangle(x, y, 1f, 1f);
                RandomRotate(model);
            }

            // Stationary Models
            for (int i = 0; i < 10; i += 2)
            {
                CreateModel.CreateBox(-4f + i, -6f);
            }

            // Box 1
            Model triangle = CreateModel.CreateTriangle(-3.1f, 3.5f, -2f, -2f);
            triangle.Rotate(-90);
            triangle = CreateModel.CreateTriangle(-6.9f, 3.5f, -2f, -2f);
            triangle.Rotate(-180);

            // Box 2
            triangle = CreateModel.CreateTriangle(3.1f, 3.5f, -2f, -2f);
            triangle.Rotate(-180);
            triangle = CreateModel.CreateTriangle(6.9f, 3.5f, -2f, -2f);
            triangle.Rotate(-90);

            // Box 3
            triangle = CreateModel.CreateTriangle(-6.9f, -6f, -2f, -2f);
            triangle.Rotate(-180);
            triangle = CreateModel.CreateTriangle(6.9f, -6f, -2f, -2f);
            triangle.Rotate(-90);

            triangle = CreateModel.CreateTriangle(8.75f, 8.75f, -2f, -2f);
            triangle = CreateModel.CreateTriangle(-8.75f, 8.75f, -2f, -2f);
            triangle.Rotate(90);
        }

    }
}