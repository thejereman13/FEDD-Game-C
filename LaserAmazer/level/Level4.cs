using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level4 : Level
    {

        public Level4() : base("Level 4")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Inner bounds
            CreateModel.CreateWall(-6.3f, -2f, 8f, .25f);

            // Laser start/stop
            laserWrappers.Add(CreateModel.CreateLaserStart(0f, 9f, 3));
            LaserStop laserStop = CreateModel.CreateLaserStop(0f, -9f);
            laserStop.Rotate(180.1f);

            // Moveables
            CreateModel.CreateMovableBox(4.05f, -5.925f);
            CreateModel.CreateMovableTrapezoid(-4f, 5f, 1.5f, 1, 1);

            CreateModel.CreateMovableTriangle(4f, 1f, 3f, 1.4f);
            CreateModel.CreateMovableTriangle(0f, 2f, 1f, 1f);

            // Stationary Models
            CreateModel.CreateBox(-4f, 2f);

            Model model = CreateModel.CreateBox(0f, 0f);
            model.Rotate(45);

            model = CreateModel.CreateTriangle(-4f, -4f, -1f, -2f);
            model.Rotate(17);
        }

    }
}