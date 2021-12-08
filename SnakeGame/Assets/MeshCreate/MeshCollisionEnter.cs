using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCollisionEnter : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering is succesfull with " + other.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision is successful with : " + collision.gameObject.name);
    }


}
