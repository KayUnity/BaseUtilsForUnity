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
using UnityEngine;

namespace KayMath
{
    public class GeoAABB2
    {
        public Vector2 mMin;
        public Vector2 mMax;
        public Vector2 mSize;
        public GeoAABB2()
        {

        }
        public GeoAABB2(Vector2 min, Vector2 max)
        {
            mMin = min;
            mMax = max;
            mSize = mMax - mMin;
        }

        public float Area()
        {
            Vector2 size = mMax - mMin;
            return size[0] * size[1];
        }
    }

    public class GeoAABB3
    {
        public Vector3 mMin;
        public Vector3 mMax;
        public Vector3 mSize;
        public GeoAABB3()
        {
        }

        public GeoAABB3(Vector3 min, Vector3 max)
        {
            mMin = min;
            mMax = max;
            mSize = mMax - mMin;
        }
    }
}
