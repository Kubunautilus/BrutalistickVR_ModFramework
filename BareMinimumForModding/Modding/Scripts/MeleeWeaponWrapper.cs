using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponWrapper : MonoBehaviour
{
    public MeleeScriptableObject meleeWeaponSO;
    public GameObject[] collidersForDetectingGrab;
    public Collider[] stabbingColliders;
}
