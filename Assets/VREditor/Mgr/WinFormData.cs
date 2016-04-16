using UnityEngine;
using System.Xml;
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;
using LibVRGeometry;

namespace U3DSceneEditor
{
    public class WinFormData : MonoBehaviour
    {
        static public WinFormData instance;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            instance = this;

        }

        //public static string EncodeMessage(object msg)
        //{
        //    XmlSerializer xs = new XmlSerializer(typeof(object));
			
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    MemoryStream stream = new MemoryStream();
        //    XmlWriterSettings setting = new XmlWriterSettings();
			
        //    settings.Indent = true;
        //    settings.Encoding = new UTF8Encoding(false);

        //    StringBuilder sb = new StringBuilder();
        //    using (XmlWriter xml = XmlWriter.Create(stream, settings))
        //    {
        //        XmlDocument doc = XmlUtil.ObjectToXml(msg);
        //        doc.Save(xml);
        //        xml.Flush();
        //    }
        //    string str = Encoding.UTF8.GetString(stream.ToArray());

        //    return str;
			
        //    return sb.ToString();
        //}

#region WinformToU3d
        public string DecodeBase64Message(string text)
        {
            byte[] buffer = Convert.FromBase64String(text);
            string xml = Encoding.UTF8.GetString(buffer);
            return xml;
        }

        public void ParseXml(string o)
        {
            //  
            //object data = EditorMessageDecoder.DecodeMessage(o);

            //if (WinFormData.instance == null)
            //{
            //    Console.GetInstance().LogError("ParseXml " + DecodeBase64Message(o));

            //    return;
            //}

            //if (data is MsgSetScene)
            //{
            //    ParseScene(data as MsgSetScene);
            //}
            //else if (data is MsgPutRegion)
            //{
            //    ParseRegion(data as MsgPutRegion);
            //}
            //else if (data is MsgPutUnit)
            //{
            //    ParseUnit(data as MsgPutUnit);
            //}
            //else if (data is MsgPutPoint)
            //{
            //    ParsePoint(data as MsgPutPoint);
            //}
            //else if (data is MsgRemoveObject)
            //{
            //    ParseRemoveObject(data as MsgRemoveObject);
            //}
            //else if (data is MsgRenameObject)
            //{
            //    ParseRenameObject(data as MsgRenameObject);
            //}
            //else if (data is MsgSelectObject)
            //{
            //    ParseSelectObject(data as MsgSelectObject);
            //}
            //else if (data is MsgLocateCamera)
            //{
            //    ParseLocateCamera(data as MsgLocateCamera);
            //}
            //else if (data is MsgShowTerrain)
            //{
            //    ParseShowTerrain(data as MsgShowTerrain);
            //}
            //else if (data is MsgSetTerrainBrush)
            //{
            //    ParseSetTerrainBrush(data as MsgSetTerrainBrush);
            //}
            //else if (data is MsgSetEditorMode)
            //{
            //    ParseSetEditorMode(data as MsgSetEditorMode);
            //}
            //else if (data is MsgPutDecoration)
            //{
            //    ParsePutDecoration(data as MsgPutDecoration);
            //}
            //else
            //{
            //    Console.GetInstance().LogError("UFO.................. ParseXml " + DecodeBase64Message(o));
            //}

        }

        /// <summary>
        /// 解析Scene 
        /// </summary>
        /// <param name="o"></param>

        //void ParseScene(MsgSetScene scene)
        //{
        //    if (scene == null)
        //    {
        //        Console.GetInstance().LogError("Wrong Scene Data");
        //        return;
        //    }

        //    D3MapCellData mScene = new D3MapCellData();
        //    mScene.ProjectName = scene.ProjectName;
        //    mScene.ID = scene.Data.ID;
        //    mScene.Name = "UniqueGroud";
        //    mScene.grid_w = scene.Data.GridCellW;
        //    mScene.grid_h = scene.Data.GridCellH;
        //    mScene.width = scene.Data.XCount;// TotalWidth;
        //    mScene.height = scene.Data.YCount;// TotalHeight;
        //    mScene.matrix = scene.Data.mTerrainMatrix;
        //    mScene.filename = scene.FileName.Replace("\\", "/");
        //    D3Config.PREFIX_FOLDER = scene.ResourceDir.Replace("\\", "/");

