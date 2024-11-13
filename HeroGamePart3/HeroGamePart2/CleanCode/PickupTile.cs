using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public abstract class PickupTile : Tile
    {
        private Position position;

        public PickupTile(Position position) : base(position)
        {
            this.Position = position;
        }


        public override char Display
        {
            get { return '█'; }
        }

        public abstract void ApplyEffect(CharacterTile character);
    }
}
