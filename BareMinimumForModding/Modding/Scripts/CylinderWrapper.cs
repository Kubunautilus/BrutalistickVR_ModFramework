using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderWrapper : MonoBehaviour
{
    [Header("Required Before Using Revolver Helper")]
    public GameObject bulletWrapperPrefab;
    public Transform cylinderHolderParent, cylinderObject;
    [Header("Set By Revolver Helper")]
    public Transform cylinderCenter;
    public Transform[] chamberPositions;
    public Vector3 chamberedBulletPosition, chamberedBulletRotation;
    public float closedRotationAngle, openRotationAngle;
    [Header("Should Be Set Manually")]
    public float inertiaToEjectBullets = 0.7f;
    public float popOpenCylinderRotationSpeed = 0.5f;
    public Collider loadingTrigger;
    [Tooltip("If these aren't supplied, default audio will be used.")]
    public AudioClip cylinderUnlockedAudio, cylinderLockedAudio, roundLoadedAudio, roundsReleasedAudio;
    [Header("Free Spinning Cylinder Options")]
    public Collider spinTrigger;
    public AudioClip spinningClickAudio;
    public float minAudioPitch = 0.9f, maxAudioPitch = 1.3f;
    public float rotationForClickSound = 60f;
    public float rotationDrag = 0.2f;
    public float cylinderRadius = 0.025f;
}
