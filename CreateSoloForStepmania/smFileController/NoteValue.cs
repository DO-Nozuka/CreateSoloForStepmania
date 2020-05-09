using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSoloForStepmania
{
    class Step
    {
        public int numerator { get; private set; }      //分子(192分単位)
        public int denominator { get; private set; }    //分母
        public string arrow { get; private set; }       //矢印

        public Step(int numerator, int denominator, string arrow)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            this.arrow = arrow;
        }
    }

    class Measure
    {
        public int measureNum { get; private set; }
        public List<Step> steps { get; private set; }

        public Measure(int measureNum)
        {
            this.measureNum = measureNum;
            steps = new List<Step>();
        }

        public Measure(int measureNum, string measureValue)
        {
            this.measureNum = measureNum;

            string[] separator = new string[1];
            separator[0] = "\r\n";
            string[] s = measureValue.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            steps = new List<Step>();
            for (int i = 0; i < s.Count(); i++)
            {
                steps.Add(new Step(i + 1, s.Count(), s[i]));
            }
        }
    }

    class NoteValue
    {
        public List<Measure> measures { get; private set; }
        public int currentMeasureNum { get; private set; }
        public int currentStepNum { get; private set; }
        public bool isEndOfSteps { get; private set; }

        public NoteValue()
        {
            measures = new List<Measure>();
            resetCurrent();
        }

        public NoteValue(string value)
        {
            string[] noteValue;

            noteValue = value.Split(",".ToCharArray());
            for (int i = 0; i < noteValue.Count(); i++)
            {
                //noteValueの 無駄な文字の処理
                while (noteValue[i].StartsWith(" ") || noteValue[i].StartsWith("\r") || noteValue[i].StartsWith("\n"))
                {
                    noteValue[i] = noteValue[i].Remove(0, 1);
                }
            }

            measures = new List<Measure>();
            for (int i = 0; i < noteValue.Count(); i++)
            {
                if (noteValue[i].Count() >= 16)
                {
                    measures.Add(new Measure(i, noteValue[i]));
                }
            }

            resetCurrent();
        }


        public void resetCurrent()
        {
            currentMeasureNum = 0;
            currentStepNum = 0;
            isEndOfSteps = false;
        }

        public Step GetNextStep()
        {
            int numerator = measures[currentMeasureNum].steps[currentStepNum].numerator;
            int denominator = measures[currentMeasureNum].steps[currentStepNum].denominator;
            string arrow = measures[currentMeasureNum].steps[currentStepNum].arrow;
            Step result = new Step(numerator, denominator, arrow);

            if (isEndOfSteps)
            {
                return null;
            }

            currentStepNum++;
            if (currentStepNum == measures[currentMeasureNum].steps.Count())
            {
                currentStepNum = 0;
                currentMeasureNum++;
                if (currentMeasureNum == measures.Count())
                {
                    isEndOfSteps = true;
                }
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";

            //値の保存
            int savedCurrentMeasure = currentMeasureNum;
            int savedCurrentStep = currentStepNum;
            bool savedIsEndOfSteps = isEndOfSteps;

            //リセット
            resetCurrent();

            //値の格納
            while (!isEndOfSteps)
            {
                int measure = currentMeasureNum;
                int step = currentStepNum;

                Step s = GetNextStep();

                result += string.Format("{0}\r\n", s.arrow);
                //result += string.Format("({0:d3},{1:d2}) {2}\r\n", measure + 1, step + 1, s.arrow);

                if (isEndOfSteps)
                {
                }
                else if ((step + 1) == measures[measure].steps.Count())
                {
                    result += ",\r\n";
                }
            }

            //保存した値の復元
            currentMeasureNum = savedCurrentMeasure;
            currentStepNum = savedCurrentStep;
            isEndOfSteps = savedIsEndOfSteps;

            return result;
        }
    }
}
