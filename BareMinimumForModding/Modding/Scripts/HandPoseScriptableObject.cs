using UnityEngine;

public class HandPoseScriptableObject : ScriptableObject
{

    public HandPoseInfo leftHand, rightHand;

    [System.Serializable]
    public struct HandPoseInfo
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public FingerPoseInfo[] thumb, index, middle, ring, pinky;
    }
    [System.Serializable]
    public struct FingerPoseInfo
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}
