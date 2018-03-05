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
    public class GeoCircle2
    {
        public Vector2 mCenter;
        public float mRadius;

        public GeoCircle2(Vector2 center, float r)
        {
            mCenter = center;
            mRadius = r;
        }
    }

    public class GeoCircle3
    {
        public Vector3 mCenter;
        public float mRadius;
        // private GeoPlane mPlane;
        public GeoCircle3(Vector3 center, float r, Vector3 normal)
        {
            mCenter = center;
            mRadius = r;
        }

    }
}
