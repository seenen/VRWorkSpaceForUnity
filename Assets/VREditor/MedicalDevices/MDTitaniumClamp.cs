using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MDTitaniumClamp : MedicalDevices
{
    public GameObject Target;


    public GameObject End_1_Root_Fixed;
    public GameObject End_2_Root_Cursor;

    public GameObject End_1_Target_Fixed;
    public GameObject End_2_Target_Cursor;

    public Vector3 Cursor_Min_LocalPos;
    public Vector3 Cursor_Max_LocalPos;
    public Vector3 Cursor_LocalPos;
    public Vector3 Dir;

    public float testopendegree = 0;

    void Start()
    {
        MedicalDevicesMgr.instance.SetLeft(gameObject);

        Cursor_Min_LocalPos = End_1_Target_Fixed.transform.localPosition;
        Cursor_Max_LocalPos = End_2_Target_Cursor.transform.localPosition;

        //  默认闭合
        Cursor_LocalPos = Cursor_Min_LocalPos;
    }

    /// <summary>
    /// 闭合 0 - 1 0表示闭合 1表示全部张开
    /// </summary>
    public void Progress(float opendegree)
    {
        testopendegree = Mathf.Clamp01(testopendegree);

        LocalDir();

        Cursor_LocalPos = Cursor_Min_LocalPos + Dir * testopendegree;

        End_2_Target_Cursor.transform.localPosition = Cursor_LocalPos;
    }

    void LocalDir()
    {
        Dir = Cursor_Max_LocalPos - Cursor_Min_LocalPos;

    }

    void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform);
        }

        End_2_Root_Cursor.transform.LookAt(End_2_Target_Cursor.transform.position);

#if UNITY_EDITOR
        Progress(testopendegree);
#endif
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("MDTitaniumClamp.OnTriggerEnter" + collider.gameObject.name + ":" + Time.time);

    }

    void OnTriggerQuit(Collider collider)
    {
        Debug.Log("MDTitaniumClamp.OnTriggerQuit" + collider.gameObject.name + ":" + Time.time);

    }

#region keyboard
    override public void WiseClock()
    {
        testopendegree += Time.deltaTime * 5;

        Progress(testopendegree);
    }

    override public void Clock()
    {
        testopendegree -= Time.deltaTime * 5;

        Progress(testopendegree);
    }
#endregion
}
