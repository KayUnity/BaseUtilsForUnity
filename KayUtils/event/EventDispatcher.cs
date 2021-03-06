using System;
using System.Collections.Generic;

using UnityEngine;

namespace KayUtils
{
    /// <summary>
    /// 事件处理类。
    /// </summary>
    public class EventController
    {
        private Dictionary<string, Delegate> _theRouter = new Dictionary<string, Delegate>();

        public Dictionary<string, Delegate> mTheRouter
        {
            get { return _theRouter; }
        }

        /// <summary>
        /// 永久注册的事件列表
        /// </summary>
        private List<string> mPermanentEvents = new List<string>();

        /// <summary>
        /// 标记为永久注册事件
        /// </summary>
        /// <param name="eventType"></param>
        public void MarkAsPermanent(string eventType)
        {
            mPermanentEvents.Add(eventType);
        }

        /// <summary>
        /// 判断是否已经包含事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public bool ContainsEvent(string eventType)
        {
            return _theRouter.ContainsKey(eventType);
        }

        /// <summary>
        /// 清除非永久性注册的事件
        /// </summary>
        public void Cleanup()
        {
            List<string> eventToRemove = new List<string>();

            foreach (KeyValuePair<string, Delegate> pair in _theRouter)
            {
                bool wasFound = false;
                foreach (string Event in mPermanentEvents)
                {
                    if (pair.Key == Event)
                    {
                        wasFound = true;
                        break;
                    }
                }

                if (!wasFound)
                    eventToRemove.Add(pair.Key);
            }

            foreach (string Event in eventToRemove)
            {
                _theRouter.Remove(Event);
            }
        }

        /// <summary>
        /// 处理增加监听器前的事项， 检查 参数等
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="listenerBeingAdded"></param>
        private void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
        {
            if (!_theRouter.ContainsKey(eventType))
            {
                _theRouter.Add(eventType, null);
            }

            Delegate d = _theRouter[eventType];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                throw new EventException(string.Format(
                        "Try to add not correct event {0}. Current mType is {1}, adding mType is {2}.",
                        eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        /// <summary>
        /// 移除监听器之前的检查
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="listenerBeingRemoved"></param>
        private bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
        {
            if (!_theRouter.ContainsKey(eventType))
            {
                return false;
            }

            Delegate d = _theRouter[eventType];
            if ((d != null) && (d.GetType() != listenerBeingRemoved.GetType()))
            {
                throw new EventException(string.Format(
                    "Remove listener {0}\" failed, Current mType is {1}, adding mType is {2}.",
                    eventType, d.GetType(), listenerBeingRemoved.GetType()));
            }
            else
                return true;
        }

        /// <summary>
        /// 移除监听器之后的处理。删掉事件
        /// </summary>
        /// <param name="eventType"></param>
        private void OnListenerRemoved(string eventType)
        {
            if (_theRouter.ContainsKey(eventType) && _theRouter[eventType] == null)
            {
                _theRouter.Remove(eventType);
            }
        }

        #region 增加监听器
        /// <summary>
        ///  增加监听器， 不带参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void AddEventListener(string eventType, Action handler)
        {
            OnListenerAdding(eventType, handler);
            _theRouter[eventType] = (Action)_theRouter[eventType] + handler;
        }

        /// <summary>
        ///  增加监听器， 1个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void AddEventListener<T>(string eventType, Action<T> handler)
        {
            OnListenerAdding(eventType, handler);
            _theRouter[eventType] = (Action<T>)_theRouter[eventType] + handler;
        }

        /// <summary>
        ///  增加监听器， 2个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void AddEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            OnListenerAdding(eventType, handler);
            _theRouter[eventType] = (Action<T, U>)_theRouter[eventType] + handler;
        }

        /// <summary>
        ///  增加监听器， 3个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            OnListenerAdding(eventType, handler);
            _theRouter[eventType] = (Action<T, U, V>)_theRouter[eventType] + handler;
        }

        /// <summary>
        ///  增加监听器， 4个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            OnListenerAdding(eventType, handler);
            _theRouter[eventType] = (Action<T, U, V, W>)_theRouter[eventType] + handler;
        }
        #endregion

        #region 移除监听器

