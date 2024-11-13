﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    public class EmptyTile : Tile
    {
        //Q.2.3
        private Position position;

        //Constructor accepts position
        
        public EmptyTile(Position position) : base(position)
        {
            this.position = position;
        }

        public override char Display
        {
            get { return '.'; } //exit tile and wall tile copy paste
        }
    }
}
