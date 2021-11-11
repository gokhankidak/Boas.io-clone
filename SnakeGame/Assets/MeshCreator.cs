using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    [SerializeField] GameObject[] points = new GameObject[6];
    [SerializeField] Material material;
    void Start()
    {
        
        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[12];

        for (int i = 0; i < points.Length; i++)
        {
            vertices[i] = new Vector3(points[i].transform.position.x, points[i].transform.position.y);
        }

        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 0);

        for (int i = 0; i*3+2 < triangles.Length; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }


        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GameObject gameObject = new GameObject("LocalPlayerMesh", typeof(MeshFilter), typeof(MeshRenderer));

        gameObject.AddComponent<MeshCollider>();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = null;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;
    }
}
