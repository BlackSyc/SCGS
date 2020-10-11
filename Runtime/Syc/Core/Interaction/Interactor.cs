using System;
using System.Collections.Generic;
using Syc.Core.System;
using Syc.Core.Utility.Extensions;
using UnityEngine;

namespace Syc.Core.Interaction
{
    public class Interactor : MonoSystemBase
    {
        public event Action OnInteracted;

        public event Action<Interactable> OnInteractionProposed;

        public event Action OnProposedInteractionCancelled;
        
        [SerializeField]
        private List<MonoSubSystem> subSystems;

        [SerializeField]
        private LayerMask layers;

        private Interactable _currentInteractable;

        public void Awake()
        {
            foreach (var subSystem in subSystems)
                AddSubsystem(subSystem);
        }

        public void Interact()
        {
            if (_currentInteractable == null)
                return;
            
            _currentInteractable.Interact(with: this);
            OnInteracted?.Invoke();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_currentInteractable != null)
                return;
            
            if (!layers.Contains(other.gameObject.layer))
                return;

            var interactable = other.GetComponent<Interactable>();

            if (interactable == null || !interactable.IsActive)
                return;
            
            ProposeInteraction(interactable);
        }

        public void OnTriggerExit(Collider other)
        {
            if(_currentInteractable != null && other.GetComponent<Interactable>() == _currentInteractable)
                CancelProposedInteraction();
        }

        private void ProposeInteraction(Interactable interactable)
        {
            _currentInteractable = interactable;
            _currentInteractable.ProposeInteraction(with: this);
            OnInteractionProposed?.Invoke(interactable);
        }

        private void CancelProposedInteraction()
        {
            _currentInteractable = null;
            OnProposedInteractionCancelled?.Invoke();
        }
    }
}
