﻿/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 7/26/2017 3:57:46 PM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkWrapper
{
    public abstract class NetworkMessage
    {
        protected byte[] mCacheBytes = null;
        int mReadIndex = 0;
        protected List<Byte> mSendBytes = new List<byte>();

        public NetworkMessage()
        {
            Initialize();
        }

        public abstract void ReadPacket();
        public abstract void WritePacket();
        public abstract void Initialize();

        public virtual void InitializeBy(byte[] bytes)
        {
            mReadIndex = 0;
            mSendBytes.Clear();
            mCacheBytes = bytes;
            ReadPacket();
        }

        public void WriteLength()
        {
            byte[] size = System.BitConverter.GetBytes(mSendBytes.Count);
            for (int i = size.Length - 1; i >= 0; --i)
            {
                mSendBytes.Insert(0, size[i]);
            }
        }

        public Int16 ReadInt16()
        {
            if (CanRead(sizeof(Int16)))
            {
                Int16 value = System.BitConverter.ToInt16(mCacheBytes, mReadIndex);
                mReadIndex += sizeof(Int16);
                return value;
            }
            return Int16.MaxValue;
        }

        public UInt16 ReadUInt16()
        {
            if (CanRead(sizeof(Int16)))
            {
                UInt16 value = System.BitConverter.ToUInt16(mCacheBytes, mReadIndex);
                mReadIndex += sizeof(Int16);
                return value;
            }
            return UInt16.MaxValue;
        }

        public void WriteUInt16(UInt16 value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public void WriteInt16(Int16 value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public Int32 ReadInt32()
        {
            if (CanRead(sizeof(Int32)))
            {
                int value = System.BitConverter.ToInt32(mCacheBytes, mReadIndex);
                mReadIndex += sizeof(Int32);
                return value;
            }
            return Int32.MaxValue;
        }

        public void WriteInt32(int value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public Int64 ReadInt64()
        {
            if (CanRead(sizeof(Int64)))
            {
                Int64 value = System.BitConverter.ToInt64(mCacheBytes, mReadIndex);
                mReadIndex += sizeof(Int64);
                return value;
            }
            return Int64.MaxValue;
        }

        public void WriteInt64(Int64 value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public void WriteBoolean(bool value)
        {
            byte b = (byte)(value ? 1 : 0);
            mSendBytes.Add(b);
        }

        public Boolean ReadBoolean()
        {
            if (CanRead(sizeof(Boolean)))
            {
                Boolean value = System.BitConverter.ToBoolean(mCacheBytes, mReadIndex);
                mReadIndex += sizeof(Boolean);
                return value;
            }
            return false;
        }

        public float ReadFloat()
        {
            if (CanRead(sizeof(float)))
            {
                float value = System.BitConverter.ToSingle(mCacheBytes, mReadIndex);
                mReadIndex += sizeof(float);
                return value;
            }
            return float.MaxValue;
        }

        public void WriteFloat(float value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public double ReadDouble()
        {
            if (CanRead(sizeof(double)))
            {
                double value = System.BitConverter.ToDouble(mCacheBytes, mReadIndex);
                mReadIndex += sizeof(double);
                return value;
            }
            return double.MaxValue;
        }

        public void WriteDouble(double value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public string ReadWString()
        {
            int len = ReadInt32();
            if (len != Int32.MaxValue)
            {
                byte[] bytes = ReadBytes(len);
                if (bytes != null)
                {
                    return Encoding.Unicode.GetString(bytes);
                }
            }
            return null;
        }

        public string ReadString()
        {
            int len = ReadInt32();
            if (len != Int32.MaxValue)
            {
                byte[] bytes = ReadBytes(len);
                if (bytes != null)
                {
                    return Encoding.UTF8.GetString(bytes);
                }
            }
            return null;
        }

        public byte[] ReadBytes(int len)
        {
            if (CanRead(len))
            {
                byte[] bytes = new byte[len];
                Array.Copy(mCacheBytes, mReadIndex, bytes, 0, len);
                mReadIndex += len;
                return bytes;
            }
            return null;
        }

        public void WriteBytes(byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public void WriteString(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            WriteInt32(bytes.Length);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public void WriteWString(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            WriteInt32(bytes.Length);
            foreach (byte b in bytes)
            {
                mSendBytes.Add(b);
            }
        }

        public bool CanRead(int len)
        {
            if (mReadIndex + len <= mCacheBytes.Length)
            {
                return true;
            }
            return false;
        }

    }
}



