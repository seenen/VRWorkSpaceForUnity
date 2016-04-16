using System.Collections;

using System.IO;
using System;

namespace U3DSceneEditor
{
    /// <summary>
    /// 地图信息
    /// </summary>
    [Serializable]
    public class D3MapCellData : D3DataBase
    {
        //  场景名 
        public int ID = -1;

        //  场景长 
        public int width = -1;

        //  场景宽 
        public int height = -1;

        //  单元格 
        public int grid_w = -1;

        public int grid_h = -1;

        public int[,] matrix;

        public string filename;

        public string ProjectName;
    }
}