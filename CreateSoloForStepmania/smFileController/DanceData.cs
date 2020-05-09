using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreateSoloForStepmania
{
    class DanceData
    {
        public string TITLE { get; private set; }
        public string SUBTITLE { get; private set; }
        public string ARTIST { get; private set; }
        public string TITLETRANSLIT { get; private set; }
        public string SUBTITLETRANSLIT { get; private set; }
        public string ARTISTTRANSLIT { get; private set; }
        public string GENRE { get; private set; }
        public string CREDIT { get; private set; }
        public string BANNER { get; private set; }
        public string BACKGROUND { get; private set; }
        public string LYRICSPATH { get; private set; }
        public string CDTITLE { get; private set; }
        public string MUSIC { get; private set; }
        public string OFFSET { get; private set; }
        public string BPMS { get; private set; }
        public string STOPS { get; private set; }
        public string SAMPLESTART { get; private set; }
        public string SAMPLELENGTH { get; private set; }
        public string DISPLAYBPM { get; private set; }
        public string SELECTABLE { get; private set; }
        public string BGCHANGES { get; private set; }
        public string FGCHANGES { get; private set; }
        public List<Notes> notesList { get; private set; } = new List<Notes>();

        //3. //で始まる行の削除
        private string DeleatComment(string input)
        {
            string pattern = @"/{2}.*\r\n";

            MatchCollection matchedObjects = Regex.Matches(input, pattern);
            foreach (Match matched in matchedObjects)
            {
                Console.WriteLine(matched.Value);
            }

            return Regex.Replace(input, pattern, "");
        }

        //4. # と ; に挟まれた文字列を取得
        private List<string> GetTagStringList(string input)
        {
            string pattern = @"#.*?;";

            List<string> result = new List<string>();

            MatchCollection matchedObjects = Regex.Matches(input, pattern, RegexOptions.Singleline);    //SingleとMultiの意味が逆な感じ。
            foreach (Match matched in matchedObjects)
            {
                Console.WriteLine(matched.Value);
                result.Add(matched.Value);
            }

            return result;
        }

        //5. TAG, VALUEリストを作成  #xxx:xxx;
        private List<Tuple<string, string>> GetTagValueList(List<string> tagStringList)
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            foreach (string tagString in tagStringList)
            {
                result.Add(GetTagValue(tagString));
            }

            return result;
        }


        //5.1 TagValueセットを抽出する
        private Tuple<string, string> GetTagValue(string tagString)
        {
            string pattern = @"#(?<tag>.*?):(?<value>.*?);";

            Match matchedObjects = Regex.Match(tagString, pattern, RegexOptions.Singleline);    //SingleとMultiの意味が逆な感じ。
            if (matchedObjects.Success)
            {
                string tag = matchedObjects.Groups["tag"].Value;
                string value = matchedObjects.Groups["value"].Value;

                return new Tuple<string, string>(tag, value);
            }

            return new Tuple<string, string>("", "");
        }

        //6. 各クラスを格納
        private void setValues(List<Tuple<string, string>> tagValueList)
        {
            foreach (Tuple<string, string> tagValue in tagValueList)
            {
                setValue(tagValue);
            }
        }

        //6.1 tagValueから値を入れる
        private void setValue(Tuple<string, string> tagValue)
        {
            string tag = tagValue.Item1;
            string value = tagValue.Item2;

            switch (tag)
            {
                case "TITLE":
                    TITLE = value;
                    break;
                case "SUBTITLE":
                    SUBTITLE = value;
                    break;
                case "ARTIST":
                    ARTIST = value;
                    break;
                case "TITLETRANSLIT":
                    TITLETRANSLIT = value;
                    break;
                case "SUBTITLETRANSLIT":
                    SUBTITLETRANSLIT = value;
                    break;
                case "ARTISTTRANSLIT":
                    ARTISTTRANSLIT = value;
                    break;
                case "GENRE":
                    GENRE = value;
                    break;
                case "CREDIT":
                    CREDIT = value;
                    break;
                case "BANNER":
                    BANNER = value;
                    break;
                case "BACKGROUND":
                    BACKGROUND = value;
                    break;
                case "LYRICSPATH":
                    LYRICSPATH = value;
                    break;
                case "CDTITLE":
                    CDTITLE = value;
                    break;
                case "MUSIC":
                    MUSIC = value;
                    break;
                case "OFFSET":
                    OFFSET = value;
                    break;
                case "BPMS":
                    BPMS = value;
                    break;
                case "STOPS":
                    STOPS = value;
                    break;
                case "SAMPLESTART":
                    SAMPLESTART = value;
                    break;
                case "SAMPLELENGTH":
                    SAMPLELENGTH = value;
                    break;
                case "DISPLAYBPM":
                    DISPLAYBPM = value;
                    break;
                case "SELECTABLE":
                    SELECTABLE = value;
                    break;
                case "BGCHANGES":
                    BGCHANGES = value;
                    break;
                case "FGCHANGES":
                    FGCHANGES = value;
                    break;
                case "NOTES":
                    notesList.Add(new Notes(value));
                    break;
            }
        }


        public DanceData(string fileContentText)
        {
            string tmpContent = DeleatComment(fileContentText);
            List<string> tagStringList = GetTagStringList(tmpContent);
            foreach (string str in tagStringList)
            {
                //Console.WriteLine(str);
            }

            List<Tuple<string, string>> tagValueList = GetTagValueList(tagStringList);
            setValues(tagValueList);
        }

        public override string ToString()
        {
            string result = "";

            if (TITLE != "") { result += string.Format("#TITLE:{0};\r\n", TITLE); }
            if (SUBTITLE != "") { result += string.Format("#SUBTITLE:{0};\r\n", SUBTITLE); }
            if (ARTIST != "") { result += string.Format("#ARTIST:{0};\r\n", ARTIST); }
            if (TITLETRANSLIT != "") { result += string.Format("#TITLETRANSLIT:{0};\r\n", TITLETRANSLIT); }
            if (SUBTITLETRANSLIT != "") { result += string.Format("#SUBTITLETRANSLIT:{0};\r\n", SUBTITLETRANSLIT); }
            if (ARTISTTRANSLIT != "") { result += string.Format("#ARTISTTRANSLIT:{0};\r\n", ARTISTTRANSLIT); }
            if (GENRE != "") { result += string.Format("#GENRE:{0};\r\n", GENRE); }
            if (CREDIT != "") { result += string.Format("#CREDIT:{0};\r\n", CREDIT); }
            if (BANNER != "") { result += string.Format("#BANNER:{0};\r\n", BANNER); }
            if (BACKGROUND != "") { result += string.Format("#BACKGROUND:{0};\r\n", BACKGROUND); }
            if (LYRICSPATH != "") { result += string.Format("#LYRICSPATH:{0};\r\n", LYRICSPATH); }
            if (CDTITLE != "") { result += string.Format("#CDTITLE:{0};\r\n", CDTITLE); }
            if (MUSIC != "") { result += string.Format("#MUSIC:{0};\r\n", MUSIC); }
            if (OFFSET != "") { result += string.Format("#OFFSET:{0};\r\n", OFFSET); }
            if (BPMS != "") { result += string.Format("#BPMS:{0};\r\n", BPMS); }
            if (STOPS != "") { result += string.Format("#STOPS:{0};\r\n", STOPS); }
            if (SAMPLESTART != "") { result += string.Format("#SAMPLESTART:{0};\r\n", SAMPLESTART); }
            if (SAMPLELENGTH != "") { result += string.Format("#SAMPLELENGTH:{0};\r\n", SAMPLELENGTH); }
            if (DISPLAYBPM != "") { result += string.Format("#DISPLAYBPM:{0};\r\n", DISPLAYBPM); }
            if (SELECTABLE != "") { result += string.Format("#SELECTABLE:{0};\r\n", SELECTABLE); }
            if (BGCHANGES != "") { result += string.Format("#BGCHANGES:{0};\r\n", BGCHANGES); }
            if (FGCHANGES != "") { result += string.Format("#FGCHANGES:{0};\r\n", FGCHANGES); }

            result += string.Format("\r\n");

            foreach (Notes notes in notesList)
            {
                result += notes.ToString();
            }

            return result;
        }
    }
}
