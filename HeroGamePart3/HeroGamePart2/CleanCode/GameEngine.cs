using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public enum GameState
    {
        InProgress,
        Complete,
        GameOver
    }

    public class GameEngine
    {
        //Q.2.5
        //Fields
        public Level level;
        private int numLevels;
        private Random random;
        private int currentLevel = 1;
        private bool attackSuccess;
        private int hitPoints;
        private bool isLocked;

        public GameState state = GameState.InProgress;

        const int MIN_SIZE = 10;
        const int MAX_SIZE = 20;

        private int enemyMoveCounter = 0;

        // ----------------------------------------------------------------------------------------------------

        public GameEngine(int numLevels) 
        {
            this.numLevels = numLevels;
            random = new Random();
            level = new Level(random.Next(MIN_SIZE, MAX_SIZE + 1),random.Next(MIN_SIZE, MAX_SIZE + 1), currentLevel, 1);
        }
        // ----------------------------------------------------------------------------------------------------
        private bool MoveHero(Direction direction)
        {
            Tile targetTile = level.Hero.Vision[(int)direction];
            if (targetTile is PickupTile)
            {
                PickupTile foundThisCoolPickup = (PickupTile)targetTile;
                foundThisCoolPickup.ApplyEffect(level.Hero);
                level.SwopTiles(level.Hero, targetTile);
                level.ReplaceTile(Level.TileType.Empty, targetTile.Position);
                return true;
            }
            else if (targetTile is ExitTile && currentLevel == numLevels)
            {
                if (isLocked == false)
                {
                    state = GameState.Complete;
                    return false;
                }
                else
                {
                    state = GameState.InProgress;
                    return true;
                }
            }

            else if (targetTile is ExitTile)
            {
                if (isLocked == false)
                {
                    NextLevel();
                    return true;
                }

                return false;
            }
            else if (targetTile is EmptyTile)
            {
                level.SwopTiles(level.Hero, targetTile);
                level.UpdateVision();
                return true;
            }
            else
            {
                return false;
            }

        }
        public void TriggerMovement(Direction direction)
        {

            if (state == GameState.GameOver)
            {
                return;
            }
            enemyMoveCounter++;
            level.Hero.UpdateVision(level, level.Hero);
            MoveHero(direction);

            if (enemyMoveCounter == 2)
            {
                MoveEnemies();
                enemyMoveCounter = 0;
            }
        }

        public void NextLevel() // move to the next level
        {
            currentLevel++;
            HeroTile hero = this.level.Hero;

            random = new Random();
            level = new Level(random.Next(MIN_SIZE, MAX_SIZE + 1), random.Next(MIN_SIZE, MAX_SIZE + 1), currentLevel, 1, hero);
            hero.UpdateVision(level, level.Hero);
        }

        private void MoveEnemies()
        {
            foreach (EnemyTile enemy in level.Enemies)
            {
                Tile target;
                bool canMove = enemy.GetMove(out target);

                if (enemy.IsDead == false && canMove == true) // if the enemy is alive and can move
                {
                    level.SwopTiles(enemy, target);
                }

                level.UpdateVision();
            }
        }


        public override string ToString()
        {
            if (state == GameState.Complete)
            {
                return "You have completed the game!";
            }
            else if (state == GameState.InProgress)
            {
                return level.ToString();
            }
            else if (state == GameState.GameOver)
            {
                return "Game over!";
            }
            else
            {
                return level.ToString();
            }
        }

        //q.3.1
        public bool HeroAttack(Direction direction)
        {
            // retrieve attack target tile from vision array

            Tile targetTile = level.Hero.Vision[((int)direction)];

            if (targetTile is CharacterTile)
            {
                level.Hero.Attack((CharacterTile)targetTile); 
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void TriggerAttack(Direction direction)
        {
            if (state == GameState.GameOver)
            {
                return;
            }

            if (HeroAttack(direction) == true)   
            {
                EnemiesAttack();
            }

            if (level.Hero.IsDead == true)
            {
                state = GameState.GameOver;
            }

            level.UpdateExit();

        }


        //q.3.2

        public bool IsDead;
        private void EnemiesAttack()
        {
            foreach (EnemyTile enemy in level.Enemies)
            {
                if (enemy.IsDead == false)
                {
                    CharacterTile[] targets = enemy.GetTargets();
                    foreach (CharacterTile target in targets) 
                    { 
                        enemy.Attack(target);
                    }
                    return;
                }
            }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
        }


        public string HeroStats
        {
            get 
            {
                return "HP: " + level.Hero.HitPoints + "/" + level.Hero.MaxHitPoints + "\nATK: " + level.Hero.AttackPower;
            }
            
        }

        // ---------------------------------------------------------------------------------------------------- saving / loading



        public void SaveGame()
        {
            string filePath = "savegame.dat";
            GameSaveData saveData = new GameSaveData(numLevels, currentLevel, level);

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fileStream, saveData);
            }

            Console.WriteLine("Game saved");

        }

        public void LoadGame()
        {
            string filePath = "savegame.dat";

            if (File.Exists(filePath))
            {
                
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    GameSaveData saveData = (GameSaveData)formatter.Deserialize(fileStream);

                    numLevels = saveData.NumLevels;
                    currentLevel = saveData.CurrentLevel;
                    level = saveData.LevelObject;
                }

                Console.WriteLine("Game loaded");
            }
            else
            {
                Console.WriteLine("No save found");
            }
        }


        /*public void SaveGame() // make a serializable class ?
        {
            
            string filename = Console.ReadLine();
            FileStream outFile = new FileStream(
                filename + ".txt", FileMode.Create, FileAccess.Write);

            StreamWriter writer = new StreamWriter(outFile);
            // send through the actual data


            
            string input = GameEngine.currentLevel;

            writer.WriteLine(input);
            writer.Close();
            outFile.Close(); 
        }

        public void LoadGame()
        {
            GameSaveData fd = new GameSaveData();

            string fileName = Console.ReadLine();

            fd.DisplayFileInfo(fileName);


            Console.ReadLine();
        }*/
        


    }
}
