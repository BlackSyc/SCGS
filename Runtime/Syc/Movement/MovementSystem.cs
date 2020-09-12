using UnityEngine;

namespace Syc.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class MovementSystem : MonoBehaviour
    {
        #region Private Fields

        private CharacterController _characterController;

        private float _upwardsMovement;
        
        #endregion

        #region Public Properties
        public abstract IMovementAttributes MovementAttributes { get; }

        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }
        #endregion

        #region Public Methods
        public void Move(Vector2 movementInput)
        {
            var ownTransform = transform;
            var localMovement = (ownTransform.right * movementInput.x 
                                 + ownTransform.forward * movementInput.y) * MovementAttributes.MovementSpeed.Remap();

            localMovement.y = _upwardsMovement;

            if (!_characterController.isGrounded)
                localMovement += Physics.gravity * Time.fixedDeltaTime;

            _upwardsMovement = localMovement.y;

            _characterController.Move(localMovement * Time.fixedDeltaTime);

        }

        public void Jump()
        {
            if (_characterController.isGrounded)
                _upwardsMovement = MovementAttributes.JumpPower.Remap();
        }

        public void Rotate(Vector2 rotationInput)
        {
            var ownTransform = transform;
            var localRotation = (ownTransform.up * rotationInput.x + ownTransform.right * rotationInput.y) * MovementAttributes.RotationSpeed.Remap();
            transform.Rotate(localRotation * Time.deltaTime, Space.World);
        }
        #endregion
    }
}
