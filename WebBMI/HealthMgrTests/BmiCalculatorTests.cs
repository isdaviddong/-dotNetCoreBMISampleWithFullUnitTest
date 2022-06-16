using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthMgr;
using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;

namespace HealthMgr.Tests
{
    [TestClass()]
    public class BmiCalculatorTests
    {
        [TestMethod()]
        public void CalculateTest()
        {
            //arrange
            HealthMgr.BmiCalculator bmi = new HealthMgr.BmiCalculator();
            bmi.Height = 170;
            bmi.Weight = 70;
            //act
            var result = bmi.Calculate();
            //assert
            Assert.AreEqual("24.22", result.ToString("00.00"));
        }

        [TestMethod]
        public void CalculateExceptionTest()
        {
            //arange
            HealthMgr.BmiCalculator bmi = new HealthMgr.BmiCalculator();
            bmi.Height = 0;
            bmi.Weight = 0;
            //action
            //var result = bmi.Calculate();
            //Assert
            Assert.ThrowsException<Exception>(() => { bmi.Calculate(); });
        }

        [DataTestMethod]
        [DataRow(170, 70, "24.22")]
        [DataRow(165, 50, "18.37")]
        public void CalculateMultiTest(int height, int weight, string expect)
        {
            HealthMgr.BmiCalculator bmi = new HealthMgr.BmiCalculator();
            bmi.Height = height;
            bmi.Weight = weight;

            var result = bmi.Calculate();

            Assert.AreEqual(expect, result.ToString("00.00"));
        }

        [TestMethod]
        public void CalculateSendMessageTest()
        {
            var bot = NSubstitute.Substitute.For<isRock.LineBot.Bot>("");
            //arrange
            HealthMgr.BmiCalculator bmi = new HealthMgr.BmiCalculator(bot);
            bmi.Height = 170;
            bmi.Weight = 90;
            //action
            var result = bmi.Calculate();
            //Assert
            bot.Received().SendNotify("ydiw9kxHigB4Cx7D9momQtstobzkrfvZGr31RqOQcC5", "test");
        }

        [TestMethod]
        public void CalculateNotSendMessageTest()
        {
            var bot = NSubstitute.Substitute.For<isRock.LineBot.Bot>("");
            //arrange
            HealthMgr.BmiCalculator bmi = new HealthMgr.BmiCalculator(bot);
            bmi.Height = 170;
            bmi.Weight = 50;
            //action
            var result = bmi.Calculate();
            //Assert
            bot.DidNotReceive().SendNotify("ydiw9kxHigB4Cx7D9momQtstobzkrfvZGr31RqOQcC5", "test");
        }

        [TestMethod]
        public void CalculateEventRaiseTest()
        {
            bool isEventFired = false;
            //arrange
            HealthMgr.BmiCalculator bmi = new HealthMgr.BmiCalculator();
            bmi.TooFatWarning += (e, value) => { isEventFired = true; };
            bmi.Height = 170;
            bmi.Weight = 90;
            //action
            var result = bmi.Calculate();
            //Assert
            Assert.IsTrue(isEventFired);
        }
    }
}