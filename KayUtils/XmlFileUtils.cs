/********************************************************************************
** All rights reserved
** Auth£º kay.yang
** E-mail: 1025115216@qq.com
** Date£º 7/28/2017 2:50:22 PM
** Version:  v1.0.0
*********************************************************************************/

using UnityEngine;
using System;
using System.Collections;

namespace KayUtils
{
    public class XmlFileUtils
    {
        public static T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                     T defal = default(T);
                     SerializeToXml<T>(filePath, defal);
                     return defal;
                }
                using (System.IO.StreamReader reader = new System.IO.StreamReader(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    T ret = (T)xs.Deserialize(reader);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                string error = ex.StackTrace;
                return default(T);
            }
        }

        public static void SerializeToXml<T>(string filePath, T obj)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    xs.Serialize(writer, obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


