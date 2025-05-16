using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/BulletScriptableObject", order = 6)]
public class BulletScriptableObject : ScriptableObject
{
    public string bulletCaliber;
    public float bulletHitForce = 20f;
    public float bulletDamage = 25f;
    public float minimumDamage = 10f;
    public int penetrateCount = 2;
    public float exitWoundGoreSize = 0.03f;
    public float exitWoundGoreChance = 100f;
    public bool instaGibLimbs = false;
    public float instaGibLimbsChance = 20f;
    public bool instaGibBody;
    public float instaGibBodyChance = 10f;
    public bool instaFractureProp = false;
    public float instaFracturePropChance = 20f;
    public int fractureChip = 2;
}
