using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class HandPoser : EditorWindow
{
    [SerializeField]
    private GameObject handPosingPrefab;
    private GameObject handPoseObject;
    private HandPosingManager handPosingManager;

    [SerializeField]
    private HandPoseScriptableObject.HandPoseInfo lHandPoseInfo, rHandPoseInfo;
    private HandPoseScriptableObject clonedSO;
    //private HandPoserDisplay handPoserDisplay;
    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private HandPoseScriptableObject handPoseToLoad;
    private static Transform[] currentActivePosableFinger;
    Vector2 scrollPos;

    [MenuItem("Modding Tools/Hand Poser")]
    private static void Init()
    {
        var windowType = typeof(SceneView);
        var window = GetWindow<HandPoser>(windowType);
        SceneView.duringSceneGui -= OnSceneGUICustom;
        SceneView.duringSceneGui += OnSceneGUICustom;
        //window.handPoserDisplay = new HandPoserDisplay();
    }
    private void SetPoseInfoFromSO(HandPoseScriptableObject handPoseSO)
    {
        if (clonedSO != null) 
        {
            DestroyImmediate(clonedSO);
        }
        clonedSO = Instantiate(handPoseSO);
        lHandPoseInfo = clonedSO.leftHand;
        rHandPoseInfo = clonedSO.rightHand;
        if (handPoseObject == null) 
        {
            handPoseObject = Instantiate(handPosingPrefab);
        }
        handPosingManager = handPoseObject.GetComponent<HandPosingManager>();
        if (grabPoint != null) 
        {
            handPoseObject.transform.parent = grabPoint;
            handPoseObject.transform.position = grabPoint.position;
            handPoseObject.transform.localRotation = Quaternion.identity;
        }
        handPosingManager.SetHandPose(lHandPoseInfo, true);
        handPosingManager.SetHandPose(rHandPoseInfo, false);
    }
    public HandPoseScriptableObject GetClonedSO()
    {
        return clonedSO;
    }
    private void OnGUI()
    {
        var thisSerialized = new SerializedObject(this);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.EndHorizontal();

        SerializedProperty grabPointProp = thisSerialized.FindProperty("grabPoint");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(grabPointProp, true);
        EditorGUILayout.EndHorizontal();

        SerializedProperty loadableHandPose = thisSerialized.FindProperty("handPoseToLoad");
        

        SerializedProperty lHandPose = thisSerialized.FindProperty("lHandPoseInfo");
        SerializedProperty rHandPose = thisSerialized.FindProperty("rHandPoseInfo");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(loadableHandPose, true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(lHandPose, true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(rHandPose, true);
        EditorGUILayout.EndHorizontal();

        
        thisSerialized.ApplyModifiedProperties();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load From Hand Pose"))
        {
            if (handPoseToLoad != null) 
            {
                SetPoseInfoFromSO(handPoseToLoad);
            }
        }
        EditorGUILayout.EndHorizontal();
        if (clonedSO != null)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Hand Pose As..."))
            {
                var path = EditorUtility.SaveFilePanelInProject("Save Hand Pose As", "newHandPose", "asset", "Select the folder inside your mod folder you want to save the hand pose to.");
                clonedSO.leftHand = lHandPoseInfo;
                clonedSO.rightHand = rHandPoseInfo;
                SaveHandPoseAs(clonedSO, path);
            }
            EditorGUILayout.EndHorizontal();
        }
        if (handPosingManager != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Left Hand Posing");
            if (GUILayout.Button("Move Left Hand"))
            {
                Selection.activeGameObject = handPosingManager.leftHand.handTransform.gameObject;
            }
            if (GUILayout.Button("Thumb"))
            {
                StartPosingFinger(handPosingManager.leftHand.thumb);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Index"))
            {
                StartPosingFinger(handPosingManager.leftHand.index);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Middle"))
            {
                StartPosingFinger(handPosingManager.leftHand.middle);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Ring"))
            {
                StartPosingFinger(handPosingManager.leftHand.ring);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Pinky"))
            {
                StartPosingFinger(handPosingManager.leftHand.pinky);
                SceneView.RepaintAll();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Right Hand Posing");
            if (GUILayout.Button("Move Right Hand"))
            {
                Selection.activeGameObject = handPosingManager.rightHand.handTransform.gameObject;
            }
            if (GUILayout.Button("Thumb"))
            {
                StartPosingFinger(handPosingManager.rightHand.thumb);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Index"))
            {
                StartPosingFinger(handPosingManager.rightHand.index);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Middle"))
            {
                StartPosingFinger(handPosingManager.rightHand.middle);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Ring"))
            {
                StartPosingFinger(handPosingManager.rightHand.ring);
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Pinky"))
            {
                StartPosingFinger(handPosingManager.rightHand.pinky);
                SceneView.RepaintAll();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Mirror Hand Poses");
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Mirror L Hand Pose to R Hand"))
            {
                handPosingManager.MirrorHandPose(true);
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("R Hand Pose to L Hand"))
            {
                handPosingManager.MirrorHandPose(false);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }
    private void OnValidate()
    {
        if (GetClonedSO() != null)
        {
            handPosingManager.SetHandPose(lHandPoseInfo, true);
            handPosingManager.SetHandPose(rHandPoseInfo, false);
        }
    }
    private void SaveHandPoseAs(HandPoseScriptableObject handPoseSO, string path)
    {
        clonedSO = null;
        DestroyImmediate(handPoseObject);
        AssetDatabase.CreateAsset(handPoseSO, path);
        handPoseToLoad = handPoseSO;
    }
    private void Update()
    {
        if (GetClonedSO() != null)
        {
            GetPoseInfoFromTransforms();
        }
    }
    private static void OnSceneGUICustom(SceneView sceneView)
    {
        if (currentActivePosableFinger != null) 
        {
            foreach (var bone in currentActivePosableFinger)
            {
                DrawRotationHandle(bone);
            }
        }
    }
    private static void DrawRotationHandle(Transform boneTransform)
    {
        if (boneTransform != null)
        {
            var newRotation = Handles.RotationHandle(boneTransform.rotation, boneTransform.position);
            if (newRotation != boneTransform.rotation)
            {
                Undo.RecordObject(boneTransform, "Finger Posed");
                boneTransform.rotation = newRotation;
            }
        }
    }
    private static void StartPosingFinger(Transform[] fingerBones)
    {
        currentActivePosableFinger = fingerBones;
    }
    private void GetPoseInfoFromTransforms()
    {
        if (handPosingManager != null)
        {
            lHandPoseInfo = handPosingManager.GetHandPoseInfo(true);
            rHandPoseInfo = handPosingManager.GetHandPoseInfo(false);
        }
    }
    private void OnDestroy()
    {
        DestroyImmediate(handPoseObject);
    }
}

