using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsWaferMap;

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
        

        public Rectangle Bounds { get; set; }
       // public bool IsEdge { get; set; }
        public DIESTATE PassFail { get; internal set; }

        public int Number { set; get; } = 0;

        public int ID { get; internal set; }


        public int Column { get; set; }

        public int Row { get; set; }

        public DiePosition diePos { set; get; }
        public bool IsFlashing { get; set; } = false; // 是否閃爍
        public bool FlashState { get; set; } = false; // 閃爍狀態

        public Die(Rectangle bounds)
        {
            Bounds = bounds;
  
            PassFail = DIESTATE.IDLE;

            //if(isLightGreen)
            //{
            //    Number = 0xED;
            //}
        }

        public void UpdateBounds(Rectangle newBounds)
        {
            Bounds = newBounds;
            
        }
    }
}
