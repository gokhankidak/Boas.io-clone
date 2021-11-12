using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public class TailFollowScript : TailFollowAbstract
    {
        private float _distance = 0;
        public int number = 0;
        // Update is called once per frame
        void FixedUpdate()
        {
            _distance = Vector3.Distance(nextObject.transform.position, this.transform.position);

            this.transform.position = Vector3.Lerp(transform.position, nextObject.transform.position, tailPref.FollowDistance/10);
            transform.LookAt(nextObject.transform);
        }
    }
}
