using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace U3DSceneEditor
{

    [ExecuteInEditMode]
    public class D3Camera : MonoBehaviour
    {
        private bool IsOrth;

        virtual public void Look(Vector3 pos)
        {
            float Distance = Vector3.Distance(pos, transform.position);

            transform.position = pos + (-transform.forward * Distance);

            Global.instance.mUniqueCamera.transform.LookAt(pos);

            return;
        }

        public MouseOrbitInfiteRotateZoom mOrbitInfite;

        virtual public void View(float delta)
        {
            //mOrbitInfite.camera.fieldOfView += -delta * D3Config.Speed_Camera_FOV;
            if (IsOrth)
            {
                float size = GetComponent<Camera>().orthographicSize + delta * D3Config.Speed_Camera_FOV;
                if (size > 0)
                {
                    GetComponent<Camera>().orthographicSize = size;
                }
            }
            else
            {
                Vector3 v = transform.position;
                v -= delta * D3Config.Speed_Camera_FOV * transform.forward;
                transform.position = v;
            }
        }

        virtual public void Moving(Vector2 d)
        {
            //dir.y = 0;

            //Vector3 pos = transform.position + dir * D3Config.Speed_Camera_Move;
            ////Vector3 pos = transform.position + transform.TransformDirection(dir) * D3Config.Speed_Camera_Move;

            //transform.position = pos;
            Vector3 v = transform.position;
            Vector3 dir = d.y * D3Config.Speed_Camera_FOV * transform.up + d.x * D3Config.Speed_Camera_FOV * transform.right;

            dir.y = 0;
            transform.position = v + dir;
        }

        virtual public void Rotating(Vector3 targetPos, Vector2 delta)
        {
            mOrbitInfite.Pinch(targetPos, delta * D3Config.Speed_Camera_Rotate);
        }

        //Camera mainCamera;

        ////地图宽度
        //public float mapWidth;
        ////地图高度
        //public float mapHeight;
        ////X坐标最小值  
        //float Xmin;
        ////X坐标最大值  
        //float Xmax;
        ////Y坐标最小值  
        //float Ymin;
        ////Y坐标最大值  
        //float Ymax;

        //void Start()
        //{
        //    mainCamera = Camera.main;
        //}

        //void Update()
        //{
        //    //摄像机张开的角度  
        //    float angle1 = mainCamera.fieldOfView;
        //    //摄像机与法线的角度  
        //    float angle2 = 90 - mainCamera.transform.eulerAngles.x;
        //    //摄像机高度  
        //    float height = mainCamera.transform.position.y;

        //    float wh = (float)Screen.width / Screen.height;

        //    Ymin = 0 - height * Mathf.Tan((angle2 - angle1 / 2) * Mathf.PI / 180);
        //    Ymax = mapHeight - height * Mathf.Tan((angle2 + angle1 / 2) * Mathf.PI / 180);

        //    float tempX = height / Mathf.Cos((angle2 + angle1 / 2) * Mathf.PI / 180);
        //    Xmin = 0 + tempX * Mathf.Sin((angle1 / 2) * Mathf.PI / 180) * wh;
        //    Xmax = mapWidth - tempX * Mathf.Sin((angle1 / 2) * Mathf.PI / 180) * wh;

        //    Debug.Log("          .........    Xmin="+Xmin+"    Xmax="+Xmax+"    Ymin="+Ymin+"    Ymax="+Ymax);

        //}

        void OnGUI()
        {
            if (GUI.Button(new Rect(0, Screen.height - 30, 50, 30), "俯视"))
            {
                mOrbitInfite.GetComponent<Camera>().transform.eulerAngles = new Vector3(90, 0, 0);
                Vector3 pos = mOrbitInfite.GetComponent<Camera>().transform.position;
                pos.x = Global.instance.Scene.width / 2;
                pos.y = 70;
                pos.z = Global.instance.Scene.height / 2;
                mOrbitInfite.GetComponent<Camera>().transform.position = pos;
            }

            if (GUI.Button(new Rect(80, Screen.height - 30, 50, 30), "平视"))
            {
                mOrbitInfite.GetComponent<Camera>().transform.eulerAngles = new Vector3(0, 0, 0);
                Vector3 pos = mOrbitInfite.GetComponent<Camera>().transform.position;
                pos.x = Global.instance.Scene.width / 2;
                pos.y = 0;
                pos.z = -70;
                mOrbitInfite.GetComponent<Camera>().transform.position = pos;

            }

            if (GUI.Button(new Rect(0, Screen.height - 70, 50, 30), "Pers"))
            {
                IsOrth = false;
                GetComponent<Camera>().orthographic = false;
            }
            if (GUI.Button(new Rect(80, Screen.height - 70, 50, 30), "Orth"))
            {
                IsOrth = true;
                GetComponent<Camera>().orthographic = true;
            }

        }
    }

}
