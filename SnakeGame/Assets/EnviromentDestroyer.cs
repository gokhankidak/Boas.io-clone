using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentDestroyer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroyer"))
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }

    }
}
