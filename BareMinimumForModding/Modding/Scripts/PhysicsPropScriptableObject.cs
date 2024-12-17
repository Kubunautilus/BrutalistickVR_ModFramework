using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsPropScriptableObject", menuName = "ScriptableObjects/PhysicsPropScriptableObject", order = 3)]
public class PhysicsPropScriptableObject : ScriptableObject
{
    public uint yourSteamId;
    [Tooltip("The custom audio clips for impact/collision sounds. If left empty, default values for the given material will be used.")]
    public AudioClip[] collisionAudioClips;
    [Tooltip("The base material type the prop has. This will be used for the default sounds and bullet impact particles.")]
    public MaterialType materialType;
    [Tooltip("If you want to use a custom particle system for bullet impacts, you should set this. Material Type should be set to custom to use this.")]
    public GameObject particleSystem;

    public enum MaterialType
    {
        Wood,
        Concrete,
        Metal,
        Glass,
        Dirt,
        Custom
    }
}