        /// <summary>
        ///  移除监听器， 不带参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void RemoveEventListener(string eventType, Action handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                _theRouter[eventType] = (Action)_theRouter[eventType] - handler;
                OnListenerRemoved(eventType);
            }
        }

        /// <summary>
        ///  移除监听器， 1个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void RemoveEventListener<T>(string eventType, Action<T> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                _theRouter[eventType] = (Action<T>)_theRouter[eventType] - handler;
                OnListenerRemoved(eventType);
            }
        }

        /// <summary>
        ///  移除监听器， 2个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                _theRouter[eventType] = (Action<T, U>)_theRouter[eventType] - handler;
                OnListenerRemoved(eventType);
            }
        }

        /// <summary>
        ///  移除监听器， 3个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                _theRouter[eventType] = (Action<T, U, V>)_theRouter[eventType] - handler;
                OnListenerRemoved(eventType);
            }
        }

        /// <summary>
        ///  移除监听器， 4个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                _theRouter[eventType] = (Action<T, U, V, W>)_theRouter[eventType] - handler;
                OnListenerRemoved(eventType);
            }
        }
        #endregion

        #region 触发事件
        /// <summary>
        ///  触发事件， 不带参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void TriggerEvent(string eventType)
        {
            Delegate d;
            if (!_theRouter.TryGetValue(eventType, out d))
            {
                return;
            }

            var callbacks = d.GetInvocationList();
            for (int i = 0; i < callbacks.Length; i++)
            {
                Action callback = callbacks[i] as Action;

                if (callback == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }

                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

        /// <summary>
        ///  触发事件， 带1个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void TriggerEvent<T>(string eventType, T arg1)
        {
            Delegate d;
            if (!_theRouter.TryGetValue(eventType, out d))
            {
                return;
            }
            var callbacks = d.GetInvocationList();
            for (int i = 0; i < callbacks.Length; i++)
            {
                Action<T> callback = callbacks[i] as Action<T>;
                if (callback == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    callback(arg1);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

        /// <summary>
        ///  触发事件， 带2个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
        {
            Delegate d;
            if (!_theRouter.TryGetValue(eventType, out d))
            {
                return;
            }
            var callbacks = d.GetInvocationList();
            for (int i = 0; i < callbacks.Length; i++)
            {
                Action<T, U> callback = callbacks[i] as Action<T, U>;
                if (callback == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    callback(arg1, arg2);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

        /// <summary>
        ///  触发事件， 带3个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
        {
            Delegate d;
            if (!_theRouter.TryGetValue(eventType, out d))
            {
                return;
            }
            var callbacks = d.GetInvocationList();
            for (int i = 0; i < callbacks.Length; i++)
            {
                Action<T, U, V> callback = callbacks[i] as Action<T, U, V>;
                if (callback == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    callback(arg1, arg2, arg3);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

        /// <summary>
        ///  触发事件， 带4个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
        {
            Delegate d;
            if (!_theRouter.TryGetValue(eventType, out d))
            {
                return;
            }
            var callbacks = d.GetInvocationList();
            for (int i = 0; i < callbacks.Length; i++)
            {
                Action<T, U, V, W> callback = callbacks[i] as Action<T, U, V, W>;
                if (callback == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    callback(arg1, arg2, arg3, arg4);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 事件分发函数。
    /// 提供事件注册， 反注册， 事件触发
    /// 采用 delegate, dictionary 实现
    /// 支持自定义事件。 事件采用字符串方式标识
    /// 支持 0，1，2，3 等4种不同参数个数的回调函数
    /// </summary>
    public class EventDispatcher
    {
        private static EventController _eventController = new EventController();

        public static Dictionary<string, Delegate> mTheRouter
        {
            get { return _eventController.mTheRouter; }
        }

        /// <summary>
        /// 标记为永久注册事件
        /// </summary>
        /// <param name="eventType"></param>
        static public void MarkAsPermanent(string eventType)
        {
            _eventController.MarkAsPermanent(eventType);
        }

        /// <summary>
        /// 清除非永久性注册的事件
        /// </summary>
        static public void Cleanup()
        {
            _eventController.Cleanup();
        }

        #region 增加监听器
        /// <summary>
        ///  增加监听器， 不带参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void AddEventListener(string eventType, Action handler)
        {
            _eventController.AddEventListener(eventType, handler);
        }

        /// <summary>
        ///  增加监听器， 1个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void AddEventListener<T>(string eventType, Action<T> handler)
        {
            _eventController.AddEventListener(eventType, handler);
        }

        /// <summary>
        ///  增加监听器， 2个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void AddEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            _eventController.AddEventListener(eventType, handler);
        }

        /// <summary>
        ///  增加监听器， 3个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            _eventController.AddEventListener(eventType, handler);
        }

        /// <summary>
        ///  增加监听器， 4个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            _eventController.AddEventListener(eventType, handler);
        }
        #endregion

        #region 移除监听器
        /// <summary>
        ///  移除监听器， 不带参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void RemoveEventListener(string eventType, Action handler)
        {
            _eventController.RemoveEventListener(eventType, handler);
        }

        /// <summary>
        ///  移除监听器， 1个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void RemoveEventListener<T>(string eventType, Action<T> handler)
        {
            _eventController.RemoveEventListener(eventType, handler);
        }

        /// <summary>
        ///  移除监听器， 2个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            _eventController.RemoveEventListener(eventType, handler);
        }

        /// <summary>
        ///  移除监听器， 3个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            _eventController.RemoveEventListener(eventType, handler);
        }

        /// <summary>
        ///  移除监听器， 4个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            _eventController.RemoveEventListener(eventType, handler);
        }
        #endregion

        #region 触发事件
        /// <summary>
        ///  触发事件， 不带参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void TriggerEvent(string eventType)
        {
            _eventController.TriggerEvent(eventType);
        }

        /// <summary>
        ///  触发事件， 带1个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void TriggerEvent<T>(string eventType, T arg1)
        {
            _eventController.TriggerEvent(eventType, arg1);
        }

        /// <summary>
        ///  触发事件， 带2个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
        {
            _eventController.TriggerEvent(eventType, arg1, arg2);
        }

        /// <summary>
        ///  触发事件， 带3个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
        {
            _eventController.TriggerEvent(eventType, arg1, arg2, arg3);
        }

        /// <summary>
        ///  触发事件， 带4个参数触发
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        static public void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
        {
            _eventController.TriggerEvent(eventType, arg1, arg2, arg3, arg4);
        }
        #endregion
    }
}

