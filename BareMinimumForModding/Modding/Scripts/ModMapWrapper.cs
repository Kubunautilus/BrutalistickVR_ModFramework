using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ModMapWrapper : MonoBehaviour
{
    public MapScriptableObject mapScriptableObject;
    [Tooltip("If the existing material types are not enough, you may opt to add a custom material. Just create a new parent object in the scene, drag it into this Custom Material Parent array, and add all the related objects to the correct index in the corresponding slots below.")]
    public GameObject[] customMaterialParent;
    public CustomMaterialType[] customMaterialInstance;
    public GameObject menuUI, weaponWall, spawnPointsParent;

    [System.Serializable]
    public struct CustomMaterialType
    {
        public GameObject particleSystemsParent;
        public AudioClip[] collisionAudioClips;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Selection.activeGameObject != null)
        {
            Transform spawnPointsTransform = null;
            Transform selectionParent = Selection.activeGameObject.transform.parent;
            if (selectionParent != null)
            {
                if (Selection.activeGameObject.transform.parent.gameObject.name.Contains("SpawnPoints"))
                {
                    spawnPointsTransform = Selection.activeGameObject.transform.parent;
                }
                else if (Selection.activeGameObject.name.Contains("SpawnPoints"))
                {
                    spawnPointsTransform = Selection.activeGameObject.transform;
                }
            }
            if (spawnPointsTransform != null)
            {
                GUIStyle style = new();
                style.fontSize = 11;
                style.normal.textColor = Color.white;
                style.alignment = TextAnchor.MiddleCenter;
                for (int i = 0; i < spawnPointsTransform.childCount; i++)
                {
                    Transform childTransform = spawnPointsTransform.GetChild(i);
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(childTransform.position, 0.09f);
                    Handles.Label(childTransform.position + Vector3.up * 0.18f, new GUIContent(childTransform.gameObject.name.Split(" ")[1]), style);
                }
            }
        }
    }
#endif
}
