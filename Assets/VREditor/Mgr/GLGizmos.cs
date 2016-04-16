using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace U3DSceneEditor
{

    public class GLGizmos : MonoBehaviour
    {
        #region instance
        private static GLGizmos s_Instance = null;
        public static GLGizmos GetInstance()
        {
            if (s_Instance == null)
            {
                Camera camera = Camera.main;
                camera.gameObject.AddComponent<GLGizmos>();
                s_Instance = camera.gameObject.GetComponent<GLGizmos>();
            }

            return s_Instance;
        }
        void Awake()
        {
            s_Instance = this;
        }
        #endregion

        public static void DrawLine(UnityEngine.Vector3 startPos, UnityEngine.Vector3 endPos, UnityEngine.Color color)
        {
            LineStruct line = new LineStruct();
            line.startPos = startPos;
            line.endPos = endPos;
            line.color = color;

            GLGizmos.GetInstance().lines.Add(line);
        }

        public static void DrawDirectLine(UnityEngine.Vector3 begin, UnityEngine.Vector3 end, UnityEngine.Color lineColor, UnityEngine.Color arrowColor)
        {
            for (int i = 0; i < 15; ++i)
            {
                UnityEngine.Vector3 L1 = end + Quaternion.AngleAxis(-i, UnityEngine.Vector3.up) * ((begin - end).normalized * 0.5f);
                DrawLine(L1, end, arrowColor);

                UnityEngine.Vector3 L2 = end + Quaternion.AngleAxis(i, UnityEngine.Vector3.up) * ((begin - end).normalized * 0.5f);
                DrawLine(L2, end, arrowColor);
            }

            DrawLine(begin, end + (begin - end).normalized * 0.5f, lineColor);
        }

        public static void DrawSphere(UnityEngine.Vector3 pos, float radios, UnityEngine.Color color)
        {
            SphereStruct sphere = new SphereStruct();
            sphere.pos = pos;
            sphere.radios = radios;
            sphere.color = color;

            GLGizmos.GetInstance().spheres.Add(sphere);
        }

        public static void DrawArc(UnityEngine.Transform trans, float radios, float degree, UnityEngine.Color color)
        {
            ArcStruct arc = new ArcStruct();
            arc.trans = trans;
            arc.radios = radios;
            arc.degree = degree;
            arc.color = color;

            GLGizmos.GetInstance().arcs.Add(arc);
        }

        public static void DrawCube(UnityEngine.Transform trans, UnityEngine.Vector3 size, UnityEngine.Color color)
        {
            CubeStruct cube = new CubeStruct();
            cube.trans = trans;
            cube.size = size;
            cube.color = color;

            GLGizmos.GetInstance().cubes.Add(cube);
        }

        class LineStruct
        {
            public UnityEngine.Vector3 startPos;
            public UnityEngine.Vector3 endPos;
            public UnityEngine.Color color;
        }

        class SphereStruct
        {
            public UnityEngine.Vector3 pos;
            public float radios;
            public UnityEngine.Color color;
        }

        class ArcStruct
        {
            public UnityEngine.Transform trans;
            public float radios;
            public float degree;
            public UnityEngine.Color color;
        }

        class CubeStruct
        {
            public UnityEngine.Transform trans;
            public UnityEngine.Vector3 size;
            public UnityEngine.Color color;
        }

        List<LineStruct> lines = new List<LineStruct>();
        List<SphereStruct> spheres = new List<SphereStruct>();
        List<ArcStruct> arcs = new List<ArcStruct>();
        List<CubeStruct> cubes = new List<CubeStruct>();

        void OnPostRender()
        {
            foreach (LineStruct line in lines)
            {
                GLGizmosDraw.DrawLine(line.startPos, line.endPos, line.color);
            }

            foreach (SphereStruct sphere in spheres)
            {
                GLGizmosDraw.DrawSphere(sphere.pos, sphere.radios, sphere.color);
            }

            foreach (ArcStruct arc in arcs)
            {
                GLGizmosDraw.DrawArc(arc.trans, arc.radios, arc.degree, arc.color);
            }

            foreach (CubeStruct cube in cubes)
            {
                GLGizmosDraw.DrawCube(cube.trans, cube.size, cube.color);
            }

            lines.Clear();
            spheres.Clear();
            arcs.Clear();
            cubes.Clear();
        }
    }
}
