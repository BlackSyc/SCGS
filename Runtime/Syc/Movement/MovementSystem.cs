using UnityEngine;

namespace Syc.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class MovementSystem : MonoBehaviour
    {
        #region Private Fields

        private CharacterController _characterController;

        private float _upwardsMovement;

        private Vector2 _movementInput = Vector2.zero;
        
        #endregion

        #region Public Properties
        public abstract IMovementAttributes MovementAttributes { get; }

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        #endregion

        [SerializeField]
        private bool isActive = true;

        [SerializeField] private bool disablePitchRotation;

        [SerializeField] private bool disableYawRotation;

        #region MonoBehaviour
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void LateUpdate()
        {
            if (!IsActive)
                return;
            
            var ownTransform = transform;

            var localMovement = _movementInput != Vector2.zero
                ? (ownTransform.right * _movementInput.x + ownTransform.forward * _movementInput.y) *
                  MovementAttributes.MovementSpeed.Remap()
                : Vector3.zero;

            localMovement.y = _upwardsMovement;

            if (!_characterController.isGrounded)
                localMovement += Physics.gravity * Time.deltaTime;

            _characterController.Move(localMovement * Time.deltaTime);
            
            _upwardsMovement = localMovement.y;
            _movementInput = Vector2.zero;
        }
        #endregion

        #region Public Methods
        public void Move(Vector2 movementInput)
        {
            _movementInput += movementInput;
        }

        public void Jump()
        {
            if (!IsActive)
                return;

            if (_characterController.isGrounded)
                _upwardsMovement = MovementAttributes.JumpPower.Remap();
        }

        public void Rotate(Vector2 rotationInput)
        {
            if (!IsActive)
                return;

            var ownTransform = transform;
            
            if (disableYawRotation)
                rotationInput.x = 0;

            if (disablePitchRotation)
                rotationInput.y = 0;
            
            var localRotation = (ownTransform.up * rotationInput.x + ownTransform.right * rotationInput.y) * MovementAttributes.RotationSpeed.Remap();


            
            transform.Rotate(localRotation * Time.deltaTime, Space.World);
        }
        #endregion
    }
}
