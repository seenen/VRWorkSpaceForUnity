using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MDScissors : MedicalDevices
{

    public GameObject End_1_Root_Cursor;
    public GameObject End_2_Root_Cursor;

    public GameObject End_1_Target_Cursor;
    public GameObject End_2_Target_Cursor;

    public Vector3 Dir1;
    public Vector3 Dir2;

    public Vector3 Cursor_1_LocalPos;
    public Vector3 Cursor_2_LocalPos;

    public Vector3 Cursor_1_CurPos;
    public Vector3 Cursor_2_CurPos;

    public float testopendegree = 0;

    // Use this for initialization
    void Start ()
    {
        MedicalDevicesMgr.instance.SetRight(gameObject);

        Cursor_1_LocalPos = End_1_Target_Cursor.transform.localPosition;
        Cursor_2_LocalPos = End_2_Target_Cursor.transform.localPosition;

        Vector3 center = (Cursor_1_LocalPos + Cursor_2_LocalPos) / 2;

        Dir1 = center - Cursor_1_LocalPos;
        Dir2 = center - Cursor_2_LocalPos;

    }

    // Update is called once per frame
    void Update ()
    {
        End_1_Root_Cursor.transform.LookAt(End_1_Target_Cursor.transform.position);
        End_2_Root_Cursor.transform.LookAt(End_2_Target_Cursor.transform.position);

#if UNITY_EDITOR
        Progress(testopendegree);
#endif

    }

    /// <summary>
    /// 闭合 0 - 1 0表示闭合 1表示全部张开
    /// </summary>
    public void Progress(float opendegree)
    {
        testopendegree = Mathf.Clamp01(testopendegree);

        Cursor_1_CurPos = Cursor_1_LocalPos + Dir1 * testopendegree;
        End_1_Target_Cursor.transform.localPosition = Cursor_1_CurPos;

        Cursor_2_CurPos = Cursor_2_LocalPos + Dir2 * testopendegree;
        End_2_Target_Cursor.transform.localPosition = Cursor_2_CurPos;

    }


    void OnTriggerEnter(Collider collider)
    {
        Debuger.Log("MDScissors.OnTriggerEnter" + collider.gameObject.name + ":" + Time.time);

    }

    void OnTriggerQuit(Collider collider)
    {
        Debuger.Log("MDScissors.OnTriggerQuit" + collider.gameObject.name + ":" + Time.time);

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
