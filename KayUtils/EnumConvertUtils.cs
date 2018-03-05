/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 8/5/2017 5:28:02 PM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KayUtils
{
    public class EnumConvertUtils
    {
        public static string EnumToString<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }
        public static T StringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static int EnumToInt<T>(T value)
        {
            return (int)(System.Object)value;
        }
        public static T IntToEnum<T>(int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static string[] GetNames(Type enumType)
        {
            return Enum.GetNames(enumType);
        }

        public static bool IsDefined(Type enumType, Object value)
        {
            return Enum.IsDefined(enumType, value);
        }
    }
}
