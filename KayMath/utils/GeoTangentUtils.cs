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
    /// <summary>
    /// 点到 线段 圆 椭圆 三角形 多边形 矩形 多边形 的切线
    /// </summary>
    public class GeoTangentUtils
    {
        public static Vector2[] TangentToSegment(Vector2 point, Vector2 p1, Vector2 p2)
        {
            return null;
        }
        public static Vector2[] TangentToCircle(Vector2 point, Vector2 center, float r)
        {
            return null;
        }

        public static Vector2[] TangentToTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return null;
        }

        public static Vector2[] TangentToRectangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return null;
        }

        public static Vector2[] TangentToEllipse(Vector2 point, Vector2 p1, float a, float b)
        {
            return null;
        }
        public static Vector2[] TangentToPolygon(Vector2 point, GeoPointsArray2 poly)
        {
            return null;
        }

    }
}
