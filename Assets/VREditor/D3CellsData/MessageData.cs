using System.Collections;
using System;

namespace U3DSceneEditor
{
    /// <summary>
    /// 消息体 
    /// </summary>
    [Serializable]
    public class MessageDataHead
    {
        //  时间戳 
        public float timestamp = 0;

        //  参数1
        public string param;
    }

    [Serializable]
    public class MessageData
    {
        //  时间戳 
        public MessageDataHead head;

        //  内容
        public string content;
    }
}
