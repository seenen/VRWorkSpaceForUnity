using UnityEngine;
using System.Collections;
using System;

public class MedicalDevices : MonoBehaviour, IKeyBoardControl
{
    public int id;

    virtual public void Clock()
    {
    }

    virtual public void WiseClock()
    {
    }

    virtual public void Down()
    {
        transform.position -= transform.forward * 5 * Time.deltaTime;
    }

    virtual public void Left()
    {
        transform.Rotate(Vector3.forward, 50 * Time.deltaTime, Space.Self);
    }

    virtual public void Right()
    {
        transform.Rotate(Vector3.forward, -50 * Time.deltaTime, Space.Self);
    }

    virtual public void Up()
    {
        transform.position += transform.forward * 5 * Time.deltaTime;
    }
}
