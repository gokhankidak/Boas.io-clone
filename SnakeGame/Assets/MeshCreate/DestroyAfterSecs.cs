using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSecs : MonoBehaviour
{
    [SerializeField] float _destroyAfterMSeconds = .5f;

    void Start()
    {
        StartCoroutine(SelfDestruct(_destroyAfterMSeconds));
    }
    IEnumerator SelfDestruct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

