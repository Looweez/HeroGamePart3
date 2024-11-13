using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public class ExitTile : Tile
    {
        public Position position;
        private bool isLocked;

        public ExitTile(Position position) : base(position)
        {
            this.Position = position;
        }

        public bool IsLocked
        {
            get;
        }

        public override char Display
        {
            get
            {
                if (isLocked == false)
                {
                    return '▒'; //IF OPEN
                }
                else
                {
                    return '▓'; //add fuckass symbol
                }
            }
        }
    }
}
