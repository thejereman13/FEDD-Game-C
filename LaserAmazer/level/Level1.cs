using LaserAmazer.render;

namespace LaserAmazer.level
{
    public class Level1 : Level
    {

        private Model m;

        public Level1() : base("Level 1")
        {
        }

        public override void RenderObjects()
        {
            base.RenderObjects();

            // Inner bounds
            CreateModel.CreateWall(0, 1f, .25f, 18f);

            // Laser start/stop
            laserWrappers.Add(CreateModel.CreateLaserStart(-10f, -1f, 2, -45));
            CreateModel.createLaserStop(7f, 9.9f);

            m = CreateModel.CreateMovableBox(4, 0);
            m.Rotate(-30);

            CreateModel.CreateMovableBox(2, 4);

            m = CreateModel.CreateBox(7, 4, 1); // Stationary box

            isRendered = true;
        }

        public override void LogicLoop()
        {
            if (isRendered)
            {
                base.LogicLoop();

                m.Rotate(1);
            }
        }

    }
}