using System;
using UnityEngine;
using UnityEngine.Events;

namespace Syc.Core.Interaction
{
    [Serializable]
    public class Interaction : UnityEvent<Interactor, Interactable> { }

    [Serializable]
    public class InteractionProposition : UnityEvent<Interactor, Interactable> { }

    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        public Interaction Interaction;
        public InteractionProposition InteractionProposition;

        [SerializeField]
        private bool isActive = true;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        public string InteractionMessage { get; set; } = "to interact";

        public void Interact(Interactor with)
        {
            Interaction.Invoke(with, this);
        }

        public void ProposeInteraction(Interactor with)
        {
            InteractionProposition.Invoke(with, this);
        }
    }
}
