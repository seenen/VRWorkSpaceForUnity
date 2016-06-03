using UnityEngine;
using System.Collections;


public class CallMessage : MonoBehaviour
{
    static public CallMessage Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (sends.Count > 0)
        {
            string str = (string)sends[0];
            sends.Remove(str);
            PeekSendMsg(str);
        }
    }

#region CallMessage
    /// <summary>
    /// 
    /// </summary>
    /// <param name="o"></param>
    void PeekSendMsg(string o)
    {
        if (string.IsNullOrEmpty(o))
            return;

        Application.ExternalCall("OnExternalCall", o);
    }

    //  ¥Ê¥¢u3dœ˚œ¢ 
    public ArrayList sends = new ArrayList();

    public void CallMsg(string o)
    {
        if (sends.Contains(o))
            return;

        sends.Add(o);
    }
#endregion

}
