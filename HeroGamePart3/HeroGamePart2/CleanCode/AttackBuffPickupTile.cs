using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public class AttackBuffPickupTile : PickupTile
    {
        public AttackBuffPickupTile(Position position) : base(position)
        {
            this.Position = position;
        }

        public override void ApplyEffect(CharacterTile character) //error no body bc it is abstract
        {
            character.SetDoubleDamage(3);
        }
        

        public override char Display
        {
            get { return '*'; }
        }
    }
}
