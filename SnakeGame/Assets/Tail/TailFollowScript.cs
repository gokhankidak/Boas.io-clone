using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public class TailFollowScript : TailFollowAbstract
    {
        public int number = 0;
        //private float _reachTime = 0.02f;
        //private Vector3 _currentTarget;
        //// Update is called once per frame

        //private void Start()
        //{
        //    _currentTarget = nextObject.transform.position;
        //}

        //void Update()
        //{
        //    //if (positions.Count == 0)

        //}
        //void FixedUpdate()
        //{
        //    if (Vector3.Distance(transform.position, _currentTarget) != 0)
        //    {
        //        if (positions.Count > 0)
        //        {
        //            _currentTarget = positions.Pop();
        //            transform.position = Vector3.Lerp(transform.position, _currentTarget, _reachTime);
        //            transform.LookAt(nextObject.transform);
        //        }
        //        else
        //        {
        //            if (nextObject.GetComponent<TailFollowScript>() != null)
        //            {
        //                positions = nextObject.GetComponent<TailFollowScript>().positions;
        //            }
        //            positions.Push(nextObject.transform.position);
        //        }
        //    }
        //    else
        //        _currentTarget = nextObject.transform.position;
        //}
    }
}
