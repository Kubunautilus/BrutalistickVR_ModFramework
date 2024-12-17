using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeformablePropScriptableObject", menuName = "ScriptableObjects/DeformablePropScriptableObject", order = 4)]
public class DeformablePropScriptableObject : ScriptableObject
{
    public uint yourSteamId;
    [Tooltip("The custom audio clips for impact/collision sounds. If left empty, default values for the given material will be used.")]
    public AudioClip[] collisionAudioClips;
    [Tooltip("The base material type the prop has. This will be used for the default sounds and bullet impact particles.")]
    public PhysicsPropScriptableObject.MaterialType materialType;
    [Tooltip("How easily the object deforms. This is really touchy so you should only change it in small increments, like 0.001.")]
    public float deformMalleability = 0.008f;
    [Tooltip("How large the deformation radius should be in meters. Every deformation is applied to this radius with a falloff.")]
    public float deformRadius = 0.15f;
    [Tooltip("The minimum force/impulse required to deform the object.")]
    public float minimumImpulse = 5f;
    [Tooltip("If you want to use a custom particle system for bullet impacts, you should set this. Material Type should be set to custom to use this.")]
    public GameObject particleSystem;
}
