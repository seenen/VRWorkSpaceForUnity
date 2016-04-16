using System.Collections;
using System;

namespace U3DSceneEditor
{
    /// <summary>
    /// ��Ϣ�� 
    /// </summary>
    [Serializable]
    public class MessageDataHead
    {
        //  ʱ��� 
        public float timestamp = 0;

        //  ����1
        public string param;
    }

    [Serializable]
    public class MessageData
    {
        //  ʱ��� 
        public MessageDataHead head;

        //  ����
        public string content;
    }
}
