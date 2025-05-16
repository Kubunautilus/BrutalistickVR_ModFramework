using UnityEngine;

public class MagazineWrapper : MonoBehaviour
{
    public GameObject bulletWrapperPrefab;
    public int magazineCapacity;
    public int maxRoundsToRender = 50;
    public float offsetPerRound;
    public Transform firstRoundPos;
    public Vector3 roundDirection, progressiveDirectionChange;
    public Vector3 alternatingOffset;
    public Vector3 progressiveRotation;
    public Vector3 firstBulletRotation;
    public bool useAutomaticPosing = false;
    public ModFirearmWrapper.StabilizerGrabpoint grabPoint;
}
