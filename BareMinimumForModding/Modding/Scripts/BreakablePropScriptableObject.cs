using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BreakablePropScriptableObject", menuName = "ScriptableObjects/BreakablePropScriptableObject", order = 2)]
public class BreakablePropScriptableObject : ScriptableObject
{
    public uint yourSteamId;
    [Tooltip("The base material type the prop has. This will be used for the default sounds and bullet impact particles.")]
    public PhysicsPropScriptableObject.MaterialType materialType;
    [Tooltip("Whether the inside of the broken off pieces should use the same material. For example, a painted wooden box should have this set to false.")]
    public bool useSameMaterialInside = true;
    [Tooltip("If Use Same Material Inside is set to false, this is the material that will be used for the interior.")]
    public Material insideMaterial;
    [Tooltip("How many times the prop can break. For example, a value of 2 would mean each resulting piece can be broken again.")]
    public int fractureCount = 1;
    [Tooltip("How many pieces the will prop split into for the first iteration.")]
    public int fracturePiecesCount;
    [Tooltip("How many times the previous split will be done. For example, Pieces Count of 2 and 2 Iterations will result in 2^2=4 pieces")]
    public int fractureIterationsCount;
    [Tooltip("How much force is required to break this object. For example, glass has a value of 20 while a wooden box has a value of around 1000.")]
    public float forceRequiredToBreak;
    [Tooltip("How resilient this prop is to bullets. A value of 1 would mean any shot can break this while a value of 5 would mean 5 shots from 9mm but only one shot from .357 Magnum. Higher calibers do more damage to this.")]
    public int propHealthPoints;
    [Tooltip("If the prop has any joints, should it try to attach broken off nearby pieces to the previous joint anchors.")]
    public bool shouldTransferJoints = false;
    [Tooltip("The custom audio clips for fracture sounds. If left empty, default values for the given material will be used.")]
    public AudioClip[] fractureSounds;
    [Tooltip("The custom audio clips for impact/collision sounds. If left empty, default values for the given material will be used.")]
    public AudioClip[] collisionAudioClips;
    [Tooltip("If you want to use a custom particle system for bullet impacts, you should set this. Material Type should be set to custom to use this.")]
    public GameObject particleSystem;
}
