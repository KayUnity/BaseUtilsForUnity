/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 6/30/2017 11:13:04 AM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KayAlgorithm
{
    public abstract class ObjectHash
    {
        public abstract string String();
    }


    // 适合 做 大量数据 的 合并； 比如 合并 顶点
    // 当然，分组 也是可以的
    public class GroupHash<T> where T : ObjectHash
    {
        public GroupHash()
        {
            mObjects = new List<T>();
            mGroups = new Dictionary<string, List<int>>();
        }

        public void Add(T t)
        {
            int index = mObjects.Count;
            mObjects.Add(t);
            string str = t.String();
            if (!mGroups.ContainsKey(str))
            {
                mGroups.Add(str, new List<int>());
            }
            mGroups[str].Add(index);
        }

        public List<List<T>> GetResult()
        {
            List<List<T>> groupResult = new List<List<T>>();
            foreach (List<int> group in mGroups.Values)
            {
                List<T> tmp = new List<T>();
                foreach (int idx in group)
                {
                    tmp.Add(mObjects[idx]);
                }
                groupResult.Add(tmp);
            }
            return groupResult;
        }

        private List<T> mObjects;
        private Dictionary<string, List<int>> mGroups;
    }
}
