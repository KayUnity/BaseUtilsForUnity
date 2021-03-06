﻿/********************************************************************************
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
    public class GeoUtils
    {
        public static Vector3 ToVector2(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0.0f);
        }
        public static Vector2 ToVector2(Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }
        public static GeoAABB2 GetAABB(List<Vector2> points)
        {
            GeoAABB2 aabb = new GeoAABB2();
            if (points.Count > 0)
            {
                aabb.mMin = points[0];
                aabb.mMax = points[0];
                for (int i = 1; i < points.Count; ++i)
                {
                    aabb.mMax = Vector2.Max(aabb.mMax, points[i]);
                    aabb.mMin = Vector2.Max(aabb.mMin, points[i]);
                }
            }
            return aabb;
        }
        public static void MeshVertexPrimitiveType(List<Vector2> points, ref List<Vector2> vertes, ref List<int> indices)
        {
            vertes.Clear();
            indices.Clear();
            int count = points.Count / 3;
            for (int i = 0; i < count; ++i)
            {
                for (int j = i * 3; j < i * 3 + 3; ++j)
                {
                    Vector2 vj = points[j];
                    int idx = vertes.FindIndex((v) => { return v == vj; });
                    if (idx == -1)
                    {
                        indices.Add(vertes.Count);
                        vertes.Add(vj);
                    }
                    else
                    {
                        indices.Add(idx);
                    }
                }
            }
        }

        public static void MeshTrianlgeType(List<Vector2> vertes, List<int> indices, ref List<Vector2> points)
        {
            points.Clear();
            int count = indices.Count;
            for (int i = 0; i < count; ++i)
            {
                points.Add(vertes[indices[i]]);
            }
        }

        public static void MeshTrianlgeType(List<Vector3> vertes, List<int> indices, ref List<Vector3> points)
        {
            points.Clear();
            int count = indices.Count;
            for (int i = 0; i < count; ++i)
            {
                points.Add(vertes[indices[i]]);
            }
        }

        public static void MeshTrianlgeToList(List<Vector3> vertes, ref List<List<Vector3>> triangles)
        {
            int count = vertes.Count / 3;
            for (int i = 0; i < count; ++i)
            {
                List<Vector3> tri = new List<Vector3>();
                tri.Add(vertes[i * 3]);
                tri.Add(vertes[i * 3 + 1]);
                tri.Add(vertes[i * 3 + 2]);
                triangles.Add(tri);
            }
        }

        public static List<Vector2> CalculateCircle(float angle, float r, Vector2 center)
        {
            List<Vector2> cir = new List<Vector2>();
            Matrix2x2 mat2 = MatrixUtils.CreateMatrix2D(angle, center);
            mat2.Scale = Vector2.one * r;
            Vector2 start = new Vector2(0, 1);
            int count = (int)(360.0f / angle);
            for (int i = 0; i < count; ++i)
            {
                Vector2 next = mat2.Rotate(start);
                start = next.normalized;
                cir.Add(start * r + center);
            }
            return cir;
        }

        public static void MeshTrianlgeToList(List<Vector2> vertes, ref List<List<Vector2>> triangles)
        {
            int count = vertes.Count / 3; 
            for (int i = 0; i < count; ++i)
            {
                List<Vector2> tri = new List<Vector2>();
                tri.Add(vertes[i * 3]);
                tri.Add(vertes[i * 3 + 1]);
                tri.Add(vertes[i * 3 + 2]);
                triangles.Add(tri);
            }
        }
        public static void FlatList<T>(List<List<T>> vertes, ref List<T> points)
        {
            points.Clear();
            foreach (List<T> lst in vertes)
            {
                points.AddRange(lst);
            }
        }

        public static void FlatList(List<List<Vector2>> vertes, ref List<Vector2> points)
        {
            points.Clear();
            foreach (List<Vector2> lst in vertes)
            {
                points.AddRange(lst);
            }
        }

        public static void MergePoint(List<Vector2> convex)
        {
            int size = convex.Count;
            Vector2 pi;
            Vector2 ni;
            List<int> needRemove = new List<int>();
            for (int i = 0; i < size; ++i)
            {
                int n = (i + 1) % size;
                int p = (i - 1 + size) % size;
                pi = convex[i] - convex[p];
                ni = convex[i] - convex[n];
                pi.Normalize();
                ni.Normalize();
                float dot = Vector2.Dot(pi, ni);
                if (dot < -0.995f)
                {
                    needRemove.Add(i);
                }
            }
            for (int i = needRemove.Count - 1; i >= 0; --i)
            {
                convex.RemoveAt(needRemove[i]);
            }
        }

        /*
         *  t2 * f2 - t1 * f1ab + f3ab = 0
         *  t2 * f1cd - t1 * f2 + f3cd = 0
         *  
         *  t = [
         *        t1 
         *        t2 
         *      ]
         *  A = [
         *        -f1ab   f2
         *        -f2   f1cd 
         *      ]
         *  b = [
         *          -f3ab
         *          -f3cd
         *      ]
         *  
         *  A * t = b
         * 
         *  A-1 = 1 / |A| * [
         *                      f1cd   -f2
         *                       f2   -f1ab
         *                  ]
         *  A-1 * b = [
         *              (f1cd * -f3ab) + (-f2 * -f3cd)
         *              (f2 * -f3ab) + (-f1ab * -f3cd)
         *            ]
         *              / |A|
         */
        public static bool LineLineClosestPoint(Vector3 line1, Vector3 line2, Vector3 line3, Vector3 line4, out Vector3 closest1, out Vector3 closest2)
        {
            // http://www.cnblogs.com/chuzhouGIS/archive/2011/12/12/2284774.html
            Vector3 ab = line2 - line1;
            Vector3 cd = line4 - line3;
            Vector3 ac = line3 - line1;
            float f1ab = Vector3.Dot(ab, ab);// ab.x * ab.x + ab.y * ab.y + ab.z * ab.z;
            float f1cd = Vector3.Dot(cd, cd); ;//cd.x * cd.x + cd.y * cd.y + cd.z * cd.z;
            float f2 = Vector3.Dot(ab, cd);//ab.x * cd.x + ab.y * cd.y + ab.z * cd.z;
            float f3ab = Vector3.Dot(ab, ac); // ab.x * ac.x + ab.y * ac.y + ab.z * ac.z;
            float f3cd = Vector3.Dot(cd, ac);// cd.x * ac.x + cd.y * ac.y + cd.z * ac.z;

            float det = (-f1ab * f1cd) + f2 * f2;
            if (det > -1e-5f && det < 1e-5f) // 平行 共线
            {
                closest1 = new Vector3();
                closest2 = new Vector3();
                return false;
            }
            det = 1.0f / det;
            float t1 = f2 * f3cd - f1cd * f3ab;
            t1 *= det;
            float t2 = f1ab * f3cd - f2 * f3ab;
            t2 *= det;
            closest1 = t1 * ab + line1;
            closest2 = t2 * cd + line3;
            return true;
        }
        public static bool LineSegmentClosestPoint(Vector3 line1, Vector3 line2, Vector3 seg1, Vector3 seg2, out Vector3 closest1, out Vector3 closest2)
        {
            Vector3 ab = line2 - line1;
            Vector3 cd = seg2 - seg1;
            Vector3 ac = seg1 - line1;
            float f1ab = Vector3.Dot(ab, ab);// ab.x * ab.x + ab.y * ab.y + ab.z * ab.z;
            float f1cd = Vector3.Dot(cd, cd); ;//cd.x * cd.x + cd.y * cd.y + cd.z * cd.z;
            float f2 = Vector3.Dot(ab, cd);//ab.x * cd.x + ab.y * cd.y + ab.z * cd.z;
            float f3ab = Vector3.Dot(ab, ac); // ab.x * ac.x + ab.y * ac.y + ab.z * ac.z;
            float f3cd = Vector3.Dot(cd, ac);// cd.x * ac.x + cd.y * ac.y + cd.z * ac.z;

            float det = (-f1ab * f1cd) + f2 * f2;
            if (det > -1e-5f && det < 1e-5f) // 平行 共线
            {
                closest1 = new Vector3();
                closest2 = new Vector3();
                return false;
            }
            det = 1.0f / det;
            float t1 = f2 * f3cd - f1cd * f3ab;
            t1 *= det;
            float t2 = f1ab * f3cd - f2 * f3ab;
            t2 *= det;
            closest1 = t1 * ab + line1;
            if (t2 > 1.0f)
            {
                closest2 = seg2;
            }
            else if (t2 < 0.0f)
            {
                closest2 = seg1;
            }
            else
            {
                closest2 = t2 * cd + seg1;
            }
            return true;
        }

        public static bool SegmentSegmentClosestPoint(Vector3 seg1, Vector3 seg2, Vector3 seg3, Vector3 seg4, out Vector3 closest1, out Vector3 closest2)
        {
            Vector3 ab = seg2 - seg1;
            Vector3 cd = seg4 - seg3;
            Vector3 ac = seg3 - seg1;
            float f1ab = Vector3.Dot(ab, ab);// ab.x * ab.x + ab.y * ab.y + ab.z * ab.z;
            float f1cd = Vector3.Dot(cd, cd); ;//cd.x * cd.x + cd.y * cd.y + cd.z * cd.z;
            float f2 = Vector3.Dot(ab, cd);//ab.x * cd.x + ab.y * cd.y + ab.z * cd.z;
            float f3ab = Vector3.Dot(ab, ac); // ab.x * ac.x + ab.y * ac.y + ab.z * ac.z;
            float f3cd = Vector3.Dot(cd, ac);// cd.x * ac.x + cd.y * ac.y + cd.z * ac.z;

            float det = (-f1ab * f1cd) + f2 * f2;
            if (det > -1e-5f && det < 1e-5f) // 平行 共线
            {
                closest1 = new Vector3();
                closest2 = new Vector3();
                return false;
            }
            det = 1.0f / det;
            float t1 = f2 * f3cd - f1cd * f3ab;
            t1 *= det;
            float t2 = f1ab * f3cd - f2 * f3ab;
            t2 *= det;

            if (t1 > 1.0f)
            {
                t1 = 1.0f;
            }
            else if (t1 < 0.0f)
            {
                t1 = 0.0f;
            }
            closest1 = t1 * ab + seg1;
            if (t2 > 1.0f)
            {
                t2 = 1.0f;
            }
            else if (t2 < 0.0f)
            {
                t2 = 0.0f;
            }
            closest2 = t2 * cd + seg3;
            return true;
        }

        public static bool RayLineClosestPoint(Vector3 rayOrigin, Vector3 rayDirection, Vector3 line1, Vector3 line2, out Vector3 closest1, out Vector3 closest2)
        {
            Vector3 ab = 10 * rayDirection;
            Vector3 cd = line2 - line1;
            Vector3 ac = line1 - rayOrigin;
            float f1ab = Vector3.Dot(ab, ab);// ab.x * ab.x + ab.y * ab.y + ab.z * ab.z;
            float f1cd = Vector3.Dot(cd, cd); ;//cd.x * cd.x + cd.y * cd.y + cd.z * cd.z;
            float f2 = Vector3.Dot(ab, cd);//ab.x * cd.x + ab.y * cd.y + ab.z * cd.z;
            float f3ab = Vector3.Dot(ab, ac); // ab.x * ac.x + ab.y * ac.y + ab.z * ac.z;
            float f3cd = Vector3.Dot(cd, ac);// cd.x * ac.x + cd.y * ac.y + cd.z * ac.z;

            float det = (-f1ab * f1cd) + f2 * f2;
            if (det > -1e-5f && det < 1e-5f) // 平行 共线
            {
                closest1 = new Vector3();
                closest2 = new Vector3();
                return false;
            }
            det = 1.0f / det;
            float t1 = f2 * f3cd - f1cd * f3ab;
            t1 *= det;
            float t2 = f1ab * f3cd - f2 * f3ab;
            t2 *= det;
            if (t1 < 0.0f)
            {
                t1 = 0.0f;
            }
            closest1 = t1 * ab + rayOrigin;
            closest2 = t2 * cd + line1;
            return true;
        }

        public static bool RaySegmentClosestPoint(Vector3 rayOrigin, Vector3 rayDirection, Vector3 seg1, Vector3 seg2, out Vector3 closest1, out Vector3 closest2)
        {
            Vector3 ab = 10 * rayDirection;
            Vector3 cd = seg2 - seg1;
            Vector3 ac = seg1 - rayOrigin;
            float f1ab = Vector3.Dot(ab, ab);// ab.x * ab.x + ab.y * ab.y + ab.z * ab.z;
            float f1cd = Vector3.Dot(cd, cd); ;//cd.x * cd.x + cd.y * cd.y + cd.z * cd.z;
            float f2 = Vector3.Dot(ab, cd);//ab.x * cd.x + ab.y * cd.y + ab.z * cd.z;
            float f3ab = Vector3.Dot(ab, ac); // ab.x * ac.x + ab.y * ac.y + ab.z * ac.z;
            float f3cd = Vector3.Dot(cd, ac);// cd.x * ac.x + cd.y * ac.y + cd.z * ac.z;

            float det = (-f1ab * f1cd) + f2 * f2;
            if (det > -1e-5f && det < 1e-5f) // 平行 共线
            {
                closest1 = new Vector3();
                closest2 = new Vector3();
                return false;
            }
            det = 1.0f / det;
            float t1 = f2 * f3cd - f1cd * f3ab;
            t1 *= det;
            float t2 = f1ab * f3cd - f2 * f3ab;
            t2 *= det;

            if (t1 < 0.0f)
            {
                t1 = 0.0f;
            }
            closest1 = t1 * ab + rayOrigin;
            if (t2 > 1.0f)
            {
                t2 = 1.0f;
            }
            else if (t2 < 0.0f)
            {
                t2 = 0.0f;
            }
            closest2 = t2 * cd + seg1;
            return true;
        }

        public static bool RayRayClosestPoint(Vector3 rayOrigin1, Vector3 rayDirection1, Vector3 rayOrigin2, Vector3 rayDirection2, out Vector3 closest1, out Vector3 closest2)
        {
            Vector3 ab = 10 * rayDirection1;
            Vector3 cd = 10 * rayDirection2;
            Vector3 ac = rayOrigin2 - rayOrigin1;
            float f1ab = Vector3.Dot(ab, ab);// ab.x * ab.x + ab.y * ab.y + ab.z * ab.z;
            float f1cd = Vector3.Dot(cd, cd); ;//cd.x * cd.x + cd.y * cd.y + cd.z * cd.z;
            float f2 = Vector3.Dot(ab, cd);//ab.x * cd.x + ab.y * cd.y + ab.z * cd.z;
            float f3ab = Vector3.Dot(ab, ac); // ab.x * ac.x + ab.y * ac.y + ab.z * ac.z;
            float f3cd = Vector3.Dot(cd, ac);// cd.x * ac.x + cd.y * ac.y + cd.z * ac.z;

            float det = (-f1ab * f1cd) + f2 * f2;
            if (det > -1e-5f && det < 1e-5f) // 平行 共线
            {
                closest1 = new Vector3();
                closest2 = new Vector3();
                return false;
            }
            det = 1.0f / det;
            float t1 = f2 * f3cd - f1cd * f3ab;
            t1 *= det;
            float t2 = f1ab * f3cd - f2 * f3ab;
            t2 *= det;
            if (t1 < 0.0f)
            {
                t1 = 0.0f;
            }
            closest1 = t1 * ab + rayOrigin1;
            if (t2 < 0.0f)
            {
                t2 = 0.0f;
            }
            closest2 = t2 * cd + rayOrigin2;
            return true;
        }

    }
}