        //    //Console.GetInstance().Log(U3DJason.SerializeToString<D3MapCell>(mScene)); ;

        //    Console.GetInstance().Log("D3MapCell " + U3DJason.SerializeToString<D3MapCellData>(mScene));

        //    TriggerMgr.Instance.Scene(mScene);
        //}

        /// <summary>
        /// 解析Unit
        /// </summary>
        /// <param name="o"></param>

        //void ParseRegion(MsgPutRegion msgRegion)
        //{
        //    if (Global.instance.Scene == null)
        //    {
        //        Console.GetInstance().LogError("No Scene Data");
        //        return;
        //    }

        //    if (msgRegion == null)
        //    {
        //        Console.GetInstance().LogError("ParseRegion DecodeMessage");
        //        return;
        //    }

        //    //
        //    D3RegionData region = new D3RegionData();
        //    region.bEnable = false;
        //    region.Name = msgRegion.Data.Name;
        //    region.Pos = GetU3DPosByUnitCell(msgRegion.Data.X, msgRegion.Data.Y);
        //    region.Scale = new Vector3( msgRegion.Data.W, 0.2f, msgRegion.Data.H);
        //    region.zonetype = (int)msgRegion.Data.RegionType;
        //    region.color = GetColorFromInt(msgRegion.Data.Color);

        //    Console.GetInstance().Log("D3RegionData " + U3DJason.SerializeToString<D3RegionData>(region));

        //    TriggerMgr.Instance.Region(region);
        //}


        //void ParsePutDecoration(MsgPutDecoration msgDecoration)
        //{
        //    if (Global.instance.Scene == null)
        //    {
        //        Console.GetInstance().LogError("No Scene Data");
        //        return;
        //    }

        //    if (msgDecoration == null)
        //    {
        //        Console.GetInstance().LogError("ParsePutDecoration DecodeMessage");
        //        return;
        //    }

        //    D3DecorationData deco = new D3DecorationData();
        //    deco.bEnable = false;
        //    deco.Name = msgDecoration.Data.Name;
        //    deco.Pos = GetU3DPosByUnitCell(msgDecoration.Data.X, msgDecoration.Data.Y);
        //    deco.Scale = new Vector3(msgDecoration.Data.W, 0.2f, msgDecoration.Data.H);
        //    deco.zonetype = (int)msgDecoration.Data.RegionType;
        //    deco.color = GetColorFromInt(msgDecoration.Data.Color);

        //    deco.DecoRes = msgDecoration.Data.ResourceID;
        //    deco.DecoAnimName = msgDecoration.Data.AnimName;
        //    deco.DecoScale = msgDecoration.Data.Scale;
        //    deco.DecoHeight = msgDecoration.Data.Height;
        //    deco.GridWidth = msgDecoration.Data.GridSizeW;
        //    deco.GridHeight = msgDecoration.Data.GridSizeH;

        //    Console.GetInstance().Log("D3DecorationData " + U3DJason.SerializeToString<D3RegionData>(deco));

        //    TriggerMgr.Instance.Decoration(deco);

        //}

        //void ParsePoint(MsgPutPoint msgPoint)
        //{
        //    if (Global.instance.Scene == null)
        //    {
        //        Console.GetInstance().LogError("No Scene Data");
        //        return;
        //    }

        //    if (msgPoint == null)
        //    {
        //        Console.GetInstance().LogError("ParsePoint DecodeMessage");
        //        return;
        //    }

        //    D3PathNodeData point = new D3PathNodeData();
        //    point.bEnable = false;
        //    point.Name = msgPoint.Data.Name;
        //    point.Pos = GetU3DPosByUnitCell(msgPoint.Data.X, msgPoint.Data.Y);
        //    if (msgPoint.Data.NextNames != null) point.nexts.AddRange(msgPoint.Data.NextNames);
        //    point.color = GetColorFromInt(msgPoint.Data.Color);

