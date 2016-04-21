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

            bDrag = false;
            bSelection = true;
            bRotation = false;

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

        float distance;

        override public void Draging(Vector2 newpos)
        {
            if (!bDrag) return;

            //  在对象所在平面的移动距离.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane plane = new Plane(transform.forward, transform.position);

            if (plane.Raycast(ray, out distance))
            {
                Vector3 intersectionPoint = ray.GetPoint(distance);
                transform.position = intersectionPoint;
            }
        }

        #region Render
        public void UpdateVBO(VBOBufferSingle omr)
        {
            switch (omr.state)
            {
                case VBOState.Null:
                    break;
                case VBOState.Create:
                    mD3VBOBuffSingleData.CreateVBOBuffer(omr);
                    base.InitGO();
                    break;
                case VBOState.Update:
                    mD3VBOBuffSingleData.ModifyVBOBuffer(omr);
                    break;
                case VBOState.Destory:
                    mD3VBOBuffSingleData.DestoryVBOBuffer(omr);
                    break;
            }

        }
        #endregion
    }
}