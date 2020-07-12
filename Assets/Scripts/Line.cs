using System.Collections;
using UnityEngine;

namespace DrawRace
{
    public class Line : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(nameof(WaitAndSetupMesh));
        }

        private IEnumerator WaitAndSetupMesh()
        {
            yield return new WaitForFixedUpdate();
            var mesh = new Mesh();
            GetComponent<LineRenderer>().BakeMesh(mesh);
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}