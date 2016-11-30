using System;
using System.IO;

namespace LaserAmazer
{
    public class SaveGame
    {

        private static object[] values;
        private static Action[] runs = new Action[] {
            () => {
                GameInstance.levelCompleteDialogue = (bool)values[0];
            },
            () => {
                GameInstance.showTimer = (bool)values[1]; 	//Shut up Matt, don't judge me on my lambda use
			},
            () => {
                GameInstance.samplingLevel = (int)values[2];
            },
            () => {
                GameInstance.latestLevel = (int)values[3];
            },
            () => {
                GameInstance.currentLevel = (int)values[4];
            }
    };

        // Read from the file, generate a new one with defaults if such a file doesn't exist
        static SaveGame()
        {
            try
            {
                using (StreamReader input = new StreamReader("saveGame.fd"))
                {
                }
            }
            catch
            {
                try
                {
                    string[] defaults = { "true", "true", "4", "2", "2" };  //Set the defaults for read settings
                    using (StreamWriter output = new StreamWriter("saveGame.fd"))
                    {
                        foreach (string line in defaults)
                        {
                            output.WriteLine(line);
                        }
                    }
                }
                catch
                {
                }
            }
            values = new object[] {
            true,
            true,
            4,
            2,
            2
        };
        }

        /**
         * Reads data from "saveGame.fd" in the root directory and stores the read data to the correct locations
         */
        public static void readData()
        {
            int i = 0;
            using (StreamReader input = new StreamReader("saveGame.fd"))
            {
                string s;
                while ((s = input.ReadLine()) != null)
                {
                    if (values[i] is bool)
                    {
                        values[i] = bool.Parse(s);
                        runs[i].Invoke();
                    }
                    else if (values[i] is int)
                    {
                        values[i] = int.Parse(s);
                        runs[i].Invoke();
                    }
                }
            }
        }

        /**
         * Writes all of the options to disk
         */
        public static void writeData()
        {
            try
            {
                string[] data = {
                    GameInstance.levelCompleteDialogue.ToString(),
                    GameInstance.showTimer.ToString(),
                    GameInstance.samplingLevel.ToString(),
                    GameInstance.latestLevel.ToString(),
                    GameInstance.currentLevel.ToString()};
                using (StreamWriter output = new StreamWriter("saveGame.fd"))
                {
                    foreach (string line in data)
                    {
                        output.WriteLine(line);
                    }
                }
            }
            catch
            {
            }
        }
    }
}