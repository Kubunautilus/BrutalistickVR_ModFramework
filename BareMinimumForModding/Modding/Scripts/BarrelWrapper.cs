using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelWrapper : MonoBehaviour
{
    [Header("Required Before Using Break-Action Helper")]
    public GameObject bulletWrapperPrefab;
    [Header("Set By Break-Action Helper")]
    public Vector3 chamberedBulletPosition;
    public Vector3 chamberedBulletRotation;
    [Header("Should Be Set Manually")]
    public Transform[] barrelPositions;
    public Vector3 ammoEjectDirection;
    public float ejectVelocity = 15f, ejectRequiredRotation = 25f;
    public Collider ammoLoadingTrigger;
    [Tooltip("If these aren't supplied, default audio will be used.")]
    public AudioClip barrelUnlockedAudio, barrelLockedAudio, roundLoadedAudio, roundsReleasedAudio;

}