        //    Console.GetInstance().Log("D3PathNodeData " + U3DJason.SerializeToString<D3PathNodeData>(point));

        //    TriggerMgr.Instance.PathNode(point);
        //}

        //void ParseRemoveObject(MsgRemoveObject data)
        //{
        //    if (Global.instance.Scene == null)
        //    {
        //        Console.GetInstance().LogError("No Scene Data");
        //        return;
        //    }

        //    if (data == null)
        //    {
        //        Console.GetInstance().LogError("ParseRemoveObject DecodeMessage");
        //        return;
        //    }

        //    Console.GetInstance().Log("ParseRemoveObject " + data.Name);

        //    TriggerMgr.Instance.RemoveObject(data.Name);

        //}

        ///// <summary>
        ///// 重命名
        ///// </summary>
        ///// <param name="data"></param>
        //void ParseRenameObject(MsgRenameObject data)
        //{
        //    if (Global.instance.Scene == null)
        //    {
        //        Console.GetInstance().LogError("No Scene Data");
        //        return;
        //    }

        //    if (data == null)
        //    {
        //        Console.GetInstance().LogError("ParseRemoveObject DecodeMessage");
        //        return;
        //    }

        //    Console.GetInstance().Log("ParseRenameObject " + data.SrcName + " To " + data.DstName);

        //    TriggerMgr.Instance.RenameObject(data.SrcName, data.DstName);

        //}

        //void ParseSelectObject(MsgSelectObject data)
        //{
        //    if (Global.instance.Scene == null)
        //    {
        //        Console.GetInstance().LogError("No Scene Data");
        //        return;
        //    }

        //    if (data == null)
        //    {
        //        Console.GetInstance().LogError("ParseSelectObject DecodeMessage");
        //        return;
        //    }

        //    Console.GetInstance().Log("ParseSelectObject " + data.Name);

        //    GameObject camfouce = TriggerMgr.Instance.SelectionObject(data.Name);

        //    if (camfouce != null)
        //    {
        //        //Global.instance.mD3Camera.Look(camfouce.transform.position);
        //        //Console.GetInstance().Log("Camera Look " + camfouce.name);

        //    }

        //}

        //void ParseLocateCamera(MsgLocateCamera data)
        //{
        //    if (Global.instance.Scene == null)
        //    {
        //        Console.GetInstance().LogError("No Scene Data");
        //        return;
        //    }

        //    if (data == null)
        //    {
        //        Console.GetInstance().LogError("ParseLocateCamera DecodeMessage");
        //        return;
        //    }

        //    Console.GetInstance().Log("ParseSelectObject " + data.X + "  " + data.Y);

        //    Vector3 WorldPos = GetU3DPosByUnitCell(data.X, data.Y);

        //    Global.instance.mD3Camera.Look(WorldPos);
        //}

        //void ParseShowTerrain(MsgShowTerrain data)
        //{
        //    Console.GetInstance().Log("ParseShowTerrain " + data.Show);

        //    if (Global.instance.mGroudMgr != null)
        //        Global.instance.mGroudMgr.ShowTerrian(data.Show);
        //}

        //void ParseSetTerrainBrush(MsgSetTerrainBrush data)
        //{
        //    Console.GetInstance().Log("ParseShowTerrain " + data.ARGB + " " + data.Size);

        //    Global.instance.mBuildMgr.SetBrush(MsgSetTerrainBrush.ToRGBA_F(data.ARGB), data.Size);
        //}

        //void ParseSetEditorMode(MsgSetEditorMode data)
        //{
        //    EditorMode em = EditorMode.NULL;

        //    switch (data.Mode)
        //    {
        //        case MsgSetEditorMode.MODE_OBJECT:
        //            Global.instance.SetEditorMode(EditorMode.OBJECT);
        //            em = EditorMode.OBJECT;
        //            break;
        //        case MsgSetEditorMode.MODE_TERRAIN:
        //            Global.instance.SetEditorMode(EditorMode.BUILD);
        //            em = EditorMode.BUILD;
        //            break;
        //    }

