using UnityEngine;

[CreateAssetMenu(fileName = "FirearmScriptableObject", menuName = "ScriptableObjects/FirearmScriptableObject", order = 5)]
public class FirearmScriptableObject : ScriptableObject
{
    public uint yourSteamId;
    [Tooltip("This is the name that will be displayed on the Weapon Wall.")]
    public string weaponName;
    public FirearmType firearmType;
    public float maxRoundsPerMinute;
    public float slideCyclingTime;
    public float bulletSpeed = 100f;
    public float muzzleFlashScale = 1f;
    public bool useMuzzleSmoke;
    public float muzzleSmokeScale = 1f;
    public int muzzleSmokeIntensity = 6;

    public bool requiresAmmo = true;
    public bool requiresChamberedRound = true;
    public bool automaticallyChamberNextRound = true;
    public bool ejectCasingAfterFiring = true;
    public bool lockBoltBackWhenEmpty = true;

    public float upForce = 10f, backwardsForce = 15f;
    public bool randomSideToSideRecoil = true;
    public float sideToSideForce = 5f;
    public float limitBackwardRecoil = -1f, limitUpRecoil = -1f, limitSideRecoil = -1f;

    [Tooltip("If the weapon doesn't fit in the weapon wall socket properly, for example if it covers text or overlaps other weapons, you should set this to true. Otherwise if the weapon is smaller than the socket's bounds, setting this to true causes it to be displayed as larger than it is when held.")]
    public bool shouldScaleInSocket = false;

    public bool multipleBulletsPerRound = false;
    public int bulletCountPerShot = 1;
    public float bulletSpread = 0.03f;
    public enum FirearmType
    {
        SemiAuto,
        ThreeRoundBurst,
        Automatic,
        PumpAction,
        BoltAction,
        Revolver,
        BreakAction
    }
}
