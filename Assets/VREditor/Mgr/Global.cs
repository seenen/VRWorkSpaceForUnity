using UnityEngine;
using System.Collections;

namespace U3DSceneEditor
{
    public enum EditorMode
    {
        NULL,
        OBJECT,
        BUILD,
    }

    public class Global : MonoBehaviour 
    {

        static public Global instance;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            SetEditorMode(EditorMode.NULL);
        }

        //
        public Camera mUniqueCamera;

        //
        public Camera mBuildCamera;

        //
        public D3Camera mD3Camera;

        //  唯一地面 
        public GameObject mGroud;

        //
        //public BulidManager mBuildMgr;

        ////
        //public TriggerMgr mTriggerMgr;

        //
        D3MapCellData mScene = null;

        ////
        //public D3MapCell mGroudMgr = null;

        /// <summary>
        /// 场景
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public D3MapCellData Scene
        {
            set { mScene = (D3MapCellData)value; }
            get { return mScene; }
        }

        /// <summary>
        /// 解析字符串
        /// </summary>
        /// <param name="msgdata"></param>
        public void ParseJson(string msgdata)
        {
            Debuger.Log("[ParseJson:]  " + msgdata);

            //MessageData data = U3DJason.DeserializeFromJson<MessageData>(msgdata);

            //if (data.head.param == "region")
            //{
            //    D3RegionData region = U3DJason.DeserializeFromJson<D3RegionData>(data.content);

            //    ParseRegion(region);
            //}
        }

        public void ParseXml(string msgdata)
        {
            Debuger.Log("[ParseXml:]  " + msgdata);
        }

        EditorMode mEM = EditorMode.NULL;

        public void SetEditorMode(EditorMode em)
        {
            //switch(em)
            //{
            //    case EditorMode.BUILD:
            //        mTriggerMgr.gameObject.SetActive(false);
            //        mBuildMgr.gameObject.SetActive(true);
            //        break;
            //    case EditorMode.OBJECT:
            //        mBuildMgr.gameObject.SetActive(false);
            //        mTriggerMgr.gameObject.SetActive(true);
            //       break;
            //    default:
            //        mBuildMgr.gameObject.SetActive(false);
            //        mTriggerMgr.gameObject.SetActive(false);
            //       break;
            //}

            em = mEM;
        }
    }

}
