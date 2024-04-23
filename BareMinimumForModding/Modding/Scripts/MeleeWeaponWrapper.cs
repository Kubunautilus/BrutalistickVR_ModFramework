using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
