using System.Collections.Generic;
using UnityEngine;

public class CockingHandleWrapper : MonoBehaviour
{
    public CockingHandleType handleType;
    public AnimatedGunPart[] animatedGunParts;
    public Vector3 forwardPosition, backwardPosition;

    public float slideForwardSpeed = 10f;
    public float springStrength = 0.05f;

    public Vector3 ejectPosition, chamberPosition;
    [HideInInspector]
    public Vector3 lockedRotationPosition, unlockedRotationPosition;
    public ModFirearmWrapper.StabilizerGrabpoint[] grabPoints;
    public List<AudioClip> slideForward;
    public List<AudioClip> slideBackward;
    [Header("Bolt Action Settings")]

    public BoltRotationDirection boltRotationDirection;
    public float maxBoltRotation, unlockBoltRotation, startBoltRotation = 0f, snapToStartRotation;

   
    public enum CockingHandleType
    {
        Reciprocating,
        NonReciprocating,
        Pump,
        Bolt
    }
    public enum BoltRotationDirection
    {
        XAxis,
        YAxis, 
        ZAxis
    }
    [System.Serializable]
    public struct AnimatedGunPart
    {
        public GameObject animatedPart;
        public Vector3 forwardPosition, backwardPosition;
    }
}
