using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSoloForStepmania
{
    public class FreezingPanel
    {
        public bool Left;
        public bool LeftUp;
        public bool Down;
        public bool Up;
        public bool RightUp;
        public bool Right;

        public FreezingPanel()
        {
            Left = false;
            LeftUp = false;
            Down = false;
            Up = false;
            RightUp = false;
            Right = false;
        }
    }
}
