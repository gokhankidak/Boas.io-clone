using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public class TailAdding : MonoBehaviour
    {
        [SerializeField] GameObject _tail;
        [SerializeField] GameObject _head;
        GameObject temp;
        [SerializeField] int _snakeLenght = 10;
        [SerializeField] float _reachTime = 0.05f;
        List<GameObject> _tailsList = new List<GameObject>();

        private void Awake()
        {
            _tailsList.Add(_head);
            for (int i = 0; i < _snakeLenght - 1; i++)
            {
                temp = Instantiate(_tail, _head.transform.position, Quaternion.identity);
                _tailsList.Add(temp);
            }
        }

        private void FixedUpdate()
        {
            float _time = Time.deltaTime / _reachTime;
            for (int i = 1; i < _tailsList.Count; i++)
            {
                StartCoroutine(FollowDelay(_reachTime, i));
            }
        }

        IEnumerator FollowDelay(float _time,int i)
        {
            yield return new WaitForSeconds(_time);

            MarkerManager markerM = _tailsList[i - 1].GetComponent<MarkerManager>();
            _tailsList[i].transform.position = Vector3.Lerp(_tailsList[i].transform.position, markerM.markerList[0].position, _time);
            _tailsList[i].transform.rotation = markerM.markerList[0].rotation;
            markerM.markerList.RemoveAt(0);
        }

    }
}
