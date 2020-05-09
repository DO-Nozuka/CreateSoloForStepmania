using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSoloForStepmania
{
    public class Panel
    {
        public char Left;
        public char LeftUp;
        public char Down;
        public char Up;
        public char RightUp;
        public char Right;

        public bool swapedUpAndLeftUp;
        public bool swapedUpAndRightUp;
        public bool swapedLeftAndLeftUp;
        public bool swapedRightAndRightUp;

        public bool haveStep { get { return Left != '0' || LeftUp != '0' || Down != '0' || Up != '0' || RightUp != '0' || Right != '0'; } }

        public void ArrowToPanel(string arrow)
        {
            char[] ca = arrow.ToCharArray();
            if (ca.Count() == 4)
            {
                Left = ca[0];
                Down = ca[1];
                Up = ca[2];
                Right = ca[3];
            }
            else if (ca.Count() == 6)
            {
                Left = ca[0];
                LeftUp = ca[1];
                Down = ca[2];
                Up = ca[3];
                RightUp = ca[4];
                Right = ca[5];
            }
        }


        public string To6Arrow()
        {
            string result = "";

            result += Left.ToString();
            result += LeftUp.ToString();
            result += Down.ToString();
            result += Up.ToString();
            result += RightUp.ToString();
            result += Right.ToString();

            return result;
        }

        public enum SwapMode
        {
            UpAndLeftUp,
            UpAndRightUp,
            LeftAndLeftUp,
            RightAndRightUp
        }

        /* 入れ替え系 */
        public void Swap(SwapMode swapMode)
        {
            switch (swapMode)
            {
                case SwapMode.UpAndLeftUp:
                    char UpAndLeftUp = Up;
                    Up = LeftUp;
                    LeftUp = UpAndLeftUp;
                    swapedUpAndLeftUp = !swapedUpAndLeftUp;
                    break;
                case SwapMode.UpAndRightUp:
                    char UpAndRightUp = Up;
                    Up = RightUp;
                    RightUp = UpAndRightUp;
                    swapedUpAndRightUp = !swapedUpAndLeftUp;
                    break;
                case SwapMode.LeftAndLeftUp:
                    char LeftAndLeftUp = Left;
                    Left = LeftUp;
                    LeftUp = LeftAndLeftUp;
                    swapedLeftAndLeftUp = !swapedLeftAndLeftUp;
                    break;
                case SwapMode.RightAndRightUp:
                    char RightAndRightUp = Right;
                    Right = RightUp;
                    RightUp = RightAndRightUp;
                    swapedRightAndRightUp = !swapedRightAndRightUp;
                    break;
            }
        }

        //コンストラクタ
        public Panel(string arrow = "000000")
        {
            Left = '0';
            LeftUp = '0';
            Down = '0';
            Up = '0';
            RightUp = '0';
            Right = '0';

            ArrowToPanel(arrow);

            swapedUpAndLeftUp = false;
            swapedUpAndRightUp = false;
            swapedLeftAndLeftUp = false;
            swapedRightAndRightUp = false;
        }
    }
}