        //    //Console.GetInstance().LogError("No Develop " + data.Mode);
        //    Console.GetInstance().Log("ParseSetEditorMode " + data.Mode + " " + em);

        //}
        #endregion

        #region U3dToWinfom
        public void UpdateState(int LoadState)
        {
            //RspEditorState state = new RspEditorState();
            //state.State = LoadState;

            //string output = EditorMessageDecoder.EncodeMessage(state);

            //Console.GetInstance().Log("U3D State: " + U3DJason.SerializeToString<RspEditorState>(state));

            //MessageFactory.Instance.CallMsg(output);

        }

        public void UpdateSelection(string name)
        {
            Debuger.Log("U3dToWinfom UpdateSelection " + name);

            //RspOnObjectSelected obj = new RspOnObjectSelected();
            //{
            //    obj.Name = name;
            //}

            //string output = EditorMessageDecoder.EncodeMessage(obj);

            //MessageFactory.Instance.CallMsg(output);
        }

        /// <summary>
        /// 更新位置 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public void UpdatePosition(string name, Vector3 pos)
        {
            //RspObjectPositionChanged opc = new RspObjectPositionChanged();
            //opc.Name = name;
            //float[] p = GetUnitPosByU3DPos(pos);
            //opc.x = p[0];
            //opc.y = p[1];

            //Console.GetInstance().Log("U3D UpdatePosition: " + U3DJason.SerializeToString<RspObjectPositionChanged>(opc));

            //string output = EditorMessageDecoder.EncodeMessage(opc);

            //MessageFactory.Instance.CallMsg(output);
        }

        /// <summary>
        /// 更新缩放 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public void UpdateScale(string name, Vector3 scale)
        {
            //RspObjectSizeChanged osc = new RspObjectSizeChanged();
            //osc.Name = name;
            //osc.x = scale.x;
            //osc.y = scale.z;

            //string output = EditorMessageDecoder.EncodeMessage(osc);

            //MessageFactory.Instance.CallMsg(output);

        }

        /// <summary>
        /// 更新缩放 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public void UpdateDirection(string name, Quaternion rotation)
        {
            //RspObjectDirectionChanged odc = new RspObjectDirectionChanged();
            //odc.Name = name;
            //odc.dir = GetDirectionByQuaternion(rotation);

            //Console.GetInstance().Log("RspObjectDirectionChanged " + U3DJason.SerializeToString<RspObjectDirectionChanged>(odc));

            //string output = EditorMessageDecoder.EncodeMessage(odc);

            //MessageFactory.Instance.CallMsg(output);

        }

        /// <summary>
        /// 更新地块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="flag"></param>
        public void UpdateTile(Vector2 pos, Color flag)
        {
            //RspZoneFlagChanged rzf = new RspZoneFlagChanged();
            //rzf.SceneX = (int)pos.x;
            //rzf.SceneY = (int)pos.y;
            //rzf.Flag = MsgSetTerrainBrush.FromRGBA_F(new float[4] { flag.r, flag.g, flag.b, flag.a });

            //Console.GetInstance().Log("RspZoneFlagChanged " + U3DJason.SerializeToString<RspZoneFlagChanged>(rzf));

            //string output = EditorMessageDecoder.EncodeMessage(rzf);

            //MessageFactory.Instance.CallMsg(output);

        }

        //public void UpdateTileTotal(List<D3TileData> list)
        //{
        //    RspZoneFlagBathChanged rzfb = new RspZoneFlagBathChanged();

        //    {
        //        List<RspZoneFlagChanged> flags = new List<RspZoneFlagChanged>();

        //        foreach(D3TileData e in list)
        //        {
        //            RspZoneFlagChanged rzf = new RspZoneFlagChanged();
        //            rzf.SceneX = (int)e.Pos2.x;
        //            rzf.SceneY = (int)e.Pos2.y;
        //            rzf.Flag = MsgSetTerrainBrush.FromRGBA_F(new float[4] { e.color.r, e.color.g, e.color.b, e.color.a });

