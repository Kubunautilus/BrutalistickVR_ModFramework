using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeScriptableObject", menuName = "ScriptableObjects/MeleeScriptableObject", order = 1)]
public class MeleeScriptableObject : ScriptableObject
{
    public uint yourSteamId;
    [Tooltip("This is the name that will be displayed on the Weapon Wall.")]
    public string weaponName;
    [Tooltip("Try to set this as low as possible without losing fidelity")]
    public TextureResolution bloodTextureResolution = TextureResolution.Texture64x64;
    public MeleeWeaponType meleeWeaponType;
    public Vector3 grabHandAngleOffset = new Vector3(15f, 0f, 0f);
    [Tooltip("If the weapon doesn't fit in the weapon wall socket properly, for example if it covers text or overlaps other weapons, you should set this to true. Otherwise if the weapon is smaller than the socket's bounds, setting this to true causes it to be displayed as larger than it is when held.")]
    public bool shouldScaleInSocket = false;
    [Tooltip("This is the minimum force required for the hit to deal damage and cause bleeding.")]
    public float damageThreshold = 8f;
    [Tooltip("This is the extra force multiplier to give blunt attacks a little extra oomph. Only applies to colliders with the tags Blunt or Handle")]
    public float bluntHitForceMultiplier = 2f;
    [Tooltip("The minimum velocity required to apply the force multiplier.")]
    public float minVelocityForBluntForceMultiplier = 1.5f;
    public HitSoundsType hitSoundsType;
    [HideInInspector]
    public string firstColliderAudioTag = "Sharp", secondColliderAudioTag = "Blunt";
    [HideInInspector]
    public AudioClip[] firstColliderTagAudioClips;
    [HideInInspector]
    public AudioClip[] secondColliderTagAudioClips;
    [HideInInspector]
    [Tooltip("The force required for slicing to take place")]
    public float sliceThreshold = 20f;
    [HideInInspector]
    [Tooltip("The power of the blade. Higher values make it easier for the blade to go through thicker body parts.\n0 - No extra force required for limbs (Katana in base game)\n1 - No extra force required for head (Machete in base game)\n2 - No extra force required for body (Greatsword in base game)")]
    public int slicePower = 0;
    [HideInInspector]
    [Tooltip("The sharpness of the stabber. This affects how much force is required for the blade to enter body parts, how sharp the stabbing angle can be, and how easily the blade slides in and out of the wound.")]
    public StabberSharpness stabberSharpness;
    [HideInInspector]
    [Tooltip("This affects whether both the tip and base of the stabber can be used for stabbing. When this is disabled, only the tip end is sharp.")]
    public bool tipAndBaseCanStab;
    [HideInInspector]
    [Tooltip("If this is enabled, the stabber can go all the way through (like spears). Otherwise the stabber will stop when reaching the base.")]
    public bool canStabberRunThrough;
    /*[HideInInspector]
    public AudioScriptableObject[] firstColliderTagAudioClips;
    [HideInInspector]
    public AudioScriptableObject[] secondColliderTagAudioClips;*/
}
public enum MeleeWeaponType
{
    Sharp,
    Blunt
}
public enum HitSoundsType
{
    SingleMaterial,
    DoubleMaterial
}
public enum StabberSharpness
{
    lowSharpness,
    mediumLowSharpness,
    mediumSharpness,
    highSharpness
}
public enum TextureResolution
{
    //Texture16x16 = 16,
    Texture32x32 = 32,
    Texture64x64 = 64,
    Texture128x128 = 128,
    Texture256x256 = 256,
    Texture512x512 = 512,
    Texture1024x1024 = 1024,
    Texture2048x2048 = 2048,
    Texture4096x4096 = 4096
}

