using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HandPosingManager : MonoBehaviour
{
    public HandInfo leftHand, rightHand;

    [System.Serializable]
    public struct HandInfo
    {
        public Transform handTransform;
        public Transform[] thumb, index, middle, ring, pinky;
    }

    public void SetHandPose(HandPoseScriptableObject.HandPoseInfo poseInfo, bool isLeftHand)
    {
        if (isLeftHand)
        {
            leftHand.handTransform.localPosition = poseInfo.Position;
            leftHand.handTransform.localRotation = poseInfo.Rotation;
            /// Left Hand
            // Position
            leftHand.thumb[0].localPosition = poseInfo.thumb[0].position;
            leftHand.thumb[1].localPosition = poseInfo.thumb[1].position;
            leftHand.thumb[2].localPosition = poseInfo.thumb[2].position;

            leftHand.index[0].localPosition = poseInfo.index[0].position;
            leftHand.index[1].localPosition = poseInfo.index[1].position;
            leftHand.index[2].localPosition = poseInfo.index[2].position;

            leftHand.middle[0].localPosition = poseInfo.middle[0].position;
            leftHand.middle[1].localPosition = poseInfo.middle[1].position;
            leftHand.middle[2].localPosition = poseInfo.middle[2].position;

            leftHand.ring[0].localPosition = poseInfo.ring[0].position;
            leftHand.ring[1].localPosition = poseInfo.ring[1].position;
            leftHand.ring[2].localPosition = poseInfo.ring[2].position;

            leftHand.pinky[0].localPosition = poseInfo.pinky[0].position;
            leftHand.pinky[1].localPosition = poseInfo.pinky[1].position;
            leftHand.pinky[2].localPosition = poseInfo.pinky[2].position;

            // Rotation
            leftHand.thumb[0].localRotation = poseInfo.thumb[0].rotation;
            leftHand.thumb[1].localRotation = poseInfo.thumb[1].rotation;
            leftHand.thumb[2].localRotation = poseInfo.thumb[2].rotation;

            leftHand.index[0].localRotation = poseInfo.index[0].rotation;
            leftHand.index[1].localRotation = poseInfo.index[1].rotation;
            leftHand.index[2].localRotation = poseInfo.index[2].rotation;

            leftHand.middle[0].localRotation = poseInfo.middle[0].rotation;
            leftHand.middle[1].localRotation = poseInfo.middle[1].rotation;
            leftHand.middle[2].localRotation = poseInfo.middle[2].rotation;

            leftHand.ring[0].localRotation = poseInfo.ring[0].rotation;
            leftHand.ring[1].localRotation = poseInfo.ring[1].rotation;
            leftHand.ring[2].localRotation = poseInfo.ring[2].rotation;

            leftHand.pinky[0].localRotation = poseInfo.pinky[0].rotation;
            leftHand.pinky[1].localRotation = poseInfo.pinky[1].rotation;
            leftHand.pinky[2].localRotation = poseInfo.pinky[2].rotation;

        }
        else
        {
            rightHand.handTransform.localPosition = poseInfo.Position;
            rightHand.handTransform.localRotation = poseInfo.Rotation;
            /// Right Hand
            // Position
            rightHand.thumb[0].localPosition = poseInfo.thumb[0].position;
            rightHand.thumb[1].localPosition = poseInfo.thumb[1].position;
            rightHand.thumb[2].localPosition = poseInfo.thumb[2].position;

            rightHand.index[0].localPosition = poseInfo.index[0].position;
            rightHand.index[1].localPosition = poseInfo.index[1].position;
            rightHand.index[2].localPosition = poseInfo.index[2].position;

            rightHand.middle[0].localPosition = poseInfo.middle[0].position;
            rightHand.middle[1].localPosition = poseInfo.middle[1].position;
            rightHand.middle[2].localPosition = poseInfo.middle[2].position;

            rightHand.ring[0].localPosition = poseInfo.ring[0].position;
            rightHand.ring[1].localPosition = poseInfo.ring[1].position;
            rightHand.ring[2].localPosition = poseInfo.ring[2].position;

            rightHand.pinky[0].localPosition = poseInfo.pinky[0].position;
            rightHand.pinky[1].localPosition = poseInfo.pinky[1].position;
            rightHand.pinky[2].localPosition = poseInfo.pinky[2].position;

            // Rotation
            rightHand.thumb[0].localRotation = poseInfo.thumb[0].rotation;
            rightHand.thumb[1].localRotation = poseInfo.thumb[1].rotation;
            rightHand.thumb[2].localRotation = poseInfo.thumb[2].rotation;

            rightHand.index[0].localRotation = poseInfo.index[0].rotation;
            rightHand.index[1].localRotation = poseInfo.index[1].rotation;
            rightHand.index[2].localRotation = poseInfo.index[2].rotation;

            rightHand.middle[0].localRotation = poseInfo.middle[0].rotation;
            rightHand.middle[1].localRotation = poseInfo.middle[1].rotation;
            rightHand.middle[2].localRotation = poseInfo.middle[2].rotation;

            rightHand.ring[0].localRotation = poseInfo.ring[0].rotation;
            rightHand.ring[1].localRotation = poseInfo.ring[1].rotation;
            rightHand.ring[2].localRotation = poseInfo.ring[2].rotation;

            rightHand.pinky[0].localRotation = poseInfo.pinky[0].rotation;
            rightHand.pinky[1].localRotation = poseInfo.pinky[1].rotation;
            rightHand.pinky[2].localRotation = poseInfo.pinky[2].rotation;
        }
    }
    public HandPoseScriptableObject.HandPoseInfo GetHandPoseInfo(bool isLeftHand)
    {
        HandPoseScriptableObject.HandPoseInfo poseInfo = new HandPoseScriptableObject.HandPoseInfo();
        poseInfo.thumb = new HandPoseScriptableObject.FingerPoseInfo[3];
        poseInfo.index = new HandPoseScriptableObject.FingerPoseInfo[3];
        poseInfo.middle = new HandPoseScriptableObject.FingerPoseInfo[3];
        poseInfo.ring = new HandPoseScriptableObject.FingerPoseInfo[3];
        poseInfo.pinky = new HandPoseScriptableObject.FingerPoseInfo[3];
        if (isLeftHand)
        {
            poseInfo.Position = leftHand.handTransform.localPosition;
            poseInfo.Rotation = leftHand.handTransform.localRotation;

            // Position
            poseInfo.thumb[0].position = leftHand.thumb[0].localPosition;
            poseInfo.thumb[1].position = leftHand.thumb[1].localPosition;
            poseInfo.thumb[2].position = leftHand.thumb[2].localPosition;

            poseInfo.index[0].position = leftHand.index[0].localPosition;
            poseInfo.index[1].position = leftHand.index[1].localPosition;
            poseInfo.index[2].position = leftHand.index[2].localPosition;

            poseInfo.middle[0].position = leftHand.middle[0].localPosition;
            poseInfo.middle[1].position = leftHand.middle[1].localPosition;
            poseInfo.middle[2].position = leftHand.middle[2].localPosition;

            poseInfo.ring[0].position = leftHand.ring[0].localPosition;
            poseInfo.ring[1].position = leftHand.ring[1].localPosition;
            poseInfo.ring[2].position = leftHand.ring[2].localPosition;

            poseInfo.pinky[0].position = leftHand.pinky[0].localPosition;
            poseInfo.pinky[1].position = leftHand.pinky[1].localPosition;
            poseInfo.pinky[2].position = leftHand.pinky[2].localPosition;

            // Rotation
            poseInfo.thumb[0].rotation = leftHand.thumb[0].localRotation;
            poseInfo.thumb[1].rotation = leftHand.thumb[1].localRotation;
            poseInfo.thumb[2].rotation = leftHand.thumb[2].localRotation;

            poseInfo.index[0].rotation = leftHand.index[0].localRotation;
            poseInfo.index[1].rotation = leftHand.index[1].localRotation;
            poseInfo.index[2].rotation = leftHand.index[2].localRotation;

            poseInfo.middle[0].rotation = leftHand.middle[0].localRotation;
            poseInfo.middle[1].rotation = leftHand.middle[1].localRotation;
            poseInfo.middle[2].rotation = leftHand.middle[2].localRotation;

            poseInfo.ring[0].rotation = leftHand.ring[0].localRotation;
            poseInfo.ring[1].rotation = leftHand.ring[1].localRotation;
            poseInfo.ring[2].rotation = leftHand.ring[2].localRotation;

            poseInfo.pinky[0].rotation = leftHand.pinky[0].localRotation;
            poseInfo.pinky[1].rotation = leftHand.pinky[1].localRotation;
            poseInfo.pinky[2].rotation = leftHand.pinky[2].localRotation;
        }
        else
        {
            poseInfo.Position = rightHand.handTransform.localPosition;
            poseInfo.Rotation = rightHand.handTransform.localRotation;

            // Position
            poseInfo.thumb[0].position = rightHand.thumb[0].localPosition;
            poseInfo.thumb[1].position = rightHand.thumb[1].localPosition;
            poseInfo.thumb[2].position = rightHand.thumb[2].localPosition;

            poseInfo.index[0].position = rightHand.index[0].localPosition;
            poseInfo.index[1].position = rightHand.index[1].localPosition;
            poseInfo.index[2].position = rightHand.index[2].localPosition;

            poseInfo.middle[0].position = rightHand.middle[0].localPosition;
            poseInfo.middle[1].position = rightHand.middle[1].localPosition;
            poseInfo.middle[2].position = rightHand.middle[2].localPosition;

            poseInfo.ring[0].position = rightHand.ring[0].localPosition;
            poseInfo.ring[1].position = rightHand.ring[1].localPosition;
            poseInfo.ring[2].position = rightHand.ring[2].localPosition;

            poseInfo.pinky[0].position = rightHand.pinky[0].localPosition;
            poseInfo.pinky[1].position = rightHand.pinky[1].localPosition;
            poseInfo.pinky[2].position = rightHand.pinky[2].localPosition;

            // Rotation
            poseInfo.thumb[0].rotation = rightHand.thumb[0].localRotation;
            poseInfo.thumb[1].rotation = rightHand.thumb[1].localRotation;
            poseInfo.thumb[2].rotation = rightHand.thumb[2].localRotation;

            poseInfo.index[0].rotation = rightHand.index[0].localRotation;
            poseInfo.index[1].rotation = rightHand.index[1].localRotation;
            poseInfo.index[2].rotation = rightHand.index[2].localRotation;

            poseInfo.middle[0].rotation = rightHand.middle[0].localRotation;
            poseInfo.middle[1].rotation = rightHand.middle[1].localRotation;
            poseInfo.middle[2].rotation = rightHand.middle[2].localRotation;

            poseInfo.ring[0].rotation = rightHand.ring[0].localRotation;
            poseInfo.ring[1].rotation = rightHand.ring[1].localRotation;
            poseInfo.ring[2].rotation = rightHand.ring[2].localRotation;

            poseInfo.pinky[0].rotation = rightHand.pinky[0].localRotation;
            poseInfo.pinky[1].rotation = rightHand.pinky[1].localRotation;
            poseInfo.pinky[2].rotation = rightHand.pinky[2].localRotation;
        }
        return poseInfo;
    }
    public void MirrorHandPose(bool leftToRight)
    {
        if (leftToRight)
        {
            HandPoseScriptableObject.HandPoseInfo sourceHandPoseInfo = GetHandPoseInfo(true);
            HandPoseScriptableObject.HandPoseInfo targetHandPoseInfo = GetHandPoseInfo(false);
            List<HandPoseScriptableObject.FingerPoseInfo[]> sourceJoints = new List<HandPoseScriptableObject.FingerPoseInfo[]>
                    {
                        sourceHandPoseInfo.thumb,
                        sourceHandPoseInfo.index,
                        sourceHandPoseInfo.middle,
                        sourceHandPoseInfo.ring,
                        sourceHandPoseInfo.pinky
                    };
            List<HandPoseScriptableObject.FingerPoseInfo[]> targetJoints = new List<HandPoseScriptableObject.FingerPoseInfo[]>
                    {
                        targetHandPoseInfo.thumb,
                        targetHandPoseInfo.index,
                        targetHandPoseInfo.middle,
                        targetHandPoseInfo.ring,
                        targetHandPoseInfo.pinky
                    };

            for (int j = 0; j < sourceJoints.Count; j++)
            {
                for (int i = 0; i < sourceJoints[j].Length; i++)
                {
                    Vector3 targetRot = sourceJoints[j][i].rotation.eulerAngles;
                    targetRot.y = 360 - targetRot.y;
                    targetRot.z = 360 - targetRot.z;
                    targetJoints[j][i].rotation = Quaternion.Euler(targetRot);
                }
            }
            Vector3 targetHandPos = sourceHandPoseInfo.Position;
            targetHandPos.y *= -1;
            Vector3 mirrorDirection = Vector3.up;
            Vector3 forward = transform.parent.InverseTransformDirection(leftHand.handTransform.forward);
            Vector3 up = transform.parent.InverseTransformDirection(leftHand.handTransform.up);
            var mirror = Vector3.Reflect(forward, mirrorDirection);
            var upMirror = Vector3.Reflect(up, mirrorDirection);
            Quaternion rotation = Quaternion.LookRotation(mirror, upMirror);
            targetHandPoseInfo.Rotation = rotation;
            targetHandPoseInfo.Position = targetHandPos;
            SetHandPose(targetHandPoseInfo, false);
        }
        else
        {
            HandPoseScriptableObject.HandPoseInfo sourceHandPoseInfo = GetHandPoseInfo(false);
            HandPoseScriptableObject.HandPoseInfo targetHandPoseInfo = GetHandPoseInfo(true);
            List<HandPoseScriptableObject.FingerPoseInfo[]> sourceJoints = new List<HandPoseScriptableObject.FingerPoseInfo[]>
                    {
                        sourceHandPoseInfo.thumb,
                        sourceHandPoseInfo.index,
                        sourceHandPoseInfo.middle,
                        sourceHandPoseInfo.ring,
                        sourceHandPoseInfo.pinky
                    };
            List<HandPoseScriptableObject.FingerPoseInfo[]> targetJoints = new List<HandPoseScriptableObject.FingerPoseInfo[]>
                    {
                        targetHandPoseInfo.thumb,
                        targetHandPoseInfo.index,
                        targetHandPoseInfo.middle,
                        targetHandPoseInfo.ring,
                        targetHandPoseInfo.pinky
                    };

            for (int j = 0; j < sourceJoints.Count; j++)
            {
                for (int i = 0; i < sourceJoints[j].Length; i++)
                {
                    Vector3 targetRot = sourceJoints[j][i].rotation.eulerAngles;
                    targetRot.y = 360 - targetRot.y;
                    targetRot.z = 360 - targetRot.z;
                    targetJoints[j][i].rotation = Quaternion.Euler(targetRot);
                }
            }
            Vector3 targetHandPos = sourceHandPoseInfo.Position;
            targetHandPos.y *= -1;
            Vector3 mirrorDirection = Vector3.up;
            Vector3 forward = transform.parent.InverseTransformDirection(rightHand.handTransform.forward);
            Vector3 up = transform.parent.InverseTransformDirection(rightHand.handTransform.up);
            var mirror = Vector3.Reflect(forward, mirrorDirection);
            var upMirror = Vector3.Reflect(up, mirrorDirection);
            Quaternion rotation = Quaternion.LookRotation(mirror, upMirror);
            targetHandPoseInfo.Rotation = rotation;
            targetHandPoseInfo.Position = targetHandPos;
            SetHandPose(targetHandPoseInfo, true);
        }
    }
}
