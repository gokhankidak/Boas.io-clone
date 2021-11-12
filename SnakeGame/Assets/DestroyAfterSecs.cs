using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSecs : MonoBehaviour
{
    [SerializeField] int _destroyAfterMSeconds = 1;
    void Awake()
    {
        StartCoroutine(SelfDestruct(_destroyAfterMSeconds));
    }

    IEnumerator SelfDestruct(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
