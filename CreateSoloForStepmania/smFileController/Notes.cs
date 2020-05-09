using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSoloForStepmania
{
    class Notes
    {
        public string chartType { get; set; }
        public string description { get; set; }
        public string difficulty { get; set; }
        public string numeniculMeter { get; set; }
        public string radarValue { get; set; }
        public NoteValue noteValue { get; set; }

        public Notes()
        {
            chartType = "";
            description = "";
            difficulty = "";
            numeniculMeter = "";
            radarValue = "";
            noteValue = new NoteValue();
        }

        public Notes(string value)
        {
            string[] s = value.Split(":".ToCharArray());

            if (s.Count() != 6)
            {
                return;
            }

            //noteValue以外 無駄な文字の処理
            for (int i = 0; i < 5; i++)
            {
                s[i] = s[i].Replace("\r\n", "");

                while (s[i].StartsWith(" "))
                {
                    s[i] = s[i].Remove(0, 1);
                }
            }

            //noteValueの 無駄な文字の処理
            while (s[5].StartsWith(" ") || s[5].StartsWith("\r") || s[5].StartsWith("\n"))
            {
                s[5] = s[5].Remove(0, 1);
            }

            chartType = s[0];
            description = s[1];
            difficulty = s[2];
            numeniculMeter = s[3];
            radarValue = s[4];
            noteValue = new NoteValue(s[5]);
        }

        public override string ToString()
        {
            string result = "";

            result += string.Format("//---------------{0} - ----------------\r\n", chartType);
            result += string.Format("#NOTES:\r\n");
            result += string.Format("     {0}:\r\n", chartType);
            result += string.Format("     {0}:\r\n", description);
            result += string.Format("     {0}:\r\n", difficulty);
            result += string.Format("     {0}:\r\n", numeniculMeter);
            result += string.Format("     {0}:\r\n", radarValue);
            result += noteValue.ToString();
            result += ";\r\n\r\n";

            return result;
        }
    }
}
