using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UTest
{
    [TestFixture]
    public class UpdateableSpinTests
    {
        [Test]
        public void Wait_NoPulse_ReturnFalse()
        {
            UpdateableSpin spin = new UpdateableSpin();
            bool wasPulsed = spin.Wait(TimeSpan.FromMilliseconds(10));
            Assert.IsFalse(wasPulsed);
        }

        [Test]
        public void Wait_Pulse_ReturnsTrue()
        {
            UpdateableSpin spin = new UpdateableSpin();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                spin.Set();
            });

            bool wasPulsed = spin.Wait(TimeSpan.FromMilliseconds(10));
            Assert.IsFalse(wasPulsed);
        }

        [Test]
        public void Wait50Milisec_CallsIsActuallyWaitingFor50Milisec()
        {
            var spin = new UpdateableSpin();

            Stopwatch objStopwatch = new Stopwatch();
            objStopwatch.Start();

            spin.Wait(TimeSpan.FromMilliseconds(50));

            objStopwatch.Stop();

            TimeSpan actual = TimeSpan.FromMilliseconds(objStopwatch.ElapsedMilliseconds);
            TimeSpan leftEpsilon = TimeSpan.FromMilliseconds(50 - (50 * 0.1));
            TimeSpan rightEpsilon = TimeSpan.FromMilliseconds(50 + (50 * 0.1));

            Assert.IsTrue(actual > leftEpsilon && actual < rightEpsilon);
        }

        [Test]
        public void Wait50Milisec_UpdateAfter300Millisec_TotalWaitingIsApprox800Millisec()
        {
            var spin = new UpdateableSpin();

            Stopwatch objStopwatch = new Stopwatch();
            objStopwatch.Start();

            const int timeout = 500;
            const int spanBeforeUpdate = 300;

            Task.Factory.StartNew(()=> 
            {
                Thread.Sleep(spanBeforeUpdate);
                spin.UpdateTimeout();
            });

            spin.Wait(TimeSpan.FromMilliseconds(timeout));

            objStopwatch.Stop();

            TimeSpan actual = TimeSpan.FromMilliseconds(objStopwatch.ElapsedMilliseconds);
            const int expected = timeout + spanBeforeUpdate;

            TimeSpan left = TimeSpan.FromMilliseconds(expected - (expected * 0.1));
            TimeSpan right = TimeSpan.FromMilliseconds(expected + (expected * 0.1));

            Assert.IsTrue(actual > left && actual < right);
        }
    }

    public class UpdateableSpin
    {
        readonly object lockObj = new object();
        bool shouldWait = true;
        long executionStartingTime;

        public bool Wait(TimeSpan timeout, int spinDuration = 0)
        {
            UpdateTimeout();
            while (true)
            {
                lock (lockObj)
                {
                    if (!shouldWait)
                        return true;
                    if(DateTime.UtcNow.Ticks - executionStartingTime > timeout.Ticks)
                    return false;
                }
                Thread.Sleep(spinDuration);
            }
        }

        public void Set()
        {
            lock (lockObj)
                shouldWait = false;
        }

        internal void UpdateTimeout()
        {
            lock (lockObj)
                executionStartingTime = DateTime.UtcNow.Ticks;
        }
    }
}
