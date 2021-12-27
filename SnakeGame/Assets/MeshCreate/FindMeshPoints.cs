using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMeshPoints : MonoBehaviour
{
    GameObject _playerHead;
    [SerializeField] TailAdding tail;

    List<nodes> _nodeList = new List<nodes>();
    int _ignoredTails = 5;
    public struct nodes { public int x, y; };

    private void Start()
    {
        _playerHead = gameObject;
    }
    public List<Triangle> FindPoints(ref int _hittedTailIndex)
    {
        List<GameObject> temp = new List<GameObject>();
        List<Triangle> _tempTriangles;
        _nodeList.Clear();

        temp.Add(_playerHead);

        FindNodes(_hittedTailIndex);

        //In every situation we must add tail to first node
        if (_nodeList.Count > 0)
        {
            for (int i = 0; i < _nodeList[0].x; i++)
            {
                temp.Add(tail.tailsList[i]);
            }
        }

        #region outside node exceptions
        List<nodes> tempNodes = new List<nodes>();

        foreach (var node in _nodeList)
        {
            tempNodes.Add(node);
        }

        for (int z = 0; z < _nodeList.Count; z++)
        {
            if (_nodeList[z].y < _hittedTailIndex)// it could be outside of the zone, so find useless nodes and delete them
            {
                if (z < _nodeList.Count - 1 && _nodeList[z].y < _nodeList[z + 1].x)//if its not last element and its independent node
                {
                    for (int i = _nodeList[z].y; i < _nodeList[z + 1].x; i++)
                    {
                        temp.Add(tail.tailsList[i]);
                    }
                }
                else if (z == _nodeList.Count - 1)//if last element
                {
                    for (int i = _nodeList[z].y; i < _hittedTailIndex; i++)
                    {
                        temp.Add(tail.tailsList[i]);
                    }
                }
                else//otherwise add deleted nodes back
                {
                    _nodeList = tempNodes;
                    break;
                }
                _nodeList.RemoveAt(z);
                z--;
            }

            if (_nodeList.Count == 0)
            {
                _hittedTailIndex = temp.Count;

                _tempTriangles = Triangulate.TriangulateConcavePolygon(temp);
                return _tempTriangles;
            }
        }
        #endregion
        //its true if there is no other nodes between first point to hitted tail
        if (_nodeList.Count != 0 && _nodeList[0].y < _hittedTailIndex)
        {
            for (int i = _nodeList[0].y; i <= _hittedTailIndex; i++)
            {
                temp.Add(tail.tailsList[i]);
            }
        }

        else if (_nodeList.Count == 1)
        {
            for (int i = _nodeList[0].y; i > _hittedTailIndex; i--)
            {
                temp.Add(tail.tailsList[i]);
            }
        }
        else if (_nodeList.Count > 1)
        {
            bool _isIndieClosed = true;

            // If there is no nodes between first node to hitted tail, ignore other nodes
            for (int j = 0; j < _nodeList.Count - 1; j++)
            {
                if (_nodeList[0].y < _nodeList[j].y && _hittedTailIndex > _nodeList[j].y)
                {
                    _isIndieClosed = false;
                    break;
                }
            }
            //if its still true ignore other nodes
            if (_isIndieClosed)
            {
                nodes x = _nodeList[0];
                _nodeList.Clear();
                _nodeList.Add(x);
            }


            for (int z = 0; z < _nodeList.Count - 1; z++)
            {
                #region deletes nodes which is we dont use
                if (_nodeList[z + 1].x > _nodeList[z].x && _nodeList[z + 1].y < _nodeList[z].y) // next node is closed and independent 
                {
                    bool isDeleted = false;
                    if (z + 2 == _nodeList.Count)//is z+1  last element ?
                    {
                        for (int j = 0; j < _nodeList.Count - 1; j++)
                        {
                            if (_nodeList[z + 1].y < _nodeList[j].y && _hittedTailIndex > _nodeList[j].y)//if there are nodes between last nodes to hitted tail , delete them
                            {
                                _nodeList.RemoveAt(z + 1);
                                z--;
                                isDeleted = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        _nodeList.RemoveAt(z + 1);
                        isDeleted = true;
                        z--;
                    }
                    #endregion
                    if (_nodeList.Count == 1)
                    {
                        for (int i = _nodeList[0].y; i > _hittedTailIndex; i--)
                        {
                            temp.Add(tail.tailsList[i]);
                        }
                        break;
                    }
                    else if (isDeleted)
                        continue;
                }

                #region Find shortest points in nodes
                if (Mathf.Abs(_nodeList[z + 1].x - _nodeList[z].x) > Mathf.Abs(_nodeList[z + 1].y - _nodeList[z].y))
                {
                    if (_nodeList[z].y < _nodeList[z + 1].y)
                    {
                        for (int i = _nodeList[z].y; i < _nodeList[z + 1].y; i++)
                        {
                            temp.Add(tail.tailsList[i]);
                        }
                    }
                    else
                    {
                        for (int i = _nodeList[z].y; i > _nodeList[z + 1].y; i--)
                        {
                            temp.Add(tail.tailsList[i]);
                        }
                    }
                }
                else
                {
                    for (int i = _nodeList[z].x; i < _nodeList[z + 1].x; i++)
                    {
                        temp.Add(tail.tailsList[i]);
                    }
                }
                #endregion
            }
            //For last element
            //Add closest node to hitted tail
            #region Add closest node
            if ((_nodeList[_nodeList.Count - 1].y - _hittedTailIndex) < (_hittedTailIndex - _nodeList[_nodeList.Count - 1].x))
            {
                if (_nodeList[_nodeList.Count - 1].y < _hittedTailIndex)
                {
                    for (int i = _nodeList[_nodeList.Count - 1].y; i <= _hittedTailIndex; i++)
                    {
                        temp.Add(tail.tailsList[i]);
                    }
                }
                else
                {
                    for (int i = _nodeList[_nodeList.Count - 1].y; i >= _hittedTailIndex; i--)
                    {
                        temp.Add(tail.tailsList[i]);
                    }
                }
            }
            else
            {
                if (_nodeList[_nodeList.Count - 1].x < _hittedTailIndex)
                {
                    for (int i = _nodeList[_nodeList.Count - 1].x; i <= _hittedTailIndex; i++)
                    {
                        temp.Add(tail.tailsList[i]);
                    }
                }
                else
                {
                    for (int i = _nodeList[_nodeList.Count - 1].x; i >= _hittedTailIndex; i--)
                    {
                        temp.Add(tail.tailsList[i]);
                    }
                }
            }
            #endregion
        }

        //for last connection
        else
        {
            for (int i = 0; i <= _hittedTailIndex; i++)
            {
                temp.Add(tail.tailsList[i]);
            }
        }

        _hittedTailIndex = temp.Count;

        if (Vector3.Distance(temp[0].transform.position, temp[temp.Count - 1].transform.position) > tail.tailsList[0].transform.localScale.x * 2)
            return null;

        _tempTriangles = Triangulate.TriangulateConcavePolygon(temp);
        return _tempTriangles;
    }

    private void FindNodes(int _hittedTailIndex)
    {
        for (int i = _ignoredTails; i <= _hittedTailIndex; i++)
        {
            for (int j = i + 1; j < tail.tailsList.Count; j++)
            {
                if (Vector3.Distance(tail.tailsList[j].transform.position, tail.tailsList[i].transform.position) < tail.tailsList[0].transform.localScale.x / 1.5f)
                {
                    nodes tempNode;
                    tempNode.x = i;
                    tempNode.y = j;
                    _nodeList.Add(tempNode);
                    i++;
                    break;
                }
            }
        }
    }
}
