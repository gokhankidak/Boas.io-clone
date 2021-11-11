using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerController : MonoBehaviour
{
    [SerializeField] PlayerPref playerPref;
    Rigidbody rigidbody;

    private const float radianToDegree = 57.29f;
    private float _mouseAngle;
    private Vector3 _screenCenter;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        _mouseAngle = GetMouseAngle(Input.mousePosition);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3 (0,_mouseAngle,0));
        transform.Translate(Vector3.forward*Time.deltaTime * playerPref.speed);
    }

    private float GetMouseAngle(Vector3 mousePos)
    {
        mousePos -= _screenCenter;
        float sqrRoot = Mathf.Sqrt(mousePos.x*mousePos.x+mousePos.y*mousePos.y);
        return Mathf.Atan2(mousePos.x/sqrRoot,mousePos.y/sqrRoot)*radianToDegree;
    }

}
