using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAdding : MonoBehaviour
{
    [SerializeField] GameObject _tail;
    [SerializeField] GameObject _head;
    GameObject temp;
    [SerializeField] int _snakeLenght = 10;
    
    static List<GameObject> _tailsList = new List<GameObject>();
    public List<GameObject> tailsList { get => _tailsList; } 

    private void Awake()
    {
        AddTails();
    }

    private void AddTails()
    {
        _tailsList.Add(_head);
        for (int i = 0; i < _snakeLenght - 1; i++)
        {
            temp = Instantiate(_tail, _head.transform.position, Quaternion.identity);
            temp.GetComponent<TailIndex>().tailIndex = i;
            _tailsList.Add(temp);
        }
    }
}

