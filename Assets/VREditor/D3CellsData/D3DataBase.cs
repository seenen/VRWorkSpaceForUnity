using UnityEngine;
using System.Collections;
using System;

namespace U3DSceneEditor
{
    public class D3DataBase
    {
        //  ������ 
        public bool bEnable = false;

        //  ���� 
        public string Name = "NoName";

        //  λ�� 
        public Vector3 Pos = Vector3.zero;

        //  λ�� 
        public Vector2 Pos2 = Vector2.zero;

        //  ���� 
        public Vector3 Scale = Vector3.zero;

        //  ��ɫ
        public Color color;

        //  ���� 
        public Quaternion Dir = Quaternion.identity;

    }
}