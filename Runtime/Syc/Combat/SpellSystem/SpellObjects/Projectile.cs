using System.Collections;
using Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects;
using Syc.Combat.TargetSystem;
using UnityEngine;

namespace Syc.Combat.SpellSystem.SpellObjects
{
    public class Projectile : SpellObject
    {
        [SerializeField] private float movementSpeed = 20;

        [SerializeField] private float rotationSpeed = 100;

        [SerializeField] private float rotationSpeedFactor = 50;

        [SerializeField] private float maxLifeTime = 10.0f;

        #region MonoBehaviour

        public override void Initialize()
        {
            SpellCast.OnSpellCompleted += FlyToTarget;
            SpellCast.OnSpellCancelled += DestroyGameObject;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != Target.TargetObject)
                return;
            
            Spell.ExecuteAll(SpellEffectTrigger.OnImpact, Source, new Target(Target.TargetObject, transform.position), SpellCast, this);
            DestroyGameObject(null);
        }
        
        protected virtual void OnDestroy()
        {
            SpellCast.OnSpellCompleted -= FlyToTarget;
            SpellCast.OnSpellCancelled -= DestroyGameObject;
        }
        
        #endregion

        protected virtual void DestroyGameObject(SpellCast _)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }

        protected virtual void FlyToTarget(SpellCast _)
        {
            transform.SetParent(null, true);
            StartCoroutine(FlyToTargetCoroutine());
        }

        protected virtual IEnumerator FlyToTargetCoroutine()
        {
            var currentLifeTime = 0f;
            
            while (maxLifeTime > currentLifeTime)
            {
                currentLifeTime += Time.deltaTime;

                rotationSpeed += Time.deltaTime * rotationSpeedFactor;
                
                var ownTransform = transform;
                var position = ownTransform.position;
                var positionDelta = Target.TargetObject != null 
                    ? Target.TargetObject.GetComponent<Collider>().bounds.center - position
                    : Target.Position - position;
                
                var desiredLookRotation =  Quaternion.LookRotation(positionDelta);
                ownTransform.rotation = Quaternion.RotateTowards(ownTransform.rotation, desiredLookRotation, rotationSpeed * Time.deltaTime);
                position += ownTransform.forward * (Time.deltaTime * movementSpeed);
                ownTransform.position = position;
                yield return null;
            }
            
            DestroyGameObject(null);
        }

    }
}
