using UnityEngine;
using System.Collections;

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
        Debug.Log("HOGallBladder.OnTriggerEnter" + collider.gameObject.name + ":" + Time.time);

        if (collider.gameObject.layer == 8)
        {
            collider.gameObject.SendMessage("Selected", true);

            MedicalDevicesMgr.instance.Trigger( collider.gameObject.GetComponent<ComponentBlade>(), 
                                                this,
                                                Vector3.one);

        }

    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("HOGallBladder.OnTriggerQuit" + collider.gameObject.name + ":" + Time.time);

        if (collider.gameObject.layer == 8)
        {
            collider.gameObject.SendMessage("Selected", false);

            MedicalDevicesMgr.instance.Trigger( collider.gameObject.GetComponent<ComponentBlade>(), 
                                                this,
                                                Vector3.one);

        }

    }
}
