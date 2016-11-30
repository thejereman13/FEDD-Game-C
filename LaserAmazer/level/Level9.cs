using LaserAmazer.Math;
using LaserAmazer.Render;

namespace LaserAmazer.Level
{
    public class Level9 : Level
    {

        private Model spinBox = null;
        private MovingBox[] movingBoxes;

        public Level9() : base("Level 9")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Walls
            {
                // Vertical walls between moving boxes
                for (int i = -8; i <= 8; i += 2)
                {
                    CreateModel.CreateWall(i, -3f, .25f, 5f);
                    CreateModel.CreateWall(i, 3f, .25f, 5f);

                    if (i == -8 || i == 3)
                        i += 3;
                }

                // Around LaserStop
                CreateModel.CreateWall(-1f, -8.5f, .25f, 2.5f);
                CreateModel.CreateWall(1f, -8.5f, .25f, 2.5f);
            }

            // Laser start/stop
            LaserStart laserStart = CreateModel.CreateLaserStart(-9.3f, 0f, 3);
            laserStart.Rotate(90);
            laserWrappers.Add(laserStart);

            LaserStop laserStop = CreateModel.CreateLaserStop(0f, -9.5f);
            laserStop.Rotate(180);

            // Moveables
            Model model;
            for (int i = 0; i < 6; i++)
            {
                int x = MathExtension.RandomInt(0, 9);
                int y = MathExtension.RandomInt(0, 8);

                model = CreateModel.CreateMovableTriangle(x, y, 1f, 1f);
                RandomRotate(model);
            }

            model = CreateModel.CreateMovableBox(0f, 7f);
            model.Rotate(25);

            model = CreateModel.CreateMovableBox(3f, 7f);
            model.Rotate(165);

            model = CreateModel.CreateMovableBox(6f, 7f);
            model.Rotate(243);

            // Unmoveable Models
            float velocity = 0.7f;
            int movingBoxCount = 5;
            MovingBox movingBox;
            movingBoxes = new MovingBox[movingBoxCount];
            for (int i = 0; i < movingBoxCount; i++)
            {
                velocity = (float)MathExtension.RandomInt(0, 1) + 0.5f;
                movingBox = new MovingBox(-4f + 2f * i, 0f, 40 + (i * 3), 0f, i % 2 == 0 ? velocity : -velocity);
                movingBox.Rotate(45);
                movingBoxes[i] = movingBox;
            }

            spinBox = CreateModel.CreateBox(-4f, -7f, 1.4f);

            Model[] triangles = {
                CreateModel.CreateTriangle(-0.88f, -2f, -1f, -1f),
                CreateModel.CreateTriangle(0.88f, -3f, -1.3f, -1.3f),
                CreateModel.CreateTriangle(-0.88f, -4f, -1f, -1f)
        };

            for (int i = 0; i < triangles.Length; i++)
            {
                if (i % 2 == 0)
                {
                    triangles[i].Rotate(-45);
                }
                else
                {
                    triangles[i].Rotate(135);
                }
            }

            model = CreateModel.CreateBox(0f, -6.5f, 1f);
            model.Rotate(65);

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

                spinBox.Rotate(1);
            }
        }

    }
}