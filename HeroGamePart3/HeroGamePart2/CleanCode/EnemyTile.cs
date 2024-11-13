using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public abstract class EnemyTile : CharacterTile
    {
        private Position position;
        private int hitPoints;
        private int attackPower;
        private int maxHitPoints;

        bool moveSuccess;
        public CharacterTile[] targets;
        private EnemyTile[] Enemies; // field(?)

        

        public EnemyTile[] enemies
        {
            get { return enemies;  }
        }

        public EnemyTile(Position position, int hitPoints, int attackPower, Level level) : base(position, 1, 1)
        {
            this.Position = position;
            this.hitPoints = hitPoints;
            this.maxHitPoints = hitPoints;
            this.attackPower = attackPower;
        }
        public bool MoveSuccess
        {
            get { return moveSuccess; }
            set { moveSuccess = value; }
        }

        public abstract bool GetMove(out Tile tile);   

        public abstract CharacterTile[] GetTargets();

        
    }
    
}
