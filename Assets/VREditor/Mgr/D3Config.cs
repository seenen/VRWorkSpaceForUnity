using UnityEngine;
using System.Collections;
//using UnityEditor;

namespace U3DSceneEditor
{

    public sealed class D3Config
    {
        //  _DATA_TEMPLATE      json������ģ�� 
        //  _DATA_OUTPUT_FOLDER json���ݵ�������ļ��� 
        //  _DATA_OUTPUT_SUFFIX json���ݵĺ�׺ 
        //  _SHOW               �༭����չʾ��ģ�� 

        //
        //  �ؿ�Ĳ� 
        static public int Layer_MedicalDevices = 8;

        //  �����Ĳ�
        static public int Layer_Nav = 10;

        //  Trigger
        public const int Layer_Vbo = 9;

        //
        static public float Speed_Camera_Move = 0.5f;
        static public float Speed_Camera_Rotate = 0.25f;
        static public float Speed_Camera_FOV = 0.25f;
        //  Tag
        //public const string REGION_NAME = "God_Region";
        //public const string UNIT_NAME = "God_UNIT";
        //public const string PATH_NAME = "God_Path";

        //  Unit
        //  ��ʾ��λģ�͵����� 
        //public const string UNIT_MODEL_NAME = "UnitModelName";

        //  Region
        //  ��ʾ��λģ�͵����� 
        public const string REGION_MODEL_NAME = "RegionModelName";

        ////  Monster
        //public const string MONSTER_ASSETS_RES_FOLDER = "Assets/Prefabs/Monsters/";
        //public const string MONSTER_ASSETS_RES_SUFFIX = ".prefab";

        ////  MainMenu
        //public const string MAINMENU_DATA_TEMPLATE = "/Template/MainMenu_Data_Template.txt";

        static public string PREFIX_FOLDER = "Error";

        static public string PROJECT_SG = "HerosZoneFactory";
    }
}
