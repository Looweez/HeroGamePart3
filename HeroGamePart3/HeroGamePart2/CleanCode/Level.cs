using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 3,
        Right = 2,
        None = 4
    }

    public class Level
    {
        public Tile[,] tiles;
        private EnemyTile[] enemies;
        private PickupTile[] pickups;
        private HeroTile hero;
        private ExitTile exit;
        private Level level;

        private int width;
        private int height;
        private int numEnemies;
        private bool isLocked;

        public int Width
        { 
            get { return width; } 
            set {  width = value; } 
        } 
        
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Level(int width, int height, int numEnemies, int numPickups, HeroTile hero = null, ExitTile exit = null) 
        {
            this.width = width;
            this.height = height;

            tiles = new Tile[width, height];
            enemies = new EnemyTile[numEnemies];
            pickups = new PickupTile[numPickups];

            for (int i = 0; i < numEnemies; i++)
            {
                CreateTile(TileType.Enemy, GetRandomEmptyPosition());
            }
            for (int i = 0; i < numPickups; i++)
            {
                CreateTile(TileType.PickUp, GetRandomEmptyPosition());
            }

            InitialiseTiles();

            if (hero == null)
            {
                this.hero = (HeroTile)CreateTile(TileType.Hero, GetRandomEmptyPosition());
            }
            else
            {
                hero.position = GetRandomEmptyPosition();
                this.hero = hero;
                tiles[hero.position.X, hero.position.Y] = hero;
            }

            CreateTile(TileType.Exit, GetRandomEmptyPosition());

            if (exit == null)
            {
                this.exit = (ExitTile)CreateTile(TileType.Exit, GetRandomEmptyPosition());
            }
            else
            {
                exit.position = GetRandomEmptyPosition();
                this.exit = exit;
                tiles[exit.position.X, exit.position.Y] = exit;
            }


            enemies = new EnemyTile[numEnemies];
            for ( int i = 0;i < enemies.Length;  i++)
            {
                enemies[i] = (EnemyTile)CreateTile(TileType.Enemy, GetRandomEmptyPosition());
                tiles[enemies[i].Position.X, enemies[i].Position.Y] = enemies[i];
            }

            UpdateVision();

            pickups = new PickupTile[numPickups];
            for (int j = 0; j < pickups.Length; j++)
            {
                pickups[j] = (PickupTile)CreateTile(TileType.PickUp, GetRandomEmptyPosition());
                tiles[pickups[j].Position.X, pickups[j].Position.Y] = pickups[j];
            }

            UpdateVision();
        }

        public enum TileType
        {
            Empty,
            Wall,
            Hero,
            Exit,
            Enemy,
            PickUp,
        }

        //Private CreateTile method
        // Task

        private Tile CreateTile(TileType type, Position position)
        {
            switch (type)
            {
                case TileType.Empty:
                    {
                        EmptyTile emptyTile = new EmptyTile(position);
                        tiles[position.X, position.Y] = emptyTile;
                        return emptyTile;
                    }
                case TileType.Wall:
                    {
                        WallTile wallTile = new WallTile(position);
                        tiles[position.X, position.Y] = wallTile;
                        return wallTile;
                    }
                case TileType.Hero:
                    {
                        HeroTile heroTile = new HeroTile(position,40,5);
                        tiles[position.X, position.Y] = heroTile;
                        return heroTile;
                    }
                case TileType.Enemy:
                    {
                        /*GruntTile enemyTile = new GruntTile(position, 40, 5, );
                        tiles[position.X, position.Y] = enemyTile;
                        return enemyTile;*/
                        return CreateEnemyTile(position, level);
                    }
                case TileType.Exit:
                    {
                        ExitTile exitTile = new ExitTile(position);
                        tiles[position.X, position.Y] = exitTile;
                        return exitTile;
                    }
                case TileType.PickUp:
                    {
                        return CreatePickUpTile(position);
                    }
                default:
                    {
                        EmptyTile emptyTile = new EmptyTile(position);
                        tiles[position.X, position.Y] = emptyTile;
                        return emptyTile;
                    }
            }
        }

        private Tile CreateTile(TileType type, int  x, int y) 
        {
            Position position = new Position(x, y);

            return CreateTile(type, position);
        }

        private EnemyTile CreateEnemyTile(Position position, Level level)
        {
            Random random = new Random();
            int chance = random.Next(1, 10 + 1); //ik i could've said 11 shhhhhut 
            if (chance < 0 && chance > 6)
            {
                return new GruntTile(position, 10, 1, level);
            }
            else if (chance < 6 && chance > 9)
            {
                return new WarlockTile(position, 10, 1, level);
            }
            else if (chance < 9 && chance > 11)
            {
                return new TyrantTile(position, 10, 1, level);
            }
            return null;

        }

        private PickupTile CreatePickUpTile(Position position)
        {
            Random random = new Random();
            int chance = random.Next(0, 3);
            if (chance < 0 && chance > 2)
            {
                return new HealthPickupTile(position);
            }
            else if (chance == 2)
            {
                return new AttackBuffPickupTile(position);
            }
            return null;
        }

        public void InitialiseTiles()
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (i > 0 && i < tiles.GetLength(0) - 1 &&
                        j > 0 && j < tiles.GetLength(1) - 1)
                    {
                        CreateTile(TileType.Empty, i, j);
                    }
                    else
                    {
                        CreateTile(TileType.Wall, i, j);
                    }
                }
            }
        }

        private Position GetRandomEmptyPosition()
        {
            // Find an empty tile and return it
            Position aGoodPosition;
            Random random = new Random();
            int randomXposition;
            int randomYposition;

            do
            {
                randomXposition = random.Next(0, tiles.GetLength(0));
                randomYposition = random.Next(0, tiles.GetLength(1));
            }
            while (tiles[randomXposition, randomYposition].Display != '.');

            aGoodPosition = new Position(randomXposition, randomYposition);
            return aGoodPosition;
        }

        public HeroTile Hero
        {
            get { return hero; }
        }

        public ExitTile Exit
        {
            get { return exit; }
        }

        public PickupTile[] Pickups
        {
            get { return pickups; }
        }

        public EnemyTile[] Enemies
        {
            get { return Enemies; }
        }


        public void SwopTiles(Tile tile1, Tile tile2)
        {
            Tile tempObject = tile1;

            tile1.Position = tile2.Position;
            tile2.Position = tempObject.Position;

            // Update the x and y positions of the tiles

            tiles[tile1.Position.X, tile1.Position.Y] = tile1;
            tiles[tile1.Position.X, tile1.Position.Y] = tile2;
        }

        public void UpdateVision() 
        {
            hero.UpdateVision(this, hero);

            foreach (EnemyTile enemy in enemies)
            {
                enemy.UpdateVision(this, enemy);
            }
        }

        public void ReplaceTile(TileType type, Position position)
        {
            CreateTile(type, position);
        }

        public override string ToString()
        {
            string mapAsString = "";

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    mapAsString += tiles[i, j].Display.ToString();
                }
                mapAsString += "\n";
            }

            return mapAsString;
        }

        public bool UpdateExit()
        {
            if (numEnemies == 0)
            {
                return isLocked = false;
            }

            return isLocked = true;
        }
    }
}
