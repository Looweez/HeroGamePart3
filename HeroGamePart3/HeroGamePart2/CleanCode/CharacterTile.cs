using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public abstract class CharacterTile : Tile
    {
        private int hitPoints;
        private int maxHitPoints;
        private int attackPower;
        private int doubleDamageCount;

        private Tile[] vision;

        public CharacterTile(Position position, int hitPoints, int attackPower) : base(position)
        {
            this.hitPoints = hitPoints;
            this.Position = position;
            this.maxHitPoints = maxHitPoints;
            this.attackPower = attackPower;

            vision = new Tile[4];
        }

        public Tile[] Vision
        {
            get { return vision; }
            set { vision = value; }
        }

        public override char Display
        {
            get { return '.'; }
        }

        public void UpdateVision(Level level, CharacterTile entity)
        {
            Position heroPos = entity.Position;

            vision[0] = level.tiles[Position.X, Position.Y - 1]; //NORTH
            vision[1] = level.tiles[Position.X + 1, Position.Y]; //EAST
            vision[2] = level.tiles[Position.X, Position.Y + 1]; //SOUTH
            vision[3] = level.tiles[Position.X - 1, Position.Y]; //WEST

            vision[4] = level.tiles[Position.X + 1, Position.Y + 1]; //NORTH EAST
            vision[5] = level.tiles[Position.X - 1, Position.Y + 1]; //NORTH WEST
            vision[6] = level.tiles[Position.X + 1, Position.Y - 1]; //SOUTH EAST
            vision[7] = level.tiles[Position.X - 1, Position.Y - 1]; //SOUTH WEST
        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;

            if (hitPoints <= 0)
            {
                hitPoints = 0;
            }
        }

        public void Attack(CharacterTile tile)
        {
            if (doubleDamageCount > 0)
            {
                doubleDamageCount--;
            }
            else
            {
                tile.TakeDamage(attackPower);
            }
        }

        public bool IsDead
        {
            get
            {
                if (hitPoints < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public void Heal(int healPoints)
        {
            hitPoints += healPoints;

            if (hitPoints >= maxHitPoints)
            {
                hitPoints = maxHitPoints;
            }
            else
            {
                hitPoints = maxHitPoints;
            }
        }

        public void SetDoubleDamage(int numAttacks)
        {
            doubleDamageCount += numAttacks;  //brain is in jar. cant find jar 
        }

        public int HitPoints
        {
            get { return hitPoints; }
        }
        public int AttackPower
        {
            get { return attackPower; }
        }
        public int MaxHitPoints
        {
            get { return maxHitPoints; }
        }

    }
}
