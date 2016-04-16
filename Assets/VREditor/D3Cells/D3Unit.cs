using System.Collections;
using UnityEngine;

namespace U3DSceneEditor
{


    [ExecuteInEditMode]
    [AddComponentMenu("Object/D3/Function")]
    public class D3Unit : D3Object, IFingerControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        override public void InitData(D3DataBase data)
        {
            base.InitData(data);

            bDrag = true;
            bSelection = true;
            bRotation = true;

            //
            mUnitData = (D3UnitData)mData;

            StartCoroutine(UpdateUnitInfo());
        }

        IEnumerator UpdateUnitInfo()
        {
            yield return 1;

            //if (mUnitData.unitinfo == null ||
            //     string.IsNullOrEmpty(mUnitData.unitinfo.FileName))
            //{
            //    GameObject res = (GameObject)Resources.Load("FlagRespawn_UnitData");

            //    SetAttachObj(res, Vector3.one, true, mUnitData.color, false, string.Empty);

            //    Console.GetInstance().LogError("mUnitData.unitinfo Wrong");

            //    SetBoxColliderSize(Vector3.one * 2);
            //}
            //else
            //{
            //    yield return 1;

            //    Console.GetInstance().Log("LoadRes " + mUnitData.unitinfo.FileName);

            //    GameObject res = BundleMgr.Instance.GetSceneRes(D3Config.PREFIX_FOLDER + mUnitData.unitinfo.FileName);

            //    //  利用模型的size来设置碰撞框 
            //    SetAttachObj(res, Vector3.one, false, Color.white, false, string.Empty);

            //    SetBoxColliderSize(Vector3.one * 2);

            //    yield return 1;

            //    transform.rotation = mUnitData.Dir;

            //}
        }

        override public void UpdateData()
        {
            base.UpdateData();

            if (m_sel)
            {
                //if (mUnitData.unitinfo != null)
                //{
                //    GLGizmos.DrawSphere(transform.position + Vector3.up * 0.01f, mUnitData.unitinfo.BodySize, Color.red);

                //    GLGizmos.DrawSphere(transform.position + Vector3.up * 0.01f, mUnitData.unitinfo.GuardRange, Color.yellow);
                //}
            }

        }

        //
        [SerializeField]
        public D3UnitData mUnitData;
    }

}
