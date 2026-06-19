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
    [Header("[DEPRECATED] Only exist for compatibility with older mods.")]
    [Tooltip("If this is enabled, you can add multiple stab lines along with their respective stabbing colliders.")]
    public bool multipleStabLines = false;
    [HideInInspector]
    [Tooltip("The stab collider for the stab line on the same index.")]
    public Collider[] stabColliders;
    [HideInInspector]
    [Tooltip("The stab line objects for extra stab lines.")]
    public GameObject[] stabLines;
    
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

    [Header("Post Update #10 Sharpness Settings")]
    [Tooltip("If you're making a mod post Update #10, make sure to switch this on. It's only toggled off by default for compatibility reasons with older mods.")]
    public bool useNewSharpnessSettings = false;
    [Tooltip("The blade collider objects to disable collision with when stabbing or slashing.")]
    public GameObject[] bladeColliders;
    [Tooltip("The thickness of the blade. This is used in friction calculations and when slicing objects/body parts in half.")]
    public float bladeThickness;
    //[HideInInspector]
    [Tooltip("These are required for stabbing, slashing, and slicing.\nBottom should be set at the base of the blade.\nTop should be at the tip of the blade.\nForward should be in front of the blade and have a 90 degree angle between the line from the base to the top.")]
    public SlashLineInfo[] slashLines;
    [Tooltip("Whether both sides of the blade can slash or only the direction the Slash Forward points to.")]
    public bool isSlashDualSided;
    [Tooltip("The angle the blade can be twisted in when slashing (0 to X: 0 requires no extra force to slash, X to Y: requires the most extra force to slash, Y to infinity: can't be slashed).")]
    public Vector2 slashForwardAngleRange = new Vector2(10f, 50f);
    [Tooltip("The angle the blade can be rotated in when slashing (0 to X: 0 requires no extra force to slash, X to Y: requires the most extra force to slash, Y to infinity: can't be slashed).")]
    public Vector2 slashUpAngleRange = new Vector2(25f, 40f);
    [Tooltip("The angle between the blade and the stabbable surface (0 to X: 0 requires no extra force to stab, X to Y: requires the most extra force to stab, Y to infinity: can't be stabbed).")]
    public Vector2 stabAngleRange = new Vector2(20f, 50f);

    [Tooltip("Custom grab points for the weapon. If these are not set, the legacy default grab point will be used.")]
    public MeleeGrabpoint[] customGrabPoints;

    [Header("Extra Options")]
    public bool hasEvents = false;
    public bool multipleGrabPoints = false;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
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
            else if (Selection.activeGameObject.name == "SlashPoints")
            {
                Transform origin = Selection.activeGameObject.transform.Find("Origin");
                Transform primary = Selection.activeGameObject.transform.Find("PrimaryAxis");
                Transform secondary = Selection.activeGameObject.transform.Find("SecondaryAxis");
                if (origin == null || primary == null || secondary == null) return;
                GUIStyle style = new();
                style.fontSize = 18;
                style.normal.textColor = Color.white;
                Ray forward = new Ray(origin.position, primary.position - origin.position);
                Ray right = new Ray(origin.position, secondary.position - origin.position);

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(origin.position, primary.position - origin.position);
                Handles.Label(origin.position + (primary.position - origin.position) * 0.5f, new GUIContent("Slash Main Axis (along the blade's edge)"), style);

                Gizmos.color = Color.red;
                Handles.Label(origin.position + (secondary.position - origin.position) * 0.5f, new GUIContent("Slash Secondary Axis (perpendicular to blade's edge)"), style);
                Gizmos.DrawRay(origin.position, secondary.position - origin.position);
            }
            else if (Selection.activeGameObject.transform.parent != null)
            {
                if (Selection.activeGameObject.transform.parent.name == "SlashPoints")
                {
                    Transform origin = Selection.activeGameObject.transform.parent.Find("Origin");
                    Transform primary = Selection.activeGameObject.transform.parent.Find("PrimaryAxis");
                    Transform secondary = Selection.activeGameObject.transform.parent.Find("SecondaryAxis");
                    if (origin == null || primary == null || secondary == null) return;
                    GUIStyle style = new();
                    style.fontSize = 18;
                    style.normal.textColor = Color.white;
                    Ray forward = new Ray(origin.position, primary.position - origin.position);
                    Ray right = new Ray(origin.position, secondary.position - origin.position);

                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(origin.position, primary.position - origin.position);
                    Handles.Label(origin.position + (primary.position - origin.position) * 0.5f, new GUIContent("Slash Main Axis (along the blade's edge)"), style);

                    Gizmos.color = Color.red;
                    Handles.Label(origin.position + (secondary.position - origin.position) * 0.5f, new GUIContent("Slash Secondary Axis (perpendicular to blade's edge)"), style);
                    Gizmos.DrawRay(origin.position, secondary.position - origin.position);
                }
            }
        }
    }
#endif
    [System.Serializable]
    public class SlashLineInfo
    {
        public Transform slashBottom;
        public Transform slashTop;
        public Transform slashForward;
        public Collider[] allowedColliders;
    }
    [System.Serializable]
    public struct MeleeGrabpoint
    {
        public Transform grabPoint;
        public Collider[] grabDetectionColliders;
        public HandPoseScriptableObject handPose;
        public Transform grabTop;
        public Transform grabBottom;
    }
}

