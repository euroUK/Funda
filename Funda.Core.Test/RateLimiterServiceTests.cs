using Funda.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;

namespace Funda.Core.Test
{
    [TestClass]
    public class RateLimiterServiceTests
    {
        private RateLimiterService _subject = null!;

        [TestInitialize]
        public void Init()
        {
            _subject = new RateLimiterService();
        }

        [TestMethod]
        public async Task WaitForAny_OneServiceCalledLessThenLimitTimes_TotalTimeLessThanInterval()
        {
            const string serviceName = "testService1";
            const int limit = 10;
            const int interval = 100;

            _subject.Register(serviceName, limit, interval);

            var elapsedMilliseconds = await ExecuteAndMeasureTime(() => _subject.WaitForAny(serviceName), limit - 1);

            elapsedMilliseconds.Should().BeLessThan(interval);
        }

        [TestMethod]
        public async Task WaitForAny_OneServiceCalledExactlyLimitTimes_TotalTimeLessThanInterval()
        {
            const string serviceName = "testService1";
            const int limit = 10;
            const int interval = 100;

            _subject.Register(serviceName, limit, interval);

            var elapsedMilliseconds = await ExecuteAndMeasureTime(() => _subject.WaitForAny(serviceName), limit);

            elapsedMilliseconds.Should().BeLessThan(interval);
        }

        [TestMethod]
        public async Task WaitForAny_OneServiceCalledMoreThanLimitTimes_TotalTimeGreaterThanInterval()
        {
            const string serviceName = "testService1";
            const int limit = 10;
            const int interval = 100;

            _subject.Register(serviceName, limit, interval);

            var elapsedMilliseconds = await ExecuteAndMeasureTime(() => _subject.WaitForAny(serviceName), limit + 1);

            elapsedMilliseconds.Should().BeGreaterOrEqualTo(interval);
        }

        [TestMethod]
        public async Task WaitForAny_TwoServicesCalledExactlyLimitTimesEach_TotalTimeIsLessThanInterval()
        {
            const string serviceName1 = "testService1";
            const int limit1 = 5;
            const int interval1 = 200;
            _subject.Register(serviceName1, limit1, interval1);

            const string serviceName2 = "testService2";
            const int limit2 = 10;
            const int interval2 = 100;
            _subject.Register(serviceName2, limit2, interval2);

            var elapsedMilliseconds = await ExecuteAndMeasureTime(
                async () =>
                {
                    await Repeat(async () => await _subject.WaitForAny(serviceName1), limit1);
                    await Repeat(async () => await _subject.WaitForAny(serviceName2), limit2);
                }
            );

            elapsedMilliseconds.Should().BeLessThan(interval1).And.BeLessThan(interval2);
        }

        [TestMethod]
        public async Task WaitForAny_FirstCalledExactlyLimitAndSecondMoreThanLimitTimes_OnlySecondServiceBlocked()
        {
            const string serviceName1 = "testService1";
            const int limit1 = 5;
            const int interval1 = 200;
            _subject.Register(serviceName1, limit1, interval1);

            const string serviceName2 = "testService2";
            const int limit2 = 5;
            const int interval2 = 100;
            _subject.Register(serviceName2, limit2, interval2);

            var elapsedMilliseconds = await ExecuteAndMeasureTime(
                async () =>
                {
                    await Repeat(async () => await _subject.WaitForAny(serviceName1), limit1);
                    await Repeat(async () => await _subject.WaitForAny(serviceName2), limit2 + 1);
                }
            );

            elapsedMilliseconds.Should().BeLessThan(interval1).And.BeGreaterOrEqualTo(interval2);
        }

        [TestMethod]
        public async Task WaitForAny_BothServicesCalledMoreThanLimitTimes_BothServicesBlocked()
        {
            const string serviceName1 = "testService1";
            const int limit1 = 5;
            const int interval1 = 200;
            _subject.Register(serviceName1, limit1, interval1);

            const string serviceName2 = "testService2";
            const int limit2 = 5;
            const int interval2 = 100;
            _subject.Register(serviceName2, limit2, interval2);

            var elapsedMilliseconds = await ExecuteAndMeasureTime(
                async () =>
                {
                    await Repeat(async () => await _subject.WaitForAny(serviceName1), limit1 + 1);
                    await Repeat(async () => await _subject.WaitForAny(serviceName2), limit2 + 1);
                }
            );

            elapsedMilliseconds.Should().BeGreaterOrEqualTo(interval1 + interval2);
        }

        private static async Task Repeat(Func<Task> action, int times)
        {
            for (var i = 1; i <= times; i++)
            {
                await action();
            }
        }

        private static async Task<long> ExecuteAndMeasureTime(Func<Task> action, int times)
        {
            return await ExecuteAndMeasureTime(
                async () => { await Repeat(async () => await action(), times); }
            );
        }

        private static async Task<long> ExecuteAndMeasureTime(Func<Task> action)
        {
            var stopwatch = Stopwatch.StartNew();

            await action();

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }
}