using Syc.Movement;
using UnityEngine;

namespace Syc.Game.LocalPlayer
{
    public class LocalPlayerMovementSystem : MovementSystem
    {
        #region Private Fields
        
        [SerializeField] private CameraPivot cameraPivot;

        [SerializeField] private DefaultAttributesSystem movementDefaultAttributes;

        [SerializeField] private InputSystem input;

        #endregion

        public override IMovementAttributes MovementAttributes => movementDefaultAttributes;
        
        #region MonoBehaviour

        private void Awake()
        {
            input.OnMove += Move;
            input.OnLook += Rotate;
            input.OnLook += RotateCameraPivot;
            input.OnJump += Jump;
        }

        private void OnDestroy()
        {
            input.OnMove -= Move;
            input.OnLook -= Rotate;
            input.OnLook -= RotateCameraPivot;
            input.OnJump -= Jump;
        }

        #endregion

        #region MyRegion

        private void RotateCameraPivot(Vector2 lookInput)
        {
            cameraPivot.Rotate(lookInput, MovementAttributes.RotationSpeed.Remap());
        }

        #endregion
    }
}
