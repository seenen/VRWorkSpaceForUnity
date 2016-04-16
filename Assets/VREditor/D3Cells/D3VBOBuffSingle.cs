using UnityEngine;
using System.Collections;
using LibVRGeometry;
using System.Collections.Generic;

namespace U3DSceneEditor
{
    public class D3VBOBuffSingle : D3Object, IFingerControl
    {
        [SerializeField]
        D3VBOBuffSingleData mD3VBOBuffSingleData;

        override public void InitData(D3DataBase data)
        {
            base.InitData(data);

            bDrag = true;
            bSelection = true;
            bRotation = true;

            //
            mD3VBOBuffSingleData = (D3VBOBuffSingleData)mData;
        }

        override public void UpdateData()
        {
            base.UpdateData();

            if (m_sel)
            {
            }
        }

        #region Render
        public void UpdateVBO(VBOBufferSingle omr)
        {
            switch (omr.state)
            {
                case MessageState.Null:
                    break;
                case MessageState.Create:
                    mD3VBOBuffSingleData.CreateVBOBuffer(omr);
                    base.InitGO();
                    break;
                case MessageState.Update:
                    mD3VBOBuffSingleData.ModifyVBOBuffer(omr);
                    break;
                case MessageState.Destory:
                    mD3VBOBuffSingleData.DestoryVBOBuffer(omr);
                    break;
            }

        }
        #endregion
    }
}