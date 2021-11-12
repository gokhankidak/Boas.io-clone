using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Tail
{
    public class MeshCreator : MonoBehaviour
    {

        [SerializeField] Material _material;
        private GameObject _nextPart;
      
        private void OnTriggerEnter(Collider other)
        {
            int _indexCounter=0;

            if(other.gameObject.GetComponent<TailFollowScript>() != null)
                _indexCounter = other.gameObject.GetComponent<TailFollowScript>().number;
            if (_indexCounter < 5)
                return;


            Vector3[] vertices = new Vector3[_indexCounter];
            Vector2[] uv = new Vector2[_indexCounter];
            int[] triangles = new int[((int)(_indexCounter/3))*9-6];

            _nextPart = null;


            for (int i = 0; i < vertices.Length; i++)
            {
                if (_nextPart == null)
                {
                    vertices[i] = other.gameObject.GetComponent<TailFollowScript>().nextObject.transform.position;
                    _nextPart = other.gameObject.GetComponent<TailFollowScript>().nextObject;
                }
                else if (_nextPart.name == "PlayerHead")
                {
                    break;
                }
                else
                {
                    _nextPart = _nextPart.GetComponent<TailFollowScript>().nextObject;
                    vertices[i] = _nextPart.transform.position;
                }
            }


            uv[0] = new Vector2(0, 1);
            uv[1] = new Vector2(1, 1);
            uv[2] = new Vector2(0, 0);
            uv[3] = new Vector2(1, 0);

            for (int i = 0; i * 3 + 2 < triangles.Length; i++)
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
            gameObject.AddComponent<DestroyAfterSecs>();
            gameObject.layer = LayerMask.NameToLayer("Destroyer");
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<MeshCollider>().sharedMesh = null;
            gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
            gameObject.GetComponent<MeshRenderer>().material = _material;
        }
    }
}
