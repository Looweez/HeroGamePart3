using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanCode.Level;

namespace CleanCode
{
    public class GruntTile : EnemyTile
    {
        private Position position;
        private int hitPoints;
        private int maxHitPoints;
        private int attackPower;

        private Random random;

        public GruntTile(Position position, int hitPoints, int attackPower, Level level) : base(position, 10, 1, level)
        {

            this.Position = position;
            this.hitPoints = hitPoints;
            this.maxHitPoints = hitPoints;
            this.attackPower = attackPower;
        }

        public override bool GetMove(out Tile tile)
        {
            Tile[] emptyNeighbours = new Tile[Vision.Length];
            int index = 0;

            for (int i = 0; i < Vision.GetLength(1); i++) 
            {
                if (Vision[i] is EmptyTile)
                {
                    emptyNeighbours[index] = Vision[i];
                    index++;
                }
            }

            Array.Resize(ref emptyNeighbours, index); 

            if (emptyNeighbours.Length != 0) // if theres empty tile
            {
                random = new Random();
                tile = emptyNeighbours[random.Next(0, emptyNeighbours.Length)];
                return true;
            }
            else // no empty tiles
            {
                tile = null;
                return false;
            }
        }

        public override CharacterTile[] GetTargets() 
        {
            CharacterTile[] targets = new CharacterTile[Vision.Length];
            int index = 0;

            for (int i = 0; i < Vision.Length; i++)
            {
                if (Vision[i] is HeroTile)
                {
                    targets[index] = (CharacterTile)Vision[i];
                    index++;

                }
            }
            Array.Resize (ref targets, index);

            return targets;
           
        }

        public override char Display
        {
            get
            {
                if (IsDead == false)
                {
                    return 'Ϫ';
                }
                else
                {
                    return 'X';
                }
            }
        }


    }
}
