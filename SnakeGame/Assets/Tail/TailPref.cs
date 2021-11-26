using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snake.Tail
{
    [CreateAssetMenu(fileName = "TailData", menuName = "ObjectPref/TailPref")]
    public class TailPref : ScriptableObject
    {
        [SerializeField]float followDistance = 2;
        [SerializeField] float speed = 2;
        [SerializeField] float processDelay = 0.1f;
        public float FollowDistance { get { return followDistance; }}
        public float Speed { get { return speed; }}

        public float ProcessDelay { get { return processDelay; } }

    }
}
