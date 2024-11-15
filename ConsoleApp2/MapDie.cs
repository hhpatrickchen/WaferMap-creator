using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class MapDie
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsEdge { get; set; }

        public MapDie(int x, int y, bool isEdge)
        {
            X = x;
            Y = y;
            IsEdge = isEdge;
        }
    }
}
