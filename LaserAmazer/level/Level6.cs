using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level6 : Level
    {

        public Level6() : base("Level 6")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Inner bounds
            CreateModel.CreateWall(0f, -5f, 8f, .25f);
            CreateModel.CreateWall(-1f, -9f, .25f, 1.5f);
            CreateModel.CreateWall(1f, -9f, .25f, 1.5f);

            // Laser start/stop
            LaserStart laserStart = CreateModel.CreateLaserStart(-9f, 9f, 3);
            laserStart.Rotate(45);
            laserWrappers.Add(laserStart);

            LaserStop laserStop = CreateModel.CreateLaserStop(0f, -9.5f);
            laserStop.Rotate(180.1f);

            // Moveables
            CreateModel.CreateMovableBox(4.05f, -5.925f);
            CreateModel.CreateMovableTrapezoid(-4f, 5f, 1.5f, 1f, 1f);
            CreateModel.CreateMovableTrapezoid(-4f, 8f, 1.5f, 1, 1f);

            CreateModel.CreateMovableTriangle(4f, 5f, 1f, 1f);
            CreateModel.CreateMovableTriangle(0f, 5f, 1f, 1f);

            // Stationary Models
            Model box;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0 + i; j < 20 - i * 2; j += 2)
                {
                    box = CreateModel.CreateBox(-9f + j, i * 2);
                    box.Rotate(45);
                }
            }
        }

    }
}