        //            flags.Add(rzf);
        //        }

        //        rzfb.Flags = flags;
        //    }

        //    Console.GetInstance().Log("RspZoneFlagBathChanged " + U3DJason.SerializeToString<RspZoneFlagBathChanged>(rzfb));

        //    string output = EditorMessageDecoder.EncodeMessage(rzfb);

        //    MessageFactory.Instance.CallMsg(output);

        //}

#endregion

#region Trans
        /// <summary>
        /// 将引擎的2d坐标转化成U3D引擎世界3d坐标
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public Vector3 GetU3DPosByUnitCell(float X, float Y)
        {
            return GetU3DPosByUnitCell(new float[2]{X, Y});
        }

        public Vector3 GetU3DPosByUnitCell(float[] cell)
        {
            Vector3 tmp = Vector3.zero;

            //tmp.x = cell[0];
            //tmp.y = 0;
            //tmp.z = Global.instance.Scene.height - cell[1];

            return tmp;
        }

        /// <summary>
        /// 将U3D引擎世界3d坐标转化成引擎的2d坐标
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public float[] GetUnitPosByU3DPos(Vector3 V3Pos)
        {
            float[] tmp = new float[2];

            //tmp[0] = V3Pos.x;
            //tmp[1] = Global.instance.Scene.height - V3Pos.z;

            return tmp;
        }

        /// <summary>
        /// 转方向 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Quaternion GetQuaternionByUnitCell(float dir)
        {
            return Quaternion.Euler(new Vector3(0, dir * Mathf.Rad2Deg + 90, 0));
        }

        public float GetDirectionByQuaternion(Quaternion quaternion)
        {
            float a = (quaternion.eulerAngles.y - 90) * Mathf.Deg2Rad;

            return a;
        }

        //public Color GetColorFromInt(int rgba)
        //{
        //    float[] fColor = MsgSetTerrainBrush.ToRGBA_F(rgba);

        //    Color color = new Color(fColor[0], fColor[1], fColor[2], fColor[3]);

        //    return color;
        //}

        //public int GetColorFromU3DColor(Color color)
        //{
        //    float[] fRgba = new float[4] { color.r, color.g, color.b, color.a };

        //    int iRgba = MsgSetTerrainBrush.FromRGBA_F(fRgba);

        //    return iRgba;
        //}
#endregion

#region Test
        public bool bTest = false;

        static int mOrder = 0;

        void OnGUI()
        {
            if (!bTest)
                return;

            if (GUI.Button(new Rect(100, 30, 100, 30), "Scene"))
                TestScene();

            if (GUI.Button(new Rect(100, 60, 100, 30), "Region"))
                TestRegion();

            if (GUI.Button(new Rect(100, 90, 100, 30), "Unit"))
                TestUnit();

            if (GUI.Button(new Rect(100, 120, 100, 30), "Point"))
                TestPoint();

            if (GUI.Button(new Rect(100, 150, 100, 30), "Deco"))
                TestDeco();

            if (GUI.Button(new Rect(400, 0, 100, 30), "ObjectMode"))
            {
                //MsgSetEditorMode e = new MsgSetEditorMode();
                //e.Mode = MsgSetEditorMode.MODE_OBJECT;
                //ParseSetEditorMode(e);
            }

            if (GUI.Button(new Rect(500, 0, 100, 30), "ObjectTerrian"))
            {
                //MsgSetEditorMode e = new MsgSetEditorMode();
                //e.Mode = MsgSetEditorMode.MODE_TERRAIN;
                //ParseSetEditorMode(e);
            }

            if (GUI.Button(new Rect(600, 0, 100, 15), "ShowTerrian"))
            {
                //MsgShowTerrain e = new MsgShowTerrain();
                //e.Show = ((m_iShow++) % 2) == 1 ? true : false;
                //ParseShowTerrain(e);
            }

            if (GUI.Button(new Rect(600, 15, 100, 15), "SetTerrainBrush"))
            {
                //MsgSetTerrainBrush e = new MsgSetTerrainBrush();
                //e.Size = UnityEngine.Random.Range(0, 5);
                //float r = UnityEngine.Random.Range(0, 1.0f);
                //float g = UnityEngine.Random.Range(0, 1.0f);
                //float b = UnityEngine.Random.Range(0, 1.0f);
                //float a = 1;
                //e.ARGB = MsgSetTerrainBrush.FromRGBA_F(new float[4] { r, g, b, a });
                //ParseSetTerrainBrush(e);
            }
        }
        int m_iShow = 0;

