using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshCreator : MonoBehaviour
{
    [SerializeField] Material _material;

    int _ignoredTails = 5;
    static bool _isActive = false;  

    private void Start()
    {
        StartCoroutine(ActiveAfterSec(1f));
    }

    private IEnumerator ActiveAfterSec(float time)
    {
        yield return new WaitForSeconds(time);
        _isActive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player") || !_isActive) return;

        int _hittedTailIndex = other.gameObject.GetComponent<TailIndex>().tailIndex;
        List<Triangle> _tempTriangles = new List<Triangle>();

        if (_hittedTailIndex > _ignoredTails)
        {
            _tempTriangles = gameObject.GetComponent<FindMeshPoints>().FindPoints(ref _hittedTailIndex);
            if (_tempTriangles == null) return;
        }
        else return;

        if (_tempTriangles.Count == 0) return;

        _isActive = false;
        StartCoroutine(ActiveAfterSec(.4f));
        CreateMesh(_hittedTailIndex, _tempTriangles);
    }

    private void CreateMesh(int _hittedTailIndex, List<Triangle> _tempTriangles)
    {
        Mesh mesh = new Mesh();
        int[] _triangles = new int[(_hittedTailIndex - 2) * 3];
        Vector3[] newVertices = new Vector3[_tempTriangles.Count * 3 + 3];

        for (int i = 0; i < _tempTriangles.Count; i++)
        {
            newVertices[i * 3] = _tempTriangles[i].v1.position;
            newVertices[i * 3 + 1] = _tempTriangles[i].v2.position;
            newVertices[i * 3 + 2] = _tempTriangles[i].v3.position;
        }

        for (int i = 0; i < _triangles.Length; i++)
        {
            _triangles[i] = i;
        }

        mesh.vertices = newVertices;
        mesh.triangles = _triangles;

        GameObject gameObject = new GameObject("LocalPlayerMesh", typeof(MeshFilter), typeof(MeshRenderer));

        gameObject.AddComponent<MeshCollider>();
        gameObject.AddComponent<DestroyAfterSecs>();
        gameObject.layer = LayerMask.NameToLayer("Destroyer");
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = null;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = _material;
    }
}

