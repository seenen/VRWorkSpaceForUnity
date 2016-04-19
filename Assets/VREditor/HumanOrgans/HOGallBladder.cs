using UnityEngine;
using System.Collections;

public class HOGallBladder : MonoBehaviour
{

    // Use this for initialization

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("HOGallBladder.OnTriggerEnter" + collider.gameObject.name + ":" + Time.time);

    }

    void OnTriggerQuit(Collider collider)
    {
        Debug.Log("HOGallBladder.OnTriggerQuit" + collider.gameObject.name + ":" + Time.time);

    }
}
