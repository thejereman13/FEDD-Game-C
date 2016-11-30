using System;
using System.Collections.Generic;

namespace LaserAmazer.level
{
    public abstract class Level : Scene
    {

        protected List<LaserStart> laserWrappers = new List<LaserStart>();
        protected bool isRendered = false;
        Random random = new Random();

        public Level(string name) : base(name)
        {
        }

        protected int randomInt(int min, int max)
        {
            return random.Next() * (max - min + 1) + min;

        }

        public override void logicLoop()
        {
            if (laserWrappers != null)
            {
                for (int i = 0; i < laserWrappers.Count; i++)
                {
                    laserWrappers[i].reflect();
                }
            }
        }

        public override void renderObjects()
        {
            laserWrappers.Clear();
            elementList.Clear();
            timeStart = getTime();
            Dropdown du = CreateUI.createDropdown(-12f, 8f, 3f, 1f, new GameFont("Select Level", GameColor.RED));
            foreach (Scene level in GameInstance.scenes)
            {
                if (level is Level && GameInstance.scenes.IndexOf(level) <= GameInstance.latestLevel)
                    du.addButton(CreateUI.createButton(-12f, 8f, 3f, 1, () =>
                    {
                        GameInstance.setLevel(GameInstance.scenes.IndexOf(level));
                    }, new GameFont(level.getName(), GameColor.RED)));
            }
            elementList.Add(du);

            elementList.Add(CreateUI.createButton(-12f, 9.25f, 3, 1, () =>
            {
                GameInstance.setLevel(0);
            }, new GameFont("Main Menu", GameColor.RED)));

            // Outer bounds
            CreateModel.createWall(0f, 10f, 20f, .5f);
            CreateModel.createWall(0f, -10f, 20f, .5f);
            CreateModel.createWall(-10f, 0f, .5f, 20f);
            CreateModel.createWall(10f, 0f, .5f, 20f);
        }

        protected void randomRotate(Model model)
        {
            bool rotate = false;
            if (randomInt(0, 1) < 0.5)
                rotate = true;

            if (rotate)
            {
                float r = (float)randomInt(0, 1);
                model.rotate(r < .5f ? (-(float)Math.PI / 3f) : ((float)Math.PI / 6f));
            }
        }

    }
}