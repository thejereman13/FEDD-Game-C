using LaserAmazer.Math;
using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level2 : Level
    {

        public Level2() : base("Level 2")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Inner bounds
            CreateModel.CreateWall(-5f, 2f, .25f, 18f);
            CreateModel.CreateWall(2f, -2f, .25f, 16f);

            // Laser start/stop
            laserWrappers.Add(CreateModel.CreateLaserStart(-10f, -1f, 2, -45));
            Model model = CreateModel.CreateLaserStop(10, 7);
            model.Rotate(-90);

            // Moveables
            for (int i = 0; i < 7; i++)
            {
                int x = MathExtension.RandomInt(-3, 9);
                int y = MathExtension.RandomInt(-3, 8);

                model = CreateModel.CreateMovableBox(x, y);
                RandomRotate(model);
            }

            CreateModel.CreateMovableTriangle(-1f, 1f, 1f, 1f);

            // Unmoveables
            CreateModel.CreateTriangle(2f, 7f, 1f, 1f);
        }

    }
}