using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnviromentDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Destroyer"))
        {
            transform.parent.GetComponent<Collider>().enabled = false;
            Destroy(transform.parent.gameObject, 1f);
        }
    }
}
