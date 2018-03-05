/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 7/26/2017 3:57:46 PM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using NetCommand;
namespace NetworkWrapper
{
    public abstract class NetworkCommand
    {
        public abstract void HandlePacket(NetworkPacket packet);

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class CommandTypeAttribute : Attribute
    {
        int mId;
        string mDescription;
        public CommandTypeAttribute(int id, string desc)
        {
            mId = id;
            mDescription = desc;
        }
        public CommandTypeAttribute()
        {

        }
        public int ID
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }

        public string Description
        {
            get
            {
                return mDescription;
            }
            set
            {
                mDescription = value;
            }
        }

    }

    public class NetworkCommandFactory
    {
        static Dictionary<int, Type> mAllCommandClasses = new Dictionary<int, Type>();
        static bool isRegistered = false;

        public static void RegisterCommand()
        {
            if (!isRegistered)
            {
                mAllCommandClasses.Add(CommandType.HeartCodec, typeof(HeartCommand));
                mAllCommandClasses.Add(CommandType.GM, typeof(GMCommand));
                isRegistered = true;
            }
        }
        public static void RegisterCommand(string spacename, Assembly ass)
        {
            if (!isRegistered)
            {
                var types = ass.GetTypes();
                foreach (var item in types)
                {
                    if (item.Namespace == spacename)
                    {
                        var type = item.BaseType;
                        while (type != null)
                        {
                            if (type == typeof(NetworkCommand))
                            {
                                CommandTypeAttribute attr = CommandTypeAttribute.GetCustomAttribute(item, typeof(CommandTypeAttribute), false) as CommandTypeAttribute;
                                if (!mAllCommandClasses.ContainsKey(attr.ID))
                                {
                                    mAllCommandClasses.Add(attr.ID, item);
                                }
                                break;
                            }
                            else
                            {
                                type = type.BaseType;
                            }
                        }
                    }
                }
                isRegistered = true;
            }
        }

        public static NetworkCommand GetCommand(int mid)
        {
            if (mAllCommandClasses.ContainsKey(mid))
            {
                return (NetworkCommand)Activator.CreateInstance(mAllCommandClasses[mid]);
            }
            return null;
        }
    }

    /// <summary>
    /// should initialize firstly
    /// </summary>
    public class NetworkCommandHandler
    {
        public static NetworkCommandHandler Instance = new NetworkCommandHandler();
        static object mLock = new object();
        Queue<NetworkPacket> mCommandPacket;
        static EventWaitHandle mWaitHandle;
        public Thread mHandleThread = null;
        static bool isClose;
        static bool isInitialized = false;

        private NetworkCommandHandler()
        {

        }
        public void Initialize()
        {
            if (!isInitialized)
            {
                NetworkCommandFactory.RegisterCommand();
                isInitialized = true;
                mWaitHandle = new AutoResetEvent(false);
                mCommandPacket = new Queue<NetworkPacket>();
                isClose = false;
                mHandleThread = new Thread(HandlePacket) { IsBackground = true };
                mHandleThread.Start();
            }
        }
        public void Initialize(string spacename, Assembly ass)
        {
            if (!isInitialized)
            {
                NetworkCommandFactory.RegisterCommand(spacename, ass);
                isInitialized = true;
                mWaitHandle = new AutoResetEvent(false);
                mCommandPacket = new Queue<NetworkPacket>();
                isClose = false;
                mHandleThread = new Thread(HandlePacket) { IsBackground = true };
                mHandleThread.Start();
            }
        }

        public void Close()
        {
            mWaitHandle.Set();
            isClose = true;
            mHandleThread.Join();
            mWaitHandle.Close();
            mCommandPacket.Clear();
        }

        public void HandlePacket()
        {
            while (!isClose)
            {
                NetworkPacket packet = null;
                lock (mLock)
                {
                    if (mCommandPacket.Count > 0)
                    {
                        packet = mCommandPacket.Dequeue();
                    }
                }
                if (packet != null)
                {
                    Handle(packet);
                }
                else
                {
                    mWaitHandle.WaitOne();
                }
            }

        }

        private void Handle(NetworkPacket packet)
        {
            NetworkCommand command = NetworkCommandFactory.GetCommand(packet.mHead.mType);
            if (command != null)
            {
                command.HandlePacket(packet);
            }
        }

        public void AddPacket(NetworkPacket packet)
        {
            lock (mLock)
            {
                mCommandPacket.Enqueue(packet);
            }
            mWaitHandle.Set();
        }
    }



}
