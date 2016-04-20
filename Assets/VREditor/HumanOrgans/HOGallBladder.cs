using UnityEngine;
using System.Collections;

public class HOGallBladder : MonoBehaviour
{
    public int id;

    // Use this for initialization

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("HOGallBladder.OnTriggerEnter" + collider.gameObject.name + ":" + Time.time);

        if (collider.gameObject.layer == 8)
        {
            MedicalDevicesMgr.instance.Trigger( collider.gameObject.GetComponent<ComponentBlade>(), 
                                                this,
                                                Vector3.one);
        }

    }

    void OnTriggerQuit(Collider collider)
    {
        Debug.Log("HOGallBladder.OnTriggerQuit" + collider.gameObject.name + ":" + Time.time);

        if (collider.gameObject.layer == 8)
        {
            MedicalDevicesMgr.instance.Trigger( collider.gameObject.GetComponent<ComponentBlade>(), 
                                                this,
                                                Vector3.one);
        }

    }
}
