using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level5 : Level
    {

        public Level5() : base("Level 5")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Walls
            {
                // Inner bounds
                CreateModel.CreateWall(0f, -5f, 8f, .25f);

                // Around LaserStart
                CreateModel.CreateWall(-1f, -1f, .25f, 3f);
                CreateModel.CreateWall(1f, -1f, .25f, 3f);
                CreateModel.CreateWall(0f, 0.6f, 2.25f, .25f);

                // Around Laser Stop
                CreateModel.CreateWall(-5f, 9f, .25f, 1.5f);
                CreateModel.CreateWall(-3f, 9f, .25f, 1.5f);
                CreateModel.CreateWall(-7.5f, 10f, 4.8f, 3.5f);
                CreateModel.CreateWall(3.5f, 10f, 12.8f, 3.5f);

                // Others
                CreateModel.CreateWall(-4f, 0f, .25f, 5f);
                CreateModel.CreateWall(4f, -2.625f, .25f, 5f);
                CreateModel.CreateWall(-6f, -2.625f, .25f, 5f);
                CreateModel.CreateWall(-4f, 5f, 5f, .25f);
                CreateModel.CreateWall(4f, 5f, 5f, .25f);
            }

            // Laser start/stop
            LaserStart laserStart = CreateModel.CreateLaserStart(0f, 0f, 3);
            laserWrappers.Add(laserStart);
            CreateModel.CreateLaserStop(-4f, 9f);

            // Moveables
            CreateModel.CreateMovableBox(4.05f, -5.925f);
            CreateModel.CreateMovableTrapezoid(-4f, 5f, 1.5f, 1f, 1f);
            CreateModel.CreateMovableTrapezoid(-4f, 8f, 1.5f, 1, 1f);

            Model model = CreateModel.CreateMovableTriangle(4f, 5f, 1f, 1f); ;
            model.Rotate(35);

            model = CreateModel.CreateMovableTriangle(4f, 6f, 1f, 1f); ;
            model.Rotate(90);

            CreateModel.CreateMovableTriangle(4f, 5f, 1f, 1f);
            CreateModel.CreateMovableTriangle(0f, 5f, 1f, 1f);

            // Stationary Models
            model = CreateModel.CreateBox(-8.8f, 7.25f, 2f);
            model = CreateModel.CreateBox(8.8f, 7.25f, 2f);
            model = CreateModel.CreateTriangle(-5.38f, 7.75f, 1f, 1f);
        }

    }
}