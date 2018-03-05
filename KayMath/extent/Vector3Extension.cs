/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 8/7/2017 9:56:43 AM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KayMath
{
    public static class Vector3Extension
    {
        public static Vector3 Multiple(this Vector3 v, Vector3 scale)
        {
            return new Vector3(v[0] * scale[0], v[1] * scale[1], v[2] * scale[2]);
        }
    }
}
