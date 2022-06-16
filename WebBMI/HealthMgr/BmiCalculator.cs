using isRock.LineBot;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthMgr
{
    public class BmiCalculator
    {
        public BmiCalculator()
        {
            this.bot = new Bot("");
        }


        public BmiCalculator(Bot bot)
        {
            this.bot = bot;
        }
        private isRock.LineBot.Bot bot { get; set; }

        public int Weight { get; set; }
        public int Height { get; set; }
        public float BMI
        {
            get
            {
                return Calculate();
            }
        }

        public delegate void TooFatWarningEventHandler(object e, float value);
        public event TooFatWarningEventHandler TooFatWarning;

        public float Calculate()
        {
            float result = 0;
            if (Weight <= 0 || Height <= 0)
                throw new Exception();
            //將float改成int試試看
            float height = (float)Height / 100;
            result = Weight / (height * height);
            //改用注入的linebot物件
            //isRock.LineBot.Bot bot = new isRock.LineBot.Bot("");
            //如果BMI>30
            if (result > 30)
            {
                if (TooFatWarning != null) TooFatWarning.Invoke(this, result);
                bot.SendNotify("ydiw9kxHigB4Cx7D9momQtstobzkrfvZGr31RqOQcC5", "test");
            }
            return result;
        }

    }
}
