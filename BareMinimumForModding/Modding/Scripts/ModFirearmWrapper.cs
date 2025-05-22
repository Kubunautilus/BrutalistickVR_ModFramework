using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ModFirearmWrapper : MonoBehaviour
{
    public FirearmScriptableObject firearmScriptableObject;
    public GameObject magazineWrapperPrefab;
    public SingleRoundLoading singleRoundLoadedWeapon;
    public MainGrabPoint mainGrabPoint;
    public StabilizerGrabpoint[] stabilizerGrabpoints;

    public TriggerObject triggerObject;

    public GameObject chamberedRound, chamberedCasing;
    public GameObject ejectionPort;
    public Transform verticalRecoilPoint, backwardRecoilPoint;
    

    public Transform bulletOrigin; 
    [Header("Magazine Properties")]
    public GameObject magazineTriggerCollider;
    public GameObject magGrabTriggerCollider, autoReleaseMagTriggerCollider;
    public GameObject magazineSocket;
    [Header("Audio Clips")]
    public AudioClip outOfAmmoAudio;
    public AudioClip magazineLoadAudio, magazineReleaseAudio;
    public List<AudioClip> firingAudio;
    /*[Header("Required for both Revolvers and Break-Actions")]
    public BulletWrapper bulletWrapper;*/
    [Header("Revolver Specific")]
    public HammerObject hammerObject;
    public CylinderWrapper cylinderWrapper;

    [Header("Break-Action Specific")]
    public LatchObject barrelLatchObject;
    public BarrelWrapper barrelWrapper;
    [Header("Multi-Barrel Options")]
    public bool multipleBarrels;
    public MultiBarrelStyle multiBarrelStyle;
    public Transform[] multiBarrelBulletOrigins;
    [Header("Gatling Barrel Options")]
    public GameObject rotatingBarrel;
    [Tooltip("Make sure the rotation speed matches the Rounds Per Minute to avoid desyncing.")]
    public float rotationSpeed;
    public Vector3 rotationAxis;
    [Tooltip("The length of the Spin Up and Spin Down audio clips govern the acceleration of the barrel rotation")]
    public AudioClip spinUpAudio, spinLoopAudio, spinDownAudio;
    [Tooltip("Since we don't want to clog up the audio with multiple overlapping audio clips due to immense rates of fire, you should have two separate firing loops." +
        "One with the last shot (no following shots but still overlapping previous shots) and a perfect loop of only firing sounds. If these aren't supplied (for example, you have a slower barrel), only regular firing sounds will be used.")]
    public AudioClip firingLoopAudioWithEnd, firingPerfectLoopAudio;


    [System.Serializable]
    public struct MainGrabPoint
    {
        public Transform grabPoint;
        public Collider[] grabDetectionColliders;
        public HandPoseScriptableObject handPose, triggerPose;
    }
    [System.Serializable]
    public struct StabilizerGrabpoint
    {
        public Transform grabPoint;
        public Collider[] grabDetectionColliders;
        public HandPoseScriptableObject handPose;
    }
    [System.Serializable]
    public struct SingleRoundLoading
    {
        public GameObject singleRoundPrefab;
        public BulletWrapper bulletWrapper;
        public int maxAmmoCount;
    }
    [System.Serializable]
    public struct TriggerObject
    {
        public GameObject triggerObject;
        public Vector3 startRotation, endRotation;
        public Vector3 startPosition, endPosition;
    }
    [System.Serializable]
    public struct HammerObject
    {
        public GameObject hammerObject;
        public Vector3 startRotation, endRotation;
    }
    [System.Serializable]
    public struct LatchObject
    {
        public GameObject latchObject;
        public float targetRotation;
        public Vector3 rotationAxis;
    }
    public enum MultiBarrelStyle
    {
        Sequential,
        DualStageTrigger,
        GatlingStyle
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (magazineSocket != null)
        {
            if (magazineSocket.transform.childCount > 0) {
                if (Selection.activeGameObject == magazineSocket.transform.GetChild(0).gameObject)
                {
                    GUIStyle style = new();
                    style.fontSize = 18;
                    style.normal.textColor = Color.white;
                    Transform activeTransform = Selection.activeGameObject.transform;
                    Ray forward = new Ray(activeTransform.position, activeTransform.forward);
                    Ray right = new Ray(activeTransform.position, activeTransform.right);

                    Gizmos.color = Color.cyan;
                    Gizmos.DrawRay(activeTransform.position, activeTransform.forward * 0.25f);
                    Handles.Label(activeTransform.position + activeTransform.forward * 0.25f, new GUIContent("Magazine moves along this axis"), style);
                }
            }
        }
    }
#endif
}
