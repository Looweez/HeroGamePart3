using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    [Serializable]                                      //make class serializable
    public class GameSaveData
    {
        public int NumLevels;
        public int CurrentLevel;
        public Level LevelObject;    //it wont let me just make it level :(

        public GameSaveData(int numLevels, int currentLevel, Level levelObject)
        {
            NumLevels = numLevels;
            CurrentLevel = currentLevel;
            LevelObject = levelObject;
        }
    }
}
