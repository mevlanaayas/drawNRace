using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DrawRace
{
    public class Wheel : MonoBehaviour
    {
        public GameObject linePrefab;
        public GameObject touchArea;
        public List<Vector2> fingerPositions;
        public int positionLimit = 25;

        private GameObject _line;
        private LineRenderer _lineRenderer;
        private MeshCollider _meshCollider;
        private Camera _camera;

        private GameObject _linePreview;
        private LineRenderer _lineRendererPreview;

        private float _deltaX;
        private float _deltaY;

        private static readonly Vector2 ReverseScaleVector = new Vector2(-1, -1);

        public void Start()
        {
            _camera = Camera.main;
            if (_camera != null)
            {
                PreparePreviewComponents();
            }
            else
            {
                Debug.LogError("No main camera.");
            }

            _line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
            _line.transform.localPosition = Vector3.zero;
            _lineRenderer = _line.GetComponent<LineRenderer>();
            _meshCollider = _line.GetComponent<MeshCollider>();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ValidateTouchPosition())
                {
                    InsertFingerPosition();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                UpdateLineRendererPositionList();
            }

            if (!Input.GetMouseButton(0))
            {
                return;
            }

            if (ValidateTouchPosition())
            {
                InsertFingerPosition();
            }
        }

        private void ClearLine()
        {
            fingerPositions.Clear();
            _lineRenderer.positionCount = 0;
        }

        private void ClearPreview()
        {
            _lineRendererPreview.positionCount = 0;
        }

        private bool ValidateTouchPosition()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, Mathf.Infinity, 1 << 8);
        }

        private void PreparePreviewComponents()
        {
            var transformRotation = _camera.transform.rotation;
            // touchArea.transform.rotation = transformRotation;
            _linePreview = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
            _linePreview.transform.rotation = transformRotation;
            var transformPosition = touchArea.transform.position;
            _linePreview.transform.position = new Vector3(transformPosition.x - 0.1f, transformPosition.y + 0.1f,
                transformPosition.z - 0.1f);
            _linePreview.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            _lineRendererPreview = _linePreview.GetComponent<LineRenderer>();
        }

        private void UpdateLineRendererPositionList()
        {
            var newPositionList = new LinkedList<Vector3>();
            var previewPositions = new List<Vector3>();
            fingerPositions.ForEach(fingerPosition =>
            {
                newPositionList.AddLast(fingerPosition);
                previewPositions.Add(fingerPosition);
            });
            fingerPositions.ForEach(fingerPosition =>
                newPositionList.AddFirst(Vector2.Scale(fingerPosition, ReverseScaleVector)));

            FinalizeLine(newPositionList);
            FinalizeLinePreview(previewPositions);
        }

        private void FinalizeLine(IReadOnlyCollection<Vector3> newPositionList)
        {
            ClearLine();
            _lineRenderer.positionCount = newPositionList.Count;
            _lineRenderer.SetPositions(newPositionList.ToArray());
            DrawMesh();
        }

        private void FinalizeLinePreview(List<Vector3> previewPositionList)
        {
            ClearPreview();
            _lineRendererPreview.positionCount = previewPositionList.Count;
            _lineRendererPreview.SetPositions(previewPositionList.ToArray());
        }

        private void DrawMesh()
        {
            var mesh = new Mesh();
            _lineRenderer.BakeMesh(mesh);
            _meshCollider.sharedMesh = mesh;
        }

        private void InsertFingerPosition()
        {
            if (fingerPositions.Count > positionLimit)
            {
                return;
            }

            var plane = new Plane(_camera.transform.forward, transform.position);
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector2 fingerPosition = plane.Raycast(ray, out var rayDistance)
                ? ray.GetPoint(rayDistance)
                : _camera.ScreenToWorldPoint(Input.mousePosition);

            if (fingerPositions.Count > 0 && Vector2.Distance(fingerPosition, fingerPositions.Last()) <= 0.1f)
            {
                return;
            }

            if (fingerPositions.Count == 0)
            {
                _deltaX = fingerPosition.x;
                _deltaY = fingerPosition.y;
                fingerPosition.x -= _deltaX;
                fingerPosition.y -= _deltaY;
                fingerPositions.Add(fingerPosition);
            }
            else
            {
                fingerPosition.x -= _deltaX;
                fingerPosition.y -= _deltaY;
                fingerPositions.Add(fingerPosition);
            }
        }
    }
}