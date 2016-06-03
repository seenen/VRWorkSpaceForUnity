using UnityEngine;
using System.Collections;

namespace U3DSceneEditor
{
    [ExecuteInEditMode]
    [AddComponentMenu("Object/D3/Function")]
    public class D3RobotArm : D3Object, IFingerControl
    {

        [SerializeField]
        ///
        ///  绑定的数据
        ///
        D3RobotArmData mMDData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        override public void InitData(D3DataBase data)
        {
            base.InitData(data);

            mMDData = (D3RobotArmData)data;
        }

    }
}
