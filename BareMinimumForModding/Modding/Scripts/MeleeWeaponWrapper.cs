using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MeleeWeaponWrapper : MonoBehaviour
{
    public MeleeScriptableObject meleeWeaponSO;
    public GameObject[] collidersForDetectingGrab;
    public Collider[] stabbingColliders;
    [HideInInspector]
    [Tooltip("If this is enabled, you can add multiple stab lines along with their respective stabbing colliders.")]
    public bool multipleStabLines = false;
    [HideInInspector]
    [Tooltip("The stab collider for the stab line on the same index.")]
    public Collider[] stabColliders;
    [HideInInspector]
    [Tooltip("The stab line objects for extra stab lines.")]
    public GameObject[] stabLines;
    public bool hasEvents = false;
    public bool multipleGrabPoints = false;
    [HideInInspector]
    [Tooltip("The GrabPoints objects for multiple grab points.")]
    public GameObject[] grabPointObjects;
    [HideInInspector]
    [Tooltip("The GrabTopAndBottom objects for multiple grab points.")]
    public GameObject[] grabTopAndBottomObjects;
    
    [HideInInspector]
    [Tooltip("The events invoked when the secondary button is pressed.")]
    public UnityEvent onSecondaryClicked = new UnityEvent();
    [HideInInspector]
    [Tooltip("The events invoked when the secondary button is released.")]
    public UnityEvent onSecondaryReleased = new UnityEvent();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        /*if (multipleGrabPoints)
        {
            if (grabTopAndBottomObjects.Contains(Selection.activeGameObject))
            {
                GUIStyle style = new();
                style.fontSize = 18;
                style.normal.textColor = Color.white;
                Transform activeTransform = Selection.activeGameObject.transform;
                Ray forward = new Ray(activeTransform.position, activeTransform.forward);
                Ray right = new Ray(activeTransform.position, activeTransform.right);

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(activeTransform.position, activeTransform.forward * 0.5f);
                Handles.Label(activeTransform.position + activeTransform.forward * 0.5f, new GUIContent("Knuckles point to"), style);

                Gizmos.color = Color.red;
                Handles.Label(activeTransform.position + activeTransform.right * 0.5f, new GUIContent("Thumb points to"), style);
                Gizmos.DrawRay(activeTransform.position, activeTransform.right * 0.5f);
            }
        }*/
        if (Selection.activeGameObject != null)
        {
            if (Selection.activeGameObject.name == "GrabPoint")
            {
                GUIStyle style = new();
                style.fontSize = 18;
                style.normal.textColor = Color.white;
                Transform activeTransform = Selection.activeGameObject.transform;
                Ray forward = new Ray(activeTransform.position, activeTransform.forward);
                Ray right = new Ray(activeTransform.position, activeTransform.right);

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(activeTransform.position, activeTransform.forward * 0.5f);
                Handles.Label(activeTransform.position + activeTransform.forward * 0.5f, new GUIContent("Knuckles point to"), style);

                Gizmos.color = Color.red;
                Handles.Label(activeTransform.position + activeTransform.right * 0.5f, new GUIContent("Thumb points to"), style);
                Gizmos.DrawRay(activeTransform.position, activeTransform.right * 0.5f);
            }
        }
    }
#endif
}
