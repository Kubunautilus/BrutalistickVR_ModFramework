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
    [HideInInspector]
    [Tooltip("The amount of damage caused by slashing. This applies only if the weapon contains slice triggers, the SlashPoints object and colliders with the tag Sharp.")]
    public float slashingDamage = 20f;

    [Header("Post Update #10 Sharpness Settings")]
    [Tooltip("If you're making a mod post Update #10, make sure to switch this on. It should only be toggled off for compatibility reasons with older mods.")]
    public bool useNewSharpnessSettings = false;
    [Tooltip("The baseline used for friction calculations for blades in objects.")]
    public float frictionCoefficient = 0.5f;
    
    [Tooltip("A flat multiplier applied to stab friction. Mostly used for weapons that don't have a stabbing tip (such as axes).")]
    public float flatStabFrictionMultiplier = 1f;
    [Tooltip("The area of the embedded blade to reach the maximum chop friction.")]
    public float areaForMaximumChop = 0.075f;
    [Tooltip("The velocity below which the blade gets lodged in the body along the chop axis.")]
    public float chopLockVelocityThreshold = 0.1f;
    [Tooltip("The force required to dislodge the blade in the body (continuous force applied for some time).")]
    public float chopUnlockForceThreshold = 250f;
    [Tooltip("The force required to instantly dislodge the blade in the body (jerking it out).")]
    public float chopInstantUnlockForceThreshold = 350f;
    //public float chopUnlockTime = 0.15f;

    [Tooltip("How deep the blade has to stab into the body to reach 100% stab friction, to account for a tapering blade tip.")]
    public float stabDepthForMaxStabMultiplier = 0.025f;
    [Tooltip("The area of the embedded blade to reach the maximum stab friction")]
    public float areaForMaximumStab = 0.05f;
    [Tooltip("The velocity below which the blade gets lodged in the body along the stab axis.")]
    public float stabLockVelocityThreshold = 0.25f;
    [Tooltip("The force required to dislodge the blade in the body (continuous force applied for some time).")]
    public float stabUnlockForceThreshold = 100f;
    [Tooltip("The force required to instantly dislodge the blade in the body (jerking it out).")]
    public float stabInstantUnlockForceThreshold = 200f;

    [Tooltip("If the blade doesn't end with a base, such as axes, this will make it easier to pull the blade out when the tip of the blade is outside a body.")]
    public bool useStabOutsideLockForces = false;
    [Tooltip("The force required to dislodge the blade in the body in the direction of the edge that sticks out (continuous force applied for some time).")]
    public float stabUnlockOutsideForceThreshold = 100f;
    [Tooltip("The force required to instantly dislodge the blade in the body in the direction of the edge that sticks out (jerking it out).")]
    public float stabInstantUnlockOutsideForceThreshold = 250f;
    //public float stabUnlockTime = 0.1f;

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

