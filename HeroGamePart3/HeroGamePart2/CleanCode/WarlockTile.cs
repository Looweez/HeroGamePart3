using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanCode.Level;

namespace CleanCode
{
    internal class WarlockTile : EnemyTile
    {
        private Tile[] vision;

        public WarlockTile(Position position, int hitPoints, int attackPower, Level level) : base(position, 10, 5, level)
        {
            this.Position = position;
            vision = new Tile[8];
        }

        public override bool GetMove(out Tile tile)
        {
            tile = null;
            Tile[] emptyNeighbours = new Tile[Vision.Length];
            int index = 0;

            return false;
        }

        public void UpdateWarlockVision(Level level, CharacterTile entity)
        {

            vision[0] = level.tiles[Position.X, Position.Y - 1]; //NORTH
            vision[1] = level.tiles[Position.X + 1, Position.Y]; //EAST
            vision[2] = level.tiles[Position.X, Position.Y + 1]; //SOUTH
            vision[3] = level.tiles[Position.X - 1, Position.Y]; //WEST

            vision[4] = level.tiles[Position.X + 1, Position.Y + 1]; //NORTH EAST
            vision[5] = level.tiles[Position.X - 1, Position.Y + 1]; //NORTH WEST
            vision[6] = level.tiles[Position.X + 1, Position.Y - 1]; //SOUTH EAST
            vision[7] = level.tiles[Position.X - 1, Position.Y - 1]; //SOUTH WEST
        }

        public override CharacterTile[] GetTargets()
        {
            List<CharacterTile> targets = new List<CharacterTile>();
            foreach (Tile tile in vision)
            {
                if (tile is CharacterTile characterTile && (tile.type == TileType.Hero || tile.type == TileType.Enemy)) // HUH
                {
                    targets.Add(characterTile);
                }
            }

            return targets.ToArray();
        }

        public override char Display
        {
            get
            {
                if (IsDead == false)
                {
                    return 'ᐂ'; //add fuckass symbol
                }
                else
                {
                    return 'X';
                }
            }
        }
    }
}
