using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HedgeHulkApp
{

    public enum DIESTATE
    {
        IDLE = 0,
        PASS = 1,
        FAIL = 2,
    }
    public class Die
    {
        public Rectangle Bounds { get; private set; }
        public bool IsLightGreen { get; set; }
        public DIESTATE PassFail { get; internal set; }

        public int ID { get; internal set; }

        public Die(Rectangle bounds, bool isLightGreen)
        {
            Bounds = bounds;
            IsLightGreen = isLightGreen;
            PassFail = DIESTATE.IDLE;
        }

        public void UpdateBounds(Rectangle newBounds)
        {
            Bounds = newBounds;
            
        }
    }
}
