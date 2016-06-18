using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LibVRGeometry;

namespace U3DSceneEditor
{

    public class VBOBufferSingleMgr
    {
        private static VBOBufferSingleMgr instance;

        public static VBOBufferSingleMgr Instance
        {
            get
            {
                if (instance == null)
                    instance = new VBOBufferSingleMgr();

                return instance;
            }
        }

        #region 处理WinForm消息

        string VBO2Name(VBOBufferSingle vbo)
        {
            return typeof(VBOBufferSingle).ToString() + "_" + vbo.id.ToString();
        }

        string UM2Name(UnitMessage vbo)
        {
            return typeof(UnitMessage).ToString() + "_" + vbo.id.ToString();
        }

        #region UnitMessage
        public void Update(UnitMessage um)
        {
            Debuger.Log("UnitMessage.Update" + um.id);

            switch (um.state)
            {
                case UnitMessageState.Null:
                    Debuger.LogError("UnitMessageState.Null");
                    break;
                case UnitMessageState.Create:
                    UMCreate(um);
                    break;
                case UnitMessageState.Modify:
                    UMUpdate(um);
                    break;
                case UnitMessageState.Destory:
                    UMDelete(um);
                    break;
            }
        }

        void UMCreate(UnitMessage um)
        {
            Debuger.Log("UnitMessage.UMCreate" + MessageDecoder.EncodeMessage(um));

            string realName = UM2Name(um);

            //  本地处理
            if (allHash.ContainsKey(realName))
            {
                D3Object d3 = (D3Object)allHash[realName];
                //((D3VBOBuffSingle)d3).UpdateVBO(um);

                allHash.Remove(realName);
            }

            {
                GameObject go = GetBase();

                D3MedicalDeviceData mddata = new D3MedicalDeviceData();
                mddata.mHDMessage = (HDMessage)um;
                mddata.Name = realName;

                D3MedicalDevice d = go.AddComponent<D3MedicalDevice>();
                d.InitData(mddata);

                allHash[realName] = d;
            }

        }

        void UMUpdate(UnitMessage um)
        {
            Debuger.Log("UnitMessage.UMUpdate" + MessageDecoder.EncodeMessage(um));

            string realName = UM2Name(um);

            if (allHash.ContainsKey(realName))
            {
                D3Object d = (D3Object)allHash[realName];

                D3MedicalDevice md = (D3MedicalDevice)d;

                md.UpdateMdData((HDMessage)um);
            }
            else
                Debuger.LogError("UMUpdate No Exist " + um.id);
        }

        void UMDelete(UnitMessage um)
        {
            Debuger.Log("UnitMessage.UMDelete" + MessageDecoder.EncodeMessage(um));

            if (allHash.ContainsKey(UM2Name(um)))
            {
                D3Object d = (D3Object)allHash[UM2Name(um)];

                allHash.Remove(UM2Name(um));
            }
            else
                Debuger.LogError("UMDelete No Exist " + UM2Name(um));
        }
        #endregion

        #region VBOBufferSingle
        public void Update(VBOBufferSingle omr)
        {
            Debuger.Log("VBOBufferSingleMgr.Update" + omr.id);

            switch (omr.state)
            {
                case VBOState.Null:
                    break;
                case VBOState.Create:
                    CreateVBOBuffer(omr);
                    break;
                case VBOState.Update:
                    ModifyVBOBuffer(omr);
                    break;
                case VBOState.Destory:
                    DestoryVBOBuffer(omr);
                    break;
            }

        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="omr"></param>
        void CreateVBOBuffer(VBOBufferSingle vbo)
        {
            Debuger.Log("VBOBufferSingleMgr.CreateVBOBuffer" + vbo.id);

            //  本地处理
            if (allHash.ContainsKey(VBO2Name(vbo)))
            {
                D3Object d3 = (D3Object)allHash[VBO2Name(vbo)];
                ((D3VBOBuffSingle)d3).UpdateVBO(vbo);

                allHash.Remove(VBO2Name(vbo));
            }

            GameObject go = GetBase();
            go.name = VBO2Name(vbo);
            go.AddComponent<HOGallBladder>();

            D3VBOBuffSingleData vbodata = new D3VBOBuffSingleData(go);
            vbodata.Name = VBO2Name(vbo);

            D3VBOBuffSingle d = go.AddComponent<D3VBOBuffSingle>();
            d.InitData(vbodata);
            d.UpdateVBO(vbo);

            allHash[VBO2Name(vbo)] = d;
        }

        void ModifyVBOBuffer(VBOBufferSingle vbo)
        {
            Debuger.Log("VBOBufferSingleMgr.ModifyVBOBuffer" + vbo.id);

            if (allHash.ContainsKey(VBO2Name(vbo)))
            {
                D3Object d = (D3Object)allHash[VBO2Name(vbo)];
                ((D3VBOBuffSingle)d).UpdateVBO(vbo);
            }
            else
                Debuger.LogError("ModifyVBOBuffer No Exist " + vbo.id);

        }

        void DestoryVBOBuffer(VBOBufferSingle vbo)
        {
            Debuger.Log("VBOBufferSingleMgr.DeleteVBOBufferSingle" + vbo.id);

            if (allHash.ContainsKey(VBO2Name(vbo)))
            {
                D3Object d = (D3Object)allHash[VBO2Name(vbo)];
                ((D3VBOBuffSingle)d).UpdateVBO(vbo);

                allHash.Remove(VBO2Name(vbo));
            }
            else
                Debuger.LogError("DestoryVBOBuffer No Exist " + VBO2Name(vbo));
        }
        #endregion

        #endregion

        #region 处理本地消息
        public Dictionary<string, D3Object> allHash = new Dictionary<string, D3Object>();

        public GameObject Selection(GameObject[] objs)
        {
            string selname = string.Empty;

            foreach (GameObject e in objs)
            {
                if (e.layer != D3Config.Layer_Vbo)
                    continue;

                selname = e.name;


                break;
            }
            GameObject obj = SelectionObject(selname);

            Debuger.Log(obj);

            return obj;

        }

        /// <summary>
        /// 选中 
        /// </summary>
        /// <param name="selname"></param>
        /// <returns></returns>
        public GameObject SelectionObject(string selname)
        {

            //  通知所有谁被选中 
            foreach (KeyValuePair<string, D3Object> e in allHash)
            {
                D3Object d = (D3Object)e.Value;

                d.Selection(selname);
            }

            if (!allHash.ContainsKey(selname))
            {
                Debuger.LogError("SelectionObject No Exist " + selname);

                return null;
            }

            //  返回被选中的对象 
            D3Object selobj = (D3Object)allHash[selname];

            return selobj.gameObject;

        }
        #endregion

        GameObject GetBase()
        {
            GameObject ZoneEditor = GameObject.Find("ZoneEditor");

            GameObject go = new GameObject();
            //go.transform.parent = ZoneEditor.transform;

            return go;
        }

    }
}
