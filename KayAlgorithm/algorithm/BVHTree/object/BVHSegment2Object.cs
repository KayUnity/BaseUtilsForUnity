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
    public class BVHSegment2Object : BVHObject2
    {
        public GeoSegment2 mSeg;
        public Vector2 mCenter;
        public GeoAABB2 mAABB;
        public BVHSegment2Object(Vector2 p1, Vector2 p2)
            : base(GeoShape.GeoSegment2)
        {
            mSeg = new GeoSegment2(p1, p2);
            mCenter = (p1 + p2) * 0.5f;
            mAABB = new GeoAABB2(Vector2.Min(p1, p2), Vector2.Max(p1, p2));
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
        public bool TestAABBIntersect(GeoAABB2 aabb)
        {
            GeoInsectPointArrayInfo insect = new GeoInsectPointArrayInfo();
            return GeoSegmentUtils.IsSegmentInsectAABB2(mSeg.mP1, mSeg.mP2, aabb.mMin, aabb.mMax, ref insect);
        }

        override
        public bool IsIntersect(ref GeoRay2 dist, ref GeoInsectPointArrayInfo insect)
        {
            GeoInsectPointInfo info = new GeoInsectPointInfo();
            bool isInsect = GeoRayUtils.IsRayInsectSegment2(dist.mOrigin, dist.mDirection, mSeg.mP1, mSeg.mP2, ref info);
            insect.mIsIntersect = isInsect;
            if (isInsect)
            {
                insect.mHitObject2 = this;
                insect.mHitGlobalPoint.mPointArray.Add(info.mHitGlobalPoint);
                insect.mLength = (GeoUtils.ToVector2(info.mHitGlobalPoint) - dist.mOrigin).magnitude;
            }
            return isInsect;
        }
    }
}
