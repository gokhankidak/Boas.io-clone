using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailFollow : MonoBehaviour
{
    [SerializeField] PlayerPref playerPref;
    [SerializeField] float _followDistance = 0.6f;
    [SerializeField] float _startingDelay = 0.05f;

    [SerializeField] TailAdding  tail;
    bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //Wait some for fill the marker
        StartCoroutine(FollowDelay(_startingDelay));
    }

    // Update is called once per frame
    private void Update()
    {
        ScaleFollow();
    }

    private void FixedUpdate()
    {
        Follow();
    }

    private void ScaleFollow()
    {
        for (int i = 0; i < tail.tailsList.Count; i++)
        {
            tail.tailsList[i].transform.localScale = new Vector3(playerPref.Scale, playerPref.Scale, playerPref.Scale);
        }
    }

    private void Follow()
    {
        if (_isActive)
        {
            for (int i = 1; i < tail.tailsList.Count; i++)
            {
                MarkerManager markerM = tail.tailsList[i - 1].GetComponent<MarkerManager>();
                tail.tailsList[i].transform.position = Vector3.Lerp(tail.tailsList[i].transform.position, markerM.markerList[0].position, _followDistance);
                tail.tailsList[i].transform.rotation = Quaternion.Lerp(tail.tailsList[i].transform.rotation, markerM.markerList[0].rotation, _followDistance);
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
