using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Syc.Combat.SpellSystem.SpellObjects
{
    public static class AssetCreateMenu
    {
        [MenuItem ("Assets/Create/Spell System/Spells/Spell Objects/Projectile Prefab")]
        public static void CreateProjectilePrefab()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = "/Projectile";
            var fileExtension = ".prefab";

            var fileExists = AssetDatabase.LoadAssetAtPath<Object>(path + fileName + fileExtension) != null;
            
            Debug.Log("Hoi");
            Object source = Resources.Load("ProjectilePrefab");
            if (source == null)
            {
                Debug.Log("source was null");
                return;
            }
            GameObject objSource = (GameObject)PrefabUtility.InstantiatePrefab(source);
            if (objSource == null)
            {
                Debug.Log("objSource was null");
                return;
            }

            var writeTo = fileExists
                ? path + fileName + Guid.NewGuid() + fileExtension
                : path + fileName + fileExtension;
            
            GameObject obj = PrefabUtility.SaveAsPrefabAsset(objSource, writeTo);
            
            Object.DestroyImmediate(objSource);
        }
        
        [MenuItem ("Assets/Create/Spell System/Spells/Spell Objects/Burst Particles Prefab")]
        public static void CreateBurstParticlesPrefab()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = "/BurstParticles";
            var fileExtension = ".prefab";

            var fileExists = AssetDatabase.LoadAssetAtPath<Object>(path + fileName + fileExtension) != null;
            
            Debug.Log("Hoi");
            Object source = Resources.Load("BurstParticlesPrefab");
            if (source == null)
            {
                Debug.Log("source was null");
                return;
            }
            GameObject objSource = (GameObject)PrefabUtility.InstantiatePrefab(source);
            if (objSource == null)
            {
                Debug.Log("objSource was null");
                return;
            }

            var writeTo = fileExists
                ? path + fileName + Guid.NewGuid() + fileExtension
                : path + fileName + fileExtension;
            
            GameObject obj = PrefabUtility.SaveAsPrefabAsset(objSource, writeTo);
            
            Object.DestroyImmediate(objSource);
        }
    }
}
