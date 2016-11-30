using LaserAmazer.gui;
using LaserAmazer.math;
using LaserAmazer.render;
using System;
using System.Collections.Generic;

namespace LaserAmazer.level
{
    public abstract class Level : Scene
    {

        protected List<LaserStart> laserWrappers = new List<LaserStart>();
        protected bool isRendered = false;

        public Level(string name) : base(name)
        {
        }

        public override void LogicLoop()
        {
            if (laserWrappers != null)
            {
                for (int i = 0; i < laserWrappers.Count; i++)
                {
                    laserWrappers[i].Reflect();
                }
            }
        }

        public override void RenderObjects()
        {
            laserWrappers.Clear();
            elementList.Clear();
            timeStart = GetTime();
            Dropdown du = CreateUI.createDropdown(-12f, 8f, 3f, 1f, new GameFont("Select Level", GameColor.RED));
            foreach (Scene level in GameInstance.scenes)
            {
                if (level is Level && GameInstance.scenes.IndexOf(level) <= GameInstance.latestLevel)
                    du.addButton(CreateUI.createButton(-12f, 8f, 3f, 1, () =>
                    {
                        GameInstance.SetLevel(GameInstance.scenes.IndexOf(level));
                    }, new GameFont(level.getName(), GameColor.RED)));
            }
            elementList.Add(du);

            elementList.Add(CreateUI.createButton(-12f, 9.25f, 3, 1, () =>
            {
                GameInstance.SetLevel(0);
            }, new GameFont("Main Menu", GameColor.RED)));

            // Outer bounds
            CreateModel.CreateWall(0f, 10f, 20f, .5f);
            CreateModel.CreateWall(0f, -10f, 20f, .5f);
            CreateModel.CreateWall(-10f, 0f, .5f, 20f);
            CreateModel.CreateWall(10f, 0f, .5f, 20f);
        }

        protected void RandomRotate(Model model)
        {
            bool rotate = false;
            if (MathExtension.RandomInt(0, 1) < 0.5)
                rotate = true;

            if (rotate)
            {
                float r = (float)MathExtension.RandomInt(0, 1);
                model.Rotate(r < .5f ? (-(float)Math.PI / 3f) : ((float)Math.PI / 6f));
            }
        }

    }
}