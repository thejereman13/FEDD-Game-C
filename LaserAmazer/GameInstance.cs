using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Threading;
using OpenTK;
using System;
using System.Diagnostics;
using LaserAmazer.Level;
using LaserAmazer.Render;

namespace LaserAmazer
{
    public class GameInstance
    {

		public static string pathName = "../../res/";

        public static Window window;
        public static ObjectManager objectManager;
		public static Thread logicLoop;
        public static bool levelCompleteDialogue = true;
        public static bool showTimer = true;
        public static int samplingLevel = 4;
        public static int latestLevel = 5;
        public static int currentLevel = 2;

        private bool canRender;
        public static readonly bool demoMode = true;

        public static List<Scene> scenes = new List<Scene>() {
            new MainMenu(),
            new OptionsMenu(),
            new Level1(),
            new Level2(),
            new Level3(),
            new Level4(),
            new Level5(),
            new Level6(),
            new Level7(),
            new Level8(),
            new Level9(),
            new Level10()
        };

        public static int levNum = 0; // Start with 0
        private static float fadeN = 90f; // Amount of fade in degrees (0-90)
        private static bool hasLevel = false;
        public static Shader shader;
        private Stopwatch timer = new Stopwatch();

        private static State state;
        static volatile bool gameState; // Boolean value to pause logic Thread when state != GAME

        // Game begins here
        public GameInstance()
        {
            // Run and quit on error
            try
            {
                Setup();
                RenderLoop();
            }
            finally
            {
               
            }
        }

        /**
         * Setup all the window settings
         */
        private void Setup()
        {
			SaveGame.readData();
            //	new ModLoader();
            objectManager = new ObjectManager();
            window = new Window(800, 800, "Laser Amazer", false);

            if (demoMode)
                scenes.Add(new DemoLevel());
            scenes.Add(new GameComplete());
        }

        /**
         * Game render loop
         */
        private void RenderLoop()
        {

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            FloatColor clearColor = GameColor.DARK_GREY;
            GL.ClearColor(clearColor.red(), clearColor.blue(), clearColor.green(), 1f);
			
            // Set up main menu text
            GameFont menuItem = new GameFont("Start Game", GameColor.ORANGE);
            GameFont startGame = new GameFont("Press Space to Start Game", GameColor.YELLOW);
			Matrix4d scale = Matrix4d.CreateTranslation(new Vector3d(100, 0, 0));   //Might still need a scale() method
            Matrix4d target = new Matrix4d();
			Camera camera = new Camera(window.GetWidth(), window.GetHeight());
            camera.SetPosition(new Vector3d(-100, 0, 0));
			
			shader = new Shader("shader");
			double frameCap = 1.0 / 60; // 60 FPS

            double frameTime = 0;
            double time = GetTime();
            double unprocessed = 0;

			SetState(State.GAME); // Set the game state
            SetLevel(0); // Set the starting level

            logicLoop = new Thread(() => LogicLoop()); // Run the logic in a separate thread
			logicLoop.Start();
            window.CenterWindow(); // Center window on screen
			RenderLevel();
			return;
			// Poll window while window isn't about to close
			while (!window.ShouldClose())
            {
				
				canRender = false;

                // Control frames per second
                {
                    double timeNow = GetTime();
                    double elapsed = timeNow - time;
                    unprocessed += elapsed;
                    frameTime += elapsed;
                    time = timeNow;
                }

                // Run all non-render related tasks
                while (unprocessed >= frameCap)
                {
                    unprocessed -= frameCap;
                    canRender = true;
                    target = scale;

                    window.Update();

                    if (frameTime >= 1.0)
                        frameTime = 0;
                }

                // Render when scene changes
                if (canRender)
                {
                    RenderLevel();
                    GL.Clear(ClearBufferMask.ColorBufferBit);

                    if (state.Equals(State.GAME))
                    {
                        gameState = true;
                        shader.Bind();
                        shader.UpdateUniforms(camera, target);
                        objectManager.RenderAll();
                        window.UpdateTime();
                        window.RenderElements();
                        GetCurrentLevel().RenderLoop();
                    }
                    else if (state.Equals(State.LEVEL_COMPLETE))
                    {
                        GetCurrentLevel().SetActive(false); // Set level as inactive

                        // If user has the level complete dialogue enabled
                        if (levelCompleteDialogue)
                        {
                            gameState = false;

                            shader.Bind();
                            shader.UpdateUniforms(camera, target);
                            objectManager.RenderAll();

                            window.RenderElements();

                            // Add dark rectangle to make text more readable
                            shader.Unbind();
                            GL.Enable(EnableCap.Blend);
                            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                            GL.Color4(clearColor.red(), clearColor.green(), clearColor.blue(), .5f);
                            GL.Rect(-10f, -10f, 10f, 10f);

                            Scene latestLevel = GetCurrentLevel();

                            menuItem.renderString("Congratulations!", Alignment.CENTER, 0.1f, 0.45f);
                            menuItem.renderString("You've completed " + latestLevel.getName() + " in " + latestLevel.GetElapsedSeconds() + " seconds.", Alignment.CENTER, 0.02f, 0.2f);
                            startGame.renderString("(Press Space to continue.)", Alignment.CENTER, -0.45f, 0.3f);
                        }
                        else
                        {
                            SetState(State.NEXT_LEVEL);
                        }
                    }
                    else if (state.Equals(State.NEXT_LEVEL))
                    {
                        gameState = false;
                        NextLevel();
                        SetState(State.GAME);
                    }

                    if (!state.Equals(State.LEVEL_COMPLETE))
                    {
                        // Fading
                        shader.Unbind();
                        GL.Enable(EnableCap.Blend);
                        GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                        if (fadeN > 0)
                        {
                            fadeN -= 2.5f;
                            GL.Color4(clearColor.red(), clearColor.blue(), clearColor.green(), (float)System.Math.Sin(fadeN));
                            GL.Rect(-10f, -10f, 10f, 10f);
                        }
                    }

                    window.SwapBuffers(); // Swap the render buffers
                }

            }
            SaveGame.writeData();
        }

