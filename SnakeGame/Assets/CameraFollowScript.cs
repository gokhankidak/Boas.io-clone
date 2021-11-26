using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] GameObject _player;

    [SerializeField] Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + offset;
    }
}
