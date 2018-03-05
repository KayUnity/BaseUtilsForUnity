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
using KayMath;

namespace KayAlgorithm
{

    public class BVHObject2 : GeometricObject
    {
        public BVHObject2() : base()
        {

        }
        public BVHObject2(GeoShape shape) : base(shape)
        {

        }

        // AABB 包围盒
        public virtual GeoAABB2 GetAABB()
        {
            return null;
        }

        public virtual Vector2 GetCenter()
        {
            return MAX_VECTOR2;
        }

        public virtual bool TestAABBIntersect(GeoAABB2 aabb)
        {
            return false;
        }

    }

    public class BVHObject3 : GeometricObject
    {
        public BVHObject3() : base()
        {

        }
        public BVHObject3(GeoShape shape)
            : base(shape)
        {

        }

        // 中心点
        public virtual Vector3 GetCenter()
        {
            return MAX_VECTOR3;
        }

        // AABB 包围盒
        public virtual GeoAABB3 GetAABB()
        {
            return null;
        }
    }
}
