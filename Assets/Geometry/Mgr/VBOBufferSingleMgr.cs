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
            if (allHash.ContainsKey(vbo.id.ToString()))
            {
                D3Object d3 = (D3Object)allHash[vbo.id.ToString()];
                ((D3VBOBuffSingle)d3).UpdateVBO(vbo);

                allHash.Remove(vbo.id.ToString());
            }

            GameObject go = GetBase();
            go.name = vbo.id.ToString();
            go.AddComponent<HOGallBladder>();

            //go.tag = D3Config.REGION_NAME;

            D3VBOBuffSingleData vbodata = new D3VBOBuffSingleData(go);
            vbodata.Name = vbo.id.ToString();

            D3VBOBuffSingle d = go.AddComponent<D3VBOBuffSingle>();
            d.InitData(vbodata);
            d.UpdateVBO(vbo);

            allHash[vbo.id.ToString()] = d;
        }

        void ModifyVBOBuffer(VBOBufferSingle vbo)
        {
            Debuger.Log("VBOBufferSingleMgr.ModifyVBOBuffer" + vbo.id);

            if (allHash.ContainsKey(vbo.id.ToString()))
            {
                D3Object d = (D3Object)allHash[vbo.id.ToString()];
                ((D3VBOBuffSingle)d).UpdateVBO(vbo);
            }
            else
                Debuger.LogError("No Exist " + vbo.id);

        }

        void DestoryVBOBuffer(VBOBufferSingle vbo)
        {
            Debuger.Log("VBOBufferSingleMgr.DeleteVBOBufferSingle" + vbo.id);

            if (allHash.ContainsKey(vbo.id.ToString()))
            {
                D3Object d = (D3Object)allHash[vbo.id.ToString()];
                ((D3VBOBuffSingle)d).UpdateVBO(vbo);

                allHash.Remove(vbo.id.ToString());
            }
            else
                Debuger.LogError("No Exist " + vbo.id.ToString());
        }
        #endregion

        #region 处理本地消息
        public Dictionary<string, D3Object> allHash = new Dictionary<string, D3Object>();

        public GameObject Selection(GameObject[] objs)
        {
            string selname = string.Empty;

            foreach (GameObject e in objs)
            {
                if (e.layer != D3Config.LAYER_TRIGGER)
                    continue;

                selname = e.name;


                break;
            }
            GameObject obj = SelectionObject(selname);

            Debug.Log(obj);

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
                Debuger.LogError("No Exist " + selname);

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
