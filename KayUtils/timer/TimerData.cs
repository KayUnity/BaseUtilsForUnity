using System;

namespace KayUtils
{
    public abstract class AbstractTimerData
    {
        private uint _timerId;
        private ulong _nextTick;
        private int _interval;

        public uint mTimerId
        {
            get { return _timerId; }
            set { _timerId = value; }
        }

        public int mInterval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public ulong mNextTick
        {
            get { return _nextTick; }
            set { _nextTick = value; }
        }

        public abstract Delegate mAction
        {
            get;
            set;
        }

        public abstract void DoAction();
    }

    public class TimerData : AbstractTimerData
    {
        private Action _action;

        public override Delegate mAction
        {
            get { return _action; }
            set { _action = value as Action; }
        }

        public override void DoAction()
        {
            _action();
        }
    }

    public class TimerData<T> : AbstractTimerData
    {
        private Action<T> _action;
        private T _arg1;

        public T mArg1
        {
            get { return _arg1; }
            set { _arg1 = value; }
        }
        public override Delegate mAction
        {
            get { return _action; }
            set { _action = value as Action<T>; }
        }

        public override void DoAction()
        {
            _action(_arg1);
        }
    }

    public class TimerData<T, U> : AbstractTimerData
    {
        private Action<T, U> _action;
        private T _arg1;
        private U _arg2;

        public T mArg1
        {
            get { return _arg1; }
            set { _arg1 = value; }
        }

        public U mArg2
        {
            get { return _arg2; }
            set { _arg2 = value; }
        }
        public override Delegate mAction
        {
            get { return _action; }
            set { _action = value as Action<T, U>; }
        }

        public override void DoAction()
        {
            _action(_arg1, _arg2);
        }
    }

    public class TimerData<T, U, V> : AbstractTimerData
    {
        private Action<T, U, V> _action;
        private T _arg1;
        private U _arg2;
        private V _arg3;

        public T mArg1
        {
            get { return _arg1; }
            set { _arg1 = value; }
        }

        public U mArg2
        {
            get { return _arg2; }
            set { _arg2 = value; }
        }

        public V mArg3
        {
            get { return _arg3; }
            set { _arg3 = value; }
        }

        public override Delegate mAction
        {
            get { return _action; }
            set { _action = value as Action<T, U, V>; }
        }

        public override void DoAction()
        {
            _action(_arg1, _arg2, _arg3);
        }
    }

}
