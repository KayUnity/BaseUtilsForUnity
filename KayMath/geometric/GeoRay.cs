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
    public class GeoRay2
    {
        public Vector2 mOrigin;
        public Vector2 mDirection;

        public GeoRay2(Vector2 origin, Vector2 direction)
        {
            mOrigin = origin;
            mDirection = direction;
        }
    }

    public class GeoRay3
    {
        public Vector3 mOrigin;
        public Vector3 mDirection;

        public GeoRay3(Vector3 origin, Vector3 direction)
        {
            mOrigin = origin;
            mDirection = direction;
        }
    }
}
