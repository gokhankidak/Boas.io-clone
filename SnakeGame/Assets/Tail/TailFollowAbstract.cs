using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public abstract class TailFollowAbstract : MonoBehaviour
    {
        public Stack<Vector3> positions = new Stack<Vector3>(); 
        public GameObject nextObject;
    }
}
