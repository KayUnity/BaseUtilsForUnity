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
    // maybe use Matrix4x4 instead Matrix2x3 and Matrix2x2
    public class Matrix2x3
    {
        Vector3 mFirst;
        Vector3 mLast;
        public Matrix2x3(Vector2 one, Vector2 two)
        {
            mFirst = new Vector3(one[0], one[1], 0.0f);
            mLast = new Vector3(two[0], two[1], 0.0f);
        }
        public Vector2 Translate
        {
            set
            {
                mFirst[2] = value[0];
                mLast[2] = value[1];
            }
            get
            {
                return new Vector2(mFirst[2], mLast[2]);
            }
        }

    }

    public class Matrix2x2
    {
        Vector2 mFirst;
        Vector2 mLast;
        Vector2 mTranslate;
        Vector2 mScale;
        public Matrix2x2(Vector2 one, Vector2 two)
        {
            mFirst = one;
            mLast = two;
            mTranslate = new Vector2(0.0f, 0.0f);
            mScale = new Vector2(1.0f, 1.0f);
        }

        public float Det()
        {
            return mFirst[0] * mLast[1] - mFirst[1] * mLast[0];
        }

        public Vector2 Scale
        {
            set
            {
                mScale = value;
            }
            get
            {
                return mScale;
            }
        }
        public Vector2 Translate
        {
            set
            {
                mTranslate = value;
            }
            get
            {
                return mTranslate;
            }
        }

        public Vector2 MultiplePoint(Vector2 point)
        {
            Vector2 temp = new Vector2(point.x, point.y);
            temp.Scale(mScale);
            temp[0] = Vector2.Dot(mFirst, temp);
            temp[1] = Vector2.Dot(mLast, temp);
            return temp + mTranslate;
        }
        public Vector2 Rotate(Vector2 point)
        {
            Vector2 temp = new Vector2(Vector2.Dot(mFirst, point), Vector2.Dot(mLast, point));
            return temp;
        }
        // just for rotation
        public Matrix2x2 Inverse()
        {
            Vector2 newOne = new Vector2(mLast[1], -mFirst[1]);
            Vector2 newTwo = new Vector2(-mLast[0], mFirst[0]);
            float rdet = 1.0f / Det();
            Matrix2x2 inv = new Matrix2x2(newOne * rdet, newTwo * rdet);
            return inv;
        }
    }
}
