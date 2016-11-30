using LaserAmazer.Math;
using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level10 : Level
    {

        private MovingBox[] movingBoxes;

        public Level10() : base("Level 10")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Inner walls
            CreateModel.CreateWall(-9.7f, 9.4f, .5f, 1f); // Wall to fill in gaps around LaserStop
            CreateModel.CreateWall(-8.3f, 9.4f, .5f, 1f); // Wall to fill in gaps around LaserStop

            CreateModel.CreateWall(-8f, 2f, .25f, 18f);
            CreateModel.CreateWall(2f, -2f, .25f, 16f);
            CreateModel.CreateWall(6f, -4f, .25f, 9f);
            CreateModel.CreateWall(6f, 7.5f, .25f, 8f);

            CreateModel.CreateWall(-3f, 5f, 6f, .25f);
            CreateModel.CreateWall(-3f, 0f, 6f, .25f);

            // Laser start/stop
            LaserStart start = CreateModel.CreateLaserStart(9.3f, 9.3f, 3, -10);
            laserWrappers.Add(start);

            CreateModel.CreateLaserStop(-9f, 9.3f);

            // MovingBoxes
            MovingBox movingBox1 = new MovingBox(2.85f, 2f, 123, 1f, 0f);
            movingBox1.Rotate(45);

            MovingBox movingBox2 = new MovingBox(0f, 2f, 123, 0f, 1f);

            movingBoxes = new MovingBox[] { movingBox1, movingBox2 };

            CreateModel.CreateMovableBox(4.05f, -5.925f);
            CreateModel.CreateMovableTrapezoid(-.9f, 5.8f, 1.5f, 1, 1);
            CreateModel.CreateBox(4f, 0f);

            Model tri1 = CreateModel.CreateTriangle(-4f, -4f, -1f, -2f);
            tri1.Rotate(17);

            CreateModel.CreateMovableTriangle(-1f, 1f, 1f, 1f);

            Model model;
            for (int i = 0; i < 7; i++)
            {
                int x = MathExtension.RandomInt(1, 9);
                int y = MathExtension.RandomInt(2, 8);

                model = CreateModel.CreateMovableTriangle(x, y, 1, 1);
                RandomRotate(model);
            }

            model = CreateModel.CreateMovableTriangle(-6f, -8f, 1f, 1f);
            model.Rotate(75);

            isRendered = true;
        }

        public override void LogicLoop()
        {
            if (isRendered)
            {
                base.LogicLoop();

                foreach (MovingBox mBox in movingBoxes)
                {
                    if (mBox != null)
                        mBox.LogicLoop();
                }
            }
        }

    }
}