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
        [SerializeField] PlayerPref playerPref;
        [SerializeField] int _snakeLenght = 10;
        [SerializeField] float _followDistance = 0.6f;
        [SerializeField] float _startingDelay = 0.05f;
        List<GameObject> _tailsList = new List<GameObject>();
        bool _isActive = false;

        private void Awake()
        {
            _tailsList.Add(_head);
            for (int i = 0; i < _snakeLenght - 1; i++)
            {
                temp = Instantiate(_tail, _head.transform.position, Quaternion.identity);
                _tailsList.Add(temp);
            }
            //wait some for fill the marker
            StartCoroutine(FollowDelay(_startingDelay));
        }

        private void Update()
        {
            for (int i = 1; i < _tailsList.Count; i++)
            {
                _tailsList[i].transform.localScale = new Vector3(playerPref.Scale, playerPref.Scale, playerPref.Scale);
            }
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                for (int i = 1; i < _tailsList.Count; i++)
                {
                    MarkerManager markerM = _tailsList[i - 1].GetComponent<MarkerManager>();
                    _tailsList[i].transform.position = Vector3.Lerp(_tailsList[i].transform.position, markerM.markerList[0].position, _followDistance);
                    _tailsList[i].transform.rotation = Quaternion.Lerp(_tailsList[i].transform.rotation, markerM.markerList[0].rotation, _followDistance);
                    markerM.markerList.RemoveAt(0);
                }
            }
        }

        IEnumerator FollowDelay(float _time)
        {
            yield return new WaitForSeconds(_time);
            _isActive = true;
        }
    }
}
