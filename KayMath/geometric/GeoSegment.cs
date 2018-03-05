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
    public class GeoSegment2
    {
        public Vector2 mP1;
        public Vector2 mP2;
        public GeoSegment2(Vector2 p1, Vector2 p2)
        {
            mP1 = p1;
            mP2 = p2;
        }

        public bool Equal(GeoSegment2 other)
        {
            return (mP1 == other.mP1 && mP2 == other.mP2) || (mP1 == other.mP2 && mP2 == other.mP1);
        }
    }

    public class GeoSegment3
    {
        public Vector3 mP1;
        public Vector3 mP2;
        public GeoSegment3(Vector3 p1, Vector3 p2)
        {
            mP1 = p1;
            mP2 = p2;
        }

        public bool Equal(GeoSegment3 other)
        {
            return (mP1 == other.mP1 && mP2 == other.mP2) || (mP1 == other.mP2 && mP2 == other.mP1);
        }
    }
}
