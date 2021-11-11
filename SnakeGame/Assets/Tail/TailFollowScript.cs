using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public class TailFollowScript : TailFollowAbstract
    {
        private float _distance = 0;
        // Update is called once per frame
        void FixedUpdate()
        {
            _distance = Vector3.Distance(this.transform.position, nextObject.transform.position);
            _distance = _distance > 1 ? _distance : 1;
            transform.position += (Vector3.forward * Time.deltaTime * tailPref.Speed * _distance);

            transform.LookAt(nextObject.transform);
        }
    }
}
