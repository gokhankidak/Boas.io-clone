using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] float _lerpSpeed = 3f;

    [SerializeField] Vector3 offset;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _player.transform.position + offset,_lerpSpeed*Time.fixedDeltaTime);
    }
}