        private void LogicLoop()
        {
            Thread.CurrentThread.Name = "Logic";
            int timing = (int)System.Math.Round(1f / 60 * 1000f);  // Get the number of milliseconds between frames based on 60 times a second

            while (!window.ShouldClose())
            {
                if (!gameState) continue;

                double timeNow = GetTime(); // Get time at the start of the loop

                // Make sure it's the active level
                if (GetCurrentLevel().IsActive())
                    GetCurrentLevel().LogicLoop();

                {
                    int sleeptime = timing - (int)(GetTime() - timeNow); // Sync the game loop to update at the refresh rate
                                                                         //System.out.println(sleeptime);
                    try
                    {
                        Thread.Sleep((int)sleeptime);
                    }
                    catch
                    {
                    }
                }

            }
        }

        /**
         * @return System time in seconds
         */
        public double GetTime()
        {
            return timer.ElapsedMilliseconds;
        }

        public static void SetState(State s)
        {
            Fade();
            state = s;
        }

        public static State GetState()
        {
            return state;
        }

        private static void NextLevel()
        {
            GetCurrentLevel().SetActive(false); // No longer the active level

            // If all levels complete, reset to level 0
            levNum++;
            latestLevel = (levNum <= latestLevel) ? (latestLevel) : (levNum);
            if (levNum > 1)
                currentLevel = levNum;

            if (levNum > scenes.Count - 1)
            {
                levNum = 0;
                SetLevel(scenes.Count - 1); // Set to GameComplete (last level)
            }

            GetCurrentLevel().SetActive(true); // Set new active level
            hasLevel = false;
        }

        public static Scene GetCurrentLevel()
        {
            return scenes[levNum];
        }

        public static void SetLevel(int levelNumber)
        {
            if (levelNumber > scenes.Count - 1)
            {
                levNum = scenes.Count - 1;
            }
            else
            {
                levNum = levelNumber;
            }

            if (demoMode)
            {
                if (levNum > 1 && levNum != scenes.Count - 2)
                    currentLevel = levNum;
            }
            else
            {
                if (levNum > 1)
                    currentLevel = levNum;
            }

            Fade();
            latestLevel = (levNum <= latestLevel) ? (latestLevel) : (levNum);
            scenes[levNum].SetActive(true); // Set active level
            hasLevel = false;
        }

        private void RenderLevel()
        {
            if (levNum < scenes.Count && !hasLevel)
            {
                objectManager.ClearAll();
                window.ClearElements();
                window.AddElements();
                GetCurrentLevel().RenderObjects(); // Add all objects to the scene from the level class
                objectManager.UpdateModels();
                hasLevel = true;
            }
            else if (!hasLevel)
            {
                Console.WriteLine("End of game");
            }
        }

        private static void Fade()
        {
            fadeN = 90f;
        }

    }
}