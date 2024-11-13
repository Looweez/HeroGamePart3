using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public class HealthPickupTile : PickupTile
    {
        private Position position;
        public HealthPickupTile(Position position) : base(position)
        {
            this.Position = position;
        }

        //weird ass implementation method between these two
        public override void ApplyEffect(CharacterTile character) 
        {
            character.Heal(10);
        }

        public override char Display
        {
            get { return '+'; }
        }
    }
}
