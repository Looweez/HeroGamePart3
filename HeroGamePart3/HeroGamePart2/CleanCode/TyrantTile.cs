using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using static CleanCode.Level;

namespace CleanCode
{
    public class TyrantTile : EnemyTile
    {
        private List<Tile> vision = new List<Tile>(4);

        public TyrantTile(Position position, int hitPoints, int attackPower, Level level) : base(position, 15, 5, level)
        {
            this.Position = position;
        }

        public override bool GetMove(out Tile tile)
        {
            tile = null;
            Tile[] emptyNeighbours = new Tile[Vision.Length];
            int index = 0;

            return false;
        }

        public void UpdateTyrantVision(Level level, CharacterTile entity)
        {

            vision[0] = level.tiles[Position.X, Position.Y - 1]; //NORTH
            vision[1] = level.tiles[Position.X + 1, Position.Y]; //EAST
            vision[2] = level.tiles[Position.X, Position.Y + 1]; //SOUTH
            vision[3] = level.tiles[Position.X - 1, Position.Y]; //WEST
        }

        public override CharacterTile[] GetTargets()
        {
            List<CharacterTile> targets = new List<CharacterTile>();
            foreach (Tile tile in vision)
            {
                if (tile is CharacterTile characterTile && (tile.type == TileType.Hero || tile.type == TileType.Enemy))
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
                    return '§'; //add fuckass symbol
                }
                else
                {
                    return 'X';
                }
            }
        }
    }
}
