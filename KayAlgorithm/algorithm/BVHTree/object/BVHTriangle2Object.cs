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
    public class BVHTriangle2Object : BVHObject2
    {
        public Vector2 mP1;
        public Vector2 mP2;
        public Vector2 mP3;
        public Vector2 mCenter;
        public GeoAABB2 mAABB;
        public BVHTriangle2Object(Vector2 p1, Vector2 p2, Vector2 p3)
            : base(GeoShape.GeoTriangle2)
        {
            mP1 = p1;
            mP2 = p2;
            mP3 = p3;
            mCenter = (mP1 + mP2 + mP3) * 0.333333f;
            mAABB = new GeoAABB2(Vector2.Min(Vector2.Min(mP1, mP2), mP3), Vector2.Max(Vector2.Max(mP1, mP2), mP3));
        }

        override
        public Vector2 GetCenter()
        {
            return mCenter;
        }

        override
        public GeoAABB2 GetAABB()
        {
            return mAABB;
        }

        override
        public bool IsIntersect(ref GeoRay2 dist, ref GeoInsectPointArrayInfo insect)
        {
            bool isInsect = GeoRayUtils.IsRayInsectTriangle2(dist.mOrigin, dist.mDirection, mP1, mP2, mP3, ref insect);
            if (isInsect)
            {
                insect.mHitObject2 = this;
                float min = 1e5f;
                foreach (Vector3 v in insect.mHitGlobalPoint.mPointArray)
                {
                    float len = (GeoUtils.ToVector2(v) - dist.mOrigin).magnitude;
                    if (len < min)
                    {
                        min = len;
                    }
                }
                insect.mLength = min;
            }
            return isInsect;
        }
    }

}