        void TestScene()
        {
            //MsgSetScene scene = new MsgSetScene();
            //{
            //    ZoneInfo zi = new ZoneInfo(70, 70, 1, 1);
            //    scene.Data = zi;
            //    scene.FileName = "/res/scene/Grammil_01.assetBundles";
            //    scene.ResourceDir = "E:/Zeus/GameEditors/GameEditor";
            //    D3Config.PREFIX_FOLDER = scene.ResourceDir.Replace("\\", "/");
            //}

            //ParseScene(scene);
        }

        void TestRegion()
        {

           // MsgPutRegion msgRegion = new MsgPutRegion();
           // {
           //     RegionData rd = new RegionData();
           //     rd.Name = "regionname" + mOrder++;
           //     rd.X = UnityEngine.Random.Range(0, 50);
           //     rd.Y = UnityEngine.Random.Range(0, 40);
           //     rd.RegionType = RegionData.Shape.RECTANGLE;
           //     rd.W = 3;
           //     rd.H = 2;

           //     msgRegion.Data = rd;
           // }

           //ParseRegion(msgRegion);

        }

        void TestDeco()
        {
            //MsgPutDecoration msgDeco = new MsgPutDecoration();
            //{
            //    DecorationData Data = new DecorationData();
            //    Data.Name = "Deconame" + mOrder++;
            //    Data.X = UnityEngine.Random.Range(0, 50);
            //    Data.Y = UnityEngine.Random.Range(0, 40);
            //    Data.RegionType = DecorationData.Shape.RECTANGLE;
            //    Data.W = 30;
            //    Data.H = 10;
            //    Data.Height = 10;
            //    Data.Color = GetColorFromU3DColor(Color.red);
            //    Data.ResourceID = "/res/unit/monster_juxiongguai_001.assetBundles";
            //    Data.AnimName = "f_idle";
            //    Data.Scale = 5;

            //    msgDeco.Data = Data;
            //}

            //ParsePutDecoration(msgDeco);
        }

        void TestUnit()
        {
            //MsgPutUnit msgUnit = new MsgPutUnit();
            //{
            //    UnitData ud = new UnitData();
            //    ud.Name = "unitname" + mOrder++;
            //    ud.X = UnityEngine.Random.Range(0, 50);
            //    ud.Y = UnityEngine.Random.Range(0, 40);
            //    ud.Direction = UnityEngine.Random.Range(0, 2 * Mathf.PI);
            //    ud.StartPointName = "path0";

            //    msgUnit.Data = ud;
            //}
            //{
            //    UnitInfo ui = new UnitInfo();
            //    ui.BodyHeight = 1;
            //    ui.BodySize = 1.3f;
            //    ui.FileName = "/res/unit/monster_baotu_001.assetBundles";
            //    ui.GuardRange = 10;
            //    ui.HealthPoint = 100;
            //    ui.MoveSpeedSEC = 6;
            //    ui.Name = "XWB";

            //    msgUnit.UnitData = ui;
            //}

            //ParseUnit(msgUnit);

        }

        void TestPoint()
        {
            //MsgPutPoint msg = new MsgPutPoint();
            //{
            //    PointData data = new PointData();
            //    data.Name = "point1";
            //    data.Color = -1;
            //    data.X = 20;
            //    data.Y = 20;

            //    msg.Data = data;
            //}

            //ParsePoint(msg);
        }
#endregion
    }

}
