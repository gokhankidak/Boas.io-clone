using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public class MeshCreator : MonoBehaviour
    {
        [SerializeField] Material _material;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

            int _tailIndex = other.gameObject.GetComponent<TailIndex>().tailIndex;


            List<Triangle> _tempTriangles = new List<Triangle>();
            Mesh mesh = new Mesh();

            if (_tailIndex > 5)
            {
                List<GameObject> temp = new List<GameObject>();

                for (int i = 0; i < _tailIndex; i++)
                {
                    for (int j = i+1; j < _tailIndex; j++)
                    {
                        if(Vector3.Distance(TailAdding.tailsList[j].transform.position, TailAdding.tailsList[i].transform.position)<TailAdding.tailsList[0].transform.localScale.x/3)
                        {
                            _tailIndex = i+1;
                            break;
                        }
                    }
                }

                //indexe kadar olan objeleri göndermek için
                for (int i = 0; i < _tailIndex; i++)
                {
                    temp.Add(TailAdding.tailsList[i]);
                }

                //eklenen tail objelerinin olduðu liste gönderiliyor
                _tempTriangles = Triangulate.TriangulateConcavePolygon(temp);
            }
            else return;

            int[] _triangles = new int[(_tailIndex - 2) * 3];
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
}
