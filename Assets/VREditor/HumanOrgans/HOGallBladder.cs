using UnityEngine;
using System.Collections;
using U3DSceneEditor;

public class HOGallBladder : MonoBehaviour
{
    public int id;

    void Start()
    {
        GameObject prefabDN = GameObject.Find("DanNang");

        transform.position = prefabDN.transform.position;
        transform.localScale = prefabDN.transform.localScale;
        transform.rotation = prefabDN.transform.rotation;

        transform.GetComponent<MeshRenderer>().materials = prefabDN.transform.GetComponent<MeshRenderer>().materials;
    }

    // Use this for initialization

    void OnTriggerEnter(Collider collider)
    {
        Debuger.Log("HOGallBladder.OnTriggerEnter" + collider.gameObject.name + ":" + Time.time);

        if (collider.gameObject.layer == D3Config.Layer_MedicalDevices)
        {
            Acti(collider.gameObject);
        }

    }

    void OnTriggerExit(Collider collider)
    {
        Debuger.Log("HOGallBladder.OnTriggerQuit" + collider.gameObject.name + ":" + Time.time);

        if (collider.gameObject.layer == D3Config.Layer_MedicalDevices)
        {
            Unacti(collider.gameObject);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        Debuger.Log("HOGallBladder.OnCollisionEnter" + collision.gameObject.name + ":" + Time.time);

        if (collision.gameObject.layer == D3Config.Layer_MedicalDevices)
        {
            Acti(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debuger.Log("HOGallBladder.OnCollisionExit" + collision.gameObject.name + ":" + Time.time);

        if (collision.gameObject.layer == D3Config.Layer_MedicalDevices)
        {
            Unacti(collision.gameObject);
        }
    }


    void Acti(GameObject target)
    {
        target.gameObject.SendMessage("Selected", true);

        MedicalDevicesMgr.instance.Trigger(target.gameObject.GetComponent<ComponentBlade>(), 
                                            this,
                                            Vector3.one);

    }

    void Unacti(GameObject target)
    {
        target.gameObject.SendMessage("Selected", false);

        MedicalDevicesMgr.instance.Trigger(target.gameObject.GetComponent<ComponentBlade>(), 
                                            this,
                                            Vector3.one);

    }
}
