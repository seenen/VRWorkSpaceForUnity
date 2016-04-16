using UnityEngine;
using System.Collections;
using System;

namespace U3DSceneEditor
{
    public class D3DataBase
    {
        //  可用性 
        public bool bEnable = false;

        //  名字 
        public string Name = "NoName";

        //  位置 
        public Vector3 Pos = Vector3.zero;

        //  位置 
        public Vector2 Pos2 = Vector2.zero;

        //  缩放 
        public Vector3 Scale = Vector3.zero;

        //  颜色
        public Color color;

        //  方向 
        public Quaternion Dir = Quaternion.identity;

    }
}