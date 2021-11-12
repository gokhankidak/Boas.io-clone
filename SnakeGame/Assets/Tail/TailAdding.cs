using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public class TailAdding : MonoBehaviour
    {
        [SerializeField] GameObject _tail;
        [SerializeField] GameObject _head;
        GameObject _nextObject, temp;
        [SerializeField] int _snakeLenght = 10;

        private void Start()
        {
            temp = Instantiate(_tail, _head.transform.position, Quaternion.identity);
            temp.GetComponent<TailFollowAbstract>().nextObject = _head;
            Destroy(temp.GetComponent<Collider>());
            for (int i = 0; i < _snakeLenght - 1; i++)
            {
                _nextObject = Instantiate(_tail, temp.transform.position, Quaternion.identity);
                _nextObject.GetComponent<TailFollowAbstract>().nextObject = temp;
                temp = _nextObject;
                temp.GetComponent<TailFollowScript>().number = i+1;
            }
        }
    }
}
