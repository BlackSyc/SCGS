using UnityEngine;

namespace Syc.Combat.SpellSystem.ScriptableObjects.SpellEffects.Triggers
{
	public abstract class SpellEffectTrigger : ScriptableObject { }
	
	//[CreateAssetMenu(menuName = "OnCastStarted")]
	public class OnCastStarted : SpellEffectTrigger { }
	
	//[CreateAssetMenu(menuName = "OnCastProgress")]
	public class OnCastProgress : SpellEffectTrigger { }
	
	//[CreateAssetMenu(menuName = "OnCastCompleted")]
	public class OnCastCompleted : SpellEffectTrigger { }
	
	//[CreateAssetMenu(menuName = "OnCastCancelled")]
	public class OnCastCancelled : SpellEffectTrigger { }
	
	//[CreateAssetMenu(menuName = "OnImpact")]
	public class OnImpact : SpellEffectTrigger { }
}