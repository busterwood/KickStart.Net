﻿using KickStart.Net.Metrics;
using NSubstitute;
using NUnit.Framework;

namespace KickStart.Net.Tests.Metrics
{
    [TestFixture]
    public class MeterTests
    {
        private IClock _clock;
        private Meter _meter;

        [SetUp]
        public void SetUp()
        {
            _clock = Substitute.For<IClock>();
            _clock.Tick.Returns(0L, 0L, TimeUnits.Seconds.ToTicks(10)); // First 0 is for the constructor
            _meter = new Meter(_clock);
        }

        [Test]
        public void test_with_zero_rate()
        {
            Assert.AreEqual(0, _meter.Count);
            Assert.AreEqual(0.0, _meter.MeanRate, 0.001);
            Assert.AreEqual(0.0, _meter.OneMinuteRate, 0.001);
            Assert.AreEqual(0.0, _meter.FiveMinutesRate, 0.001);
            Assert.AreEqual(0.0, _meter.FifteenMinutesRate, 0.001);
        }

        [Test]
        public void test_mark()
        {
            _meter.Mark();
            _meter.Mark(2);

            Assert.AreEqual(0.3, _meter.MeanRate, 0.001);
            Assert.AreEqual(0.1840, _meter.OneMinuteRate, 0.001);
            Assert.AreEqual(0.19666, _meter.FiveMinutesRate, 0.001);
            Assert.AreEqual(0.1988, _meter.FifteenMinutesRate, 0.001);
        }
    }
}
