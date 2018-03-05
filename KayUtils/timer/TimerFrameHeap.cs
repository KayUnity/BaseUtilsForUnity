using System;
using UnityEngine;

using KayAlgorithm;
using KayDatastructure;
namespace KayUtils
{
    public class TimerTaskQueue
    {
        private static uint mNextTimerId;
        private static uint mCurrentTick;
        private static KeyedPriorityQueue<uint, AbstractTimerData, ulong> mPriorityQueue;
        private static readonly object mQueueLock = new object();

        private TimerTaskQueue() { }

        static TimerTaskQueue()
        {
            mPriorityQueue = new KeyedPriorityQueue<uint, AbstractTimerData, ulong>();
        }

        public static uint AddTimer(uint start, int interval, Action handler)
        {
            var p = GetTimerData(new TimerData(), start, interval);
            p.mAction = handler;
            return AddTimer(p);
        }

        public static uint AddTimer<T>(uint start, int interval, Action<T> handler, T arg1)
        {
            var p = GetTimerData(new TimerData<T>(), start, interval);
            p.mAction = handler;
            p.mArg1 = arg1;
            return AddTimer(p);
        }

        public static uint AddTimer<T, U>(uint start, int interval, Action<T, U> handler, T arg1, U arg2)
        {
            var p = GetTimerData(new TimerData<T, U>(), start, interval);
            p.mAction = handler;
            p.mArg1 = arg1;
            p.mArg2 = arg2;
            return AddTimer(p);
        }

        public static uint AddTimer<T, U, V>(uint start, int interval, Action<T, U, V> handler, T arg1, U arg2, V arg3)
        {
            var p = GetTimerData(new TimerData<T, U, V>(), start, interval);
            p.mAction = handler;
            p.mArg1 = arg1;
            p.mArg2 = arg2;
            p.mArg3 = arg3;
            return AddTimer(p);
        }

        public static void DelTimer(uint timerId)
        {
            lock (mQueueLock)
                mPriorityQueue.Remove(timerId);
        }

        public static void Tick()
        {
            mCurrentTick += (uint)(1000 * Time.deltaTime);
            while (mPriorityQueue.Count != 0)
            {
                AbstractTimerData p;
                lock (mQueueLock)
                    p = mPriorityQueue.Peek();
                if (mCurrentTick < p.mNextTick)
                {
                    break;
                }
                lock (mQueueLock)
                    mPriorityQueue.Dequeue();
                if (p.mInterval > 0)
                {
                    p.mNextTick += (ulong)p.mInterval;
                    lock (mQueueLock)
                        mPriorityQueue.Enqueue(p.mTimerId, p, p.mNextTick);
                    p.DoAction();
                }
                else
                {
                    p.DoAction();
                }
            }
        }

        public static void Reset()
        {
            mCurrentTick = 0;
            mNextTimerId = 0;
            lock (mQueueLock)
                while (mPriorityQueue.Count != 0)
                    mPriorityQueue.Dequeue();
        }

        private static uint AddTimer(AbstractTimerData p)
        {
            lock (mQueueLock)
                mPriorityQueue.Enqueue(p.mTimerId, p, p.mNextTick);
            return p.mTimerId;
        }

        private static T GetTimerData<T>(T p, uint start, int interval) where T : AbstractTimerData
        {
            p.mInterval = interval;
            p.mTimerId = ++mNextTimerId;
            p.mNextTick = mCurrentTick + 1 + start;
            return p;
        }
    }
}
