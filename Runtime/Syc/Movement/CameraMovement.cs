using UnityEngine;

namespace Syc.Movement
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraPivot;
        [SerializeField]
        private LayerMask ignoreRaycastLayerMask;

        [SerializeField]
        private float zoomSpeed = 2;
        [SerializeField]
        private float zoomDistanceMultiplier = 0.001f;
        [SerializeField]
        private float minZoomDistance = 2.5f;
        [SerializeField]
        private float maxZoomDistance = 5f;
        [SerializeField]
        private float preferedZoomDistance = 3.5f;

        public void Update()
        {
            if (transform.parent != cameraPivot)
            {
                transform.parent = cameraPivot;
                transform.LookAt(cameraPivot);
            }

            if (IsVisionObstructed(out Vector3 newPosition))
            {
                transform.position = newPosition;
            }
            else
            {
                SmoothZoom();
            }
        }

        private bool IsVisionObstructed(out Vector3 newPosition)
        {
            newPosition = transform.position;

            var pivotPosition = cameraPivot.position;
            var cameraPosition = newPosition;

            var cameraDistance = Vector3.Distance(pivotPosition, cameraPosition);
            var raycastDirection = cameraPosition - pivotPosition;

            if (Physics.Raycast(pivotPosition, raycastDirection, out RaycastHit hit, preferedZoomDistance, ~ignoreRaycastLayerMask))
            {
                var hitPointDistance = Vector3.Distance(pivotPosition, hit.point);
                if (hitPointDistance < cameraDistance + 0.05f)
                {
                    newPosition = hit.point;
                    Debug.DrawLine(pivotPosition, cameraPosition, Color.red);
                    return true;
                }
            }

            Debug.DrawLine(pivotPosition, cameraPosition, Color.green);
            return false;
        }

        // Zoom input is inputsystems CallBackContext.ReadValue()
        public void Zoom(Vector2 zoomInput)
        {
            preferedZoomDistance -= zoomInput.y * zoomDistanceMultiplier;
            preferedZoomDistance = Mathf.Clamp(preferedZoomDistance, minZoomDistance, maxZoomDistance);
        }

        private void SmoothZoom()
        {
            var distanceFromPivot = Vector3.Distance(transform.position, cameraPivot.transform.position);
            var distanceToPreference = Mathf.Abs(preferedZoomDistance - distanceFromPivot);
            var actualZoomSpeed = zoomSpeed * distanceToPreference;

            if (preferedZoomDistance + 0.05f < distanceFromPivot) // zoom in
            {
                transform.Translate(transform.forward * (actualZoomSpeed * Time.deltaTime), Space.World);
            }
            else if (preferedZoomDistance - 0.05f > distanceFromPivot) // zoom out 
            {
                transform.Translate(-transform.forward * (actualZoomSpeed * Time.deltaTime), Space.World);
            }
        }
    }
}
