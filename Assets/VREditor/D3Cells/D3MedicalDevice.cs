using LibVRGeometry;
using UnityEngine;

namespace U3DSceneEditor
{
    [ExecuteInEditMode]
    [AddComponentMenu("Object/D3/Function")]
    public class D3MedicalDevice : D3Object, IFingerControl
    {
        
        [SerializeField]
        ///
        ///  绑定的数据
        ///
        D3MedicalDeviceData mMDData;

        /// <summary>
        /// 器具对象上的Mono脚本
        /// </summary>
        public MedicalDevices mMonoMedicalDevices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        override public void InitData(D3DataBase data)
        {
            base.InitData(data);

            mMDData = (D3MedicalDeviceData)data;

            switch(mMDData.mHDMessage.type)
            {
                case HDType.Scissors:
                    mMonoMedicalDevices = GameObject.Find("MDScissors").GetComponent<MDScissors>();
                    break;
                case HDType.TitaniumClamp:
                    mMonoMedicalDevices = GameObject.Find("MDTitaniumClamp").GetComponent<MDTitaniumClamp>();
                    break;
                case HDType.RobotArm:
                    mMonoMedicalDevices = GameObject.Find("MDRobotArm").GetComponent<MDRobotArm>();
                    break;
            }

            UpdateMdData(mMDData.mHDMessage);
        }

        #region Render
        public void UpdateMdData(HDMessage omr)
        {
            Debuger.Log("D3MedicalDevice.UpdateMdData " + omr.ToString());

            if (omr == null)
            {
                Debuger.LogError("D3MedicalDevice.UpdateMdData HDMessage == null");
                return;
            }

            if (mMonoMedicalDevices == null)
            {
                Debuger.LogError("D3MedicalDevice.UpdateMdData mMonoMedicalDevices == null");
                return;
            }

            mMDData.mHDMessage = omr;

            mMonoMedicalDevices.move_speed = omr.move_speed;
            mMonoMedicalDevices.rotate_speed = omr.rotate_speed;
            switch (omr.type)
            {
                case HDType.Null:
                    Debuger.LogError("D3MedicalDevice.UpdateMdData HDType == null");
                    break;
                case HDType.Scissors:
                    ((MDScissors)mMonoMedicalDevices).openangle = ((HDScissorsMessage)omr).merge_degree;
                    ((MDScissors)mMonoMedicalDevices).merge_speed = ((HDScissorsMessage)omr).merge_speed;
                    break;
                case HDType.TitaniumClamp:
                    ((MDTitaniumClamp)mMonoMedicalDevices).Cur_Ange = ((HDTitaniumClampMessage)omr).merge_degree;
                    ((MDTitaniumClamp)mMonoMedicalDevices).merge_speed = ((HDTitaniumClampMessage)omr).merge_speed;
                    break;
                case HDType.RobotArm:
                    ((MDRobotArm)mMonoMedicalDevices).UpdateData((HDRobotArmMessage)omr);
                    break;
            }
        }
        #endregion

    }
}
