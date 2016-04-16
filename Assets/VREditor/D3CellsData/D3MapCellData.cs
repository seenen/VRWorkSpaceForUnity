using System.Collections;

using System.IO;
using System;

namespace U3DSceneEditor
{
    /// <summary>
    /// ��ͼ��Ϣ
    /// </summary>
    [Serializable]
    public class D3MapCellData : D3DataBase
    {
        //  ������ 
        public int ID = -1;

        //  ������ 
        public int width = -1;

        //  ������ 
        public int height = -1;

        //  ��Ԫ�� 
        public int grid_w = -1;

        public int grid_h = -1;

        public int[,] matrix;

        public string filename;

        public string ProjectName;
    }
}