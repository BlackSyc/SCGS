using UnityEngine;

namespace Syc.Movement
{
    public class CameraPivot : MonoBehaviour
    {
        #region Private Fields
        [SerializeField]
        private float minXRotation = -45;
        [SerializeField]
        private float maxXRotation = 45;

        private float _xRotation;
        #endregion

        #region Public Methods
        public void Rotate(Vector2 rotationInput, float rotationSpeed)
        {
            var lookInput = rotationInput * (rotationSpeed * Time.deltaTime);

            _xRotation -= lookInput.y;
            _xRotation = Mathf.Clamp(_xRotation, minXRotation, maxXRotation);

            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        }
        #endregion
    }
}
