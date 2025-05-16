using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirearmHelper : EditorWindow
{
    [SerializeField]
    private ModFirearmWrapper firearmWrapper;
    Vector2 scrollPos;

    private bool editingMagPos;
    private GameObject magObj;

    private bool editingTriggerPos;
    private bool editingSlide, slideNotFound;
    private bool editingGunPart, editingGunParts;
    private int editableGunPart = -1;
    private CockingHandleWrapper cockingHandle;
    private  GUIStyle warningStyle;
    [MenuItem("Modding Tools/Firearm Helper")]
    private static void Init()
    {
        var windowType = typeof(SceneView);
        var window = GetWindow<FirearmHelper>(windowType);
    }
    private void OnGUI()
    {
        var thisSerialized = new SerializedObject(this);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        SerializedProperty firearmWrapperProperty = thisSerialized.FindProperty("firearmWrapper");
        EditorGUILayout.PropertyField(firearmWrapperProperty, true);
        if (firearmWrapper)
        {
            if (!editingMagPos && firearmWrapper.magazineWrapperPrefab)
            {
                if (GUILayout.Button("Edit Magazine Position"))
                {
                    StartEditingMagPos();
                }
            }
            else if (editingMagPos)
            {
                if (GUILayout.Button("Save Magazine Position"))
                {
                    StopEditingMagPos();
                }
            }
            if (!firearmWrapper.triggerObject.triggerObject)
            {
                GUILayout.Label("Assign the Trigger Object to Edit Position");
            }
            else if (!editingTriggerPos)
            {
                if (GUILayout.Button("Edit Trigger Position"))
                {
                    StartEditingTriggerPos();
                }
            }
            else
            {
                GUILayout.Label("Editing Trigger...");
                if (GUILayout.Button("Stop Editing Trigger Position"))
                {
                    StopEditingTriggerPos();
                }
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Go To Start Rotation"))
                {
                    firearmWrapper.triggerObject.triggerObject.transform.localRotation = Quaternion.Euler(firearmWrapper.triggerObject.startRotation);
                }
                if (GUILayout.Button("Go To End Rotation"))
                {
                    firearmWrapper.triggerObject.triggerObject.transform.localRotation = Quaternion.Euler(firearmWrapper.triggerObject.endRotation);
                }
                if (GUILayout.Button("Go To Start Position"))
                {
                    firearmWrapper.triggerObject.triggerObject.transform.localPosition = firearmWrapper.triggerObject.startPosition;
                }
                if (GUILayout.Button("Go To End Position"))
                {
                    firearmWrapper.triggerObject.triggerObject.transform.localPosition = firearmWrapper.triggerObject.endPosition;
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Save Start Rotation"))
                {
                    firearmWrapper.triggerObject.startRotation = firearmWrapper.triggerObject.triggerObject.transform.localRotation.eulerAngles;
                }
                if (GUILayout.Button("Save End Rotation"))
                {
                    firearmWrapper.triggerObject.endRotation = firearmWrapper.triggerObject.triggerObject.transform.localRotation.eulerAngles;
                }
                if (GUILayout.Button("Save Start Position"))
                {
                    firearmWrapper.triggerObject.startPosition = firearmWrapper.triggerObject.triggerObject.transform.localPosition;
                }
                if (GUILayout.Button("Save End Position"))
                {
                    firearmWrapper.triggerObject.endPosition = firearmWrapper.triggerObject.triggerObject.transform.localPosition;
                }
                EditorGUILayout.EndVertical();
               
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("Go To Start Position and Rotation"))
                {
                    firearmWrapper.triggerObject.triggerObject.transform.localRotation = Quaternion.Euler(firearmWrapper.triggerObject.startRotation);
                    firearmWrapper.triggerObject.triggerObject.transform.localPosition = firearmWrapper.triggerObject.startPosition;
                }
                if (GUILayout.Button("Go To End Position and Rotation"))
                {
                    firearmWrapper.triggerObject.triggerObject.transform.localRotation = Quaternion.Euler(firearmWrapper.triggerObject.endRotation);
                    firearmWrapper.triggerObject.triggerObject.transform.localPosition = firearmWrapper.triggerObject.endPosition;
                }
            }
            if (slideNotFound)
            {
                GUILayout.Label("Add a Cocking Handle Wrapper first", warningStyle);
            }
            if (GUILayout.Button("Start Editing Slide"))
            {
                slideNotFound = StartEditingSlide();
            }
            if (editingSlide && cockingHandle != null) 
            {
                GUILayout.Label("Editing Cocking Handle...");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Go To Forward Position"))
                {
                    cockingHandle.transform.localPosition = cockingHandle.forwardPosition;
                }
                if (GUILayout.Button("Go To Backward Position"))
                {
                    cockingHandle.transform.localPosition = cockingHandle.backwardPosition;
                }
                if (GUILayout.Button("Go To Eject Position"))
                {
                    cockingHandle.transform.localPosition = cockingHandle.ejectPosition;
                }
                if (GUILayout.Button("Go To Chamber Position"))
                {
                    cockingHandle.transform.localPosition = cockingHandle.chamberPosition;
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Save Forward Position"))
                {
                    cockingHandle.forwardPosition = cockingHandle.transform.localPosition;
                }
                if (GUILayout.Button("Save Backward Position"))
                {
                    cockingHandle.backwardPosition = cockingHandle.transform.localPosition;
                }
                if (GUILayout.Button("Save Eject Position"))
                {
                    cockingHandle.ejectPosition = cockingHandle.transform.localPosition;
                }
                if (GUILayout.Button("Save Chamber Position"))
                {
                    cockingHandle.chamberPosition = cockingHandle.transform.localPosition;
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                if (cockingHandle.handleType == CockingHandleWrapper.CockingHandleType.Bolt)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    Vector3 boltDirection;
                    if (cockingHandle.boltRotationDirection == CockingHandleWrapper.BoltRotationDirection.XAxis)
                    {
                        boltDirection = Vector3.right;
                    }
                    else if (cockingHandle.boltRotationDirection == CockingHandleWrapper.BoltRotationDirection.YAxis) 
                    {
                        boltDirection = Vector3.up;
                    }
                    else
                    {
                        boltDirection = Vector3.forward;
                    }
                    if (GUILayout.Button("Go To Max Bolt Rotation"))
                    {
                        cockingHandle.transform.localRotation = Quaternion.Euler(boltDirection * cockingHandle.maxBoltRotation);
                    }
                    if (GUILayout.Button("Go To Start Bolt Rotation"))
                    {
                        cockingHandle.transform.localRotation = Quaternion.Euler(boltDirection * cockingHandle.startBoltRotation);
                    }
                    if (GUILayout.Button("Go To Snap To Start Rotation"))
                    {
                        cockingHandle.transform.localRotation = Quaternion.Euler(boltDirection * cockingHandle.snapToStartRotation);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    float angleOfRotation;
                    if (cockingHandle.boltRotationDirection == CockingHandleWrapper.BoltRotationDirection.XAxis)
                    {
                        angleOfRotation = cockingHandle.transform.localRotation.eulerAngles.x;
                    }
                    else if (cockingHandle.boltRotationDirection == CockingHandleWrapper.BoltRotationDirection.YAxis)
                    {
                        angleOfRotation = cockingHandle.transform.localRotation.eulerAngles.y;
                    }
                    else if (cockingHandle.boltRotationDirection == CockingHandleWrapper.BoltRotationDirection.ZAxis)
                    {
                        angleOfRotation = cockingHandle.transform.localRotation.eulerAngles.z;
                    }
                    else
                    {
                        angleOfRotation = 0f;
                    }
                    if (GUILayout.Button("Save Max Bolt Rotation"))
                    {
                        cockingHandle.maxBoltRotation = angleOfRotation;
                    }
                    if (GUILayout.Button("Save Start Bolt Rotation"))
                    {
                        cockingHandle.startBoltRotation = angleOfRotation;
                    }
                    if (GUILayout.Button("Save Snap To Start Rotation"))
                    {
                        cockingHandle.snapToStartRotation = angleOfRotation;
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    if (cockingHandle.transform.localRotation.eulerAngles != Vector3.zero && angleOfRotation == 0f)
                    {
                        GUILayout.Label("To avoid bugs, you should have the game object the\nCocking Handle Wrapper is attached to at (0, 0, 0) rotation.", warningStyle);
                    }
                    /*if (angleOfRotation == 0f)
                    {
                        GUILayout.Label("Make sure only one axis is set on\nthe Cocking Handle Wrapper's bolt rotation direction.", warningStyle);
                    }*/
                }
                if (GUILayout.Button("Stop Editing Cocking Handle"))
                {
                    StopEditingSlide();
                }
            }

            if (!editingGunParts)
            {
                if (GUILayout.Button("Start Editing Gun Parts"))
                    slideNotFound = StartEditingGunParts();
            }
            else if (editingGunParts)
            {
                if (GUILayout.Button("Stop Editing Gun Parts"))
                    StopEditingGunParts();
            }
            if (editingGunParts && cockingHandle != null) 
            {
                if (cockingHandle.animatedGunParts.Length > 0) 
                {
                    for (int i = 0; i < cockingHandle.animatedGunParts.Length; i++) 
                    {
                        if (cockingHandle.animatedGunParts[i].animatedPart != null)
                        {
                            int editableIndex = i;
                            if (GUILayout.Button($"Start Editing Gun Part {cockingHandle.animatedGunParts[editableIndex].animatedPart.name}"))
                            {
                                StartEditingGunPart(editableIndex);
                            }
                        }
                    }
                }
                if (editableGunPart != -1)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    if (GUILayout.Button("Go To Forward Position"))
                    {
                        cockingHandle.animatedGunParts[editableGunPart].animatedPart.transform.localPosition = cockingHandle.animatedGunParts[editableGunPart].forwardPosition;
                    }
                    if (GUILayout.Button("Go To Backward Position"))
                    {
                        cockingHandle.animatedGunParts[editableGunPart].animatedPart.transform.localPosition = cockingHandle.animatedGunParts[editableGunPart].backwardPosition;
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    if (GUILayout.Button("Save Forward Position"))
                    {
                        cockingHandle.animatedGunParts[editableGunPart].forwardPosition = cockingHandle.animatedGunParts[editableGunPart].animatedPart.transform.localPosition;
                    }
                    if (GUILayout.Button("Save Backward Position"))
                    {
                        cockingHandle.animatedGunParts[editableGunPart].backwardPosition = cockingHandle.animatedGunParts[editableGunPart].animatedPart.transform.localPosition;
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Save Current Gun Part"))
                {
                    StopEditingGunPart();
                }
            }
        }
        thisSerialized.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView();
    }
    private void StartEditingMagPos()
    {
        editingMagPos = true;
        Transform magazineTransform;
        if (firearmWrapper.magazineSocket != null)
        {
            magazineTransform = firearmWrapper.magazineSocket.transform;
        }
        else if (firearmWrapper.transform.Find("MagazineSocket") != null)
        {
            magazineTransform = firearmWrapper.transform.Find("MagazineSocket");
        }
        else
        {
            GameObject magSocket = new GameObject();
            magSocket.name = "MagazineSocket";
            magSocket.transform.parent = firearmWrapper.transform;
            magSocket.transform.localPosition = Vector3.zero;
            magSocket.transform.localRotation = Quaternion.identity;
            magazineTransform = magSocket.transform;
        }
        magObj = Instantiate(firearmWrapper.magazineWrapperPrefab);
        magObj.transform.parent = magazineTransform;
        magObj.transform.localPosition = Vector3.zero;
        magObj.transform.localRotation = Quaternion.identity;
        Selection.activeGameObject = magazineTransform.gameObject;
    }
    private void StopEditingMagPos()
    {
        editingMagPos = false;
        if (magObj != null)
        {
            DestroyImmediate(magObj);
        }
    }
    private void StartEditingTriggerPos()
    {
        editingTriggerPos = true;
        Selection.activeGameObject = firearmWrapper.triggerObject.triggerObject;
    }
    private void StopEditingTriggerPos()
    {
        editingTriggerPos = false;
    }
    private bool StartEditingSlide()
    {
        if (warningStyle == null)
        {
            warningStyle = new GUIStyle(EditorStyles.label);
            warningStyle.fontSize = 18;
            warningStyle.normal.textColor = Color.yellow;
            warningStyle.hover.textColor = new Color(1f, 0.5f, 0f);
        }
        cockingHandle = firearmWrapper.GetComponentInChildren<CockingHandleWrapper>();
        editingSlide = true;
        if (cockingHandle != null) 
        {
            return false;
        }
        return true;
    }
    private void StopEditingSlide()
    {
        editingSlide = false;
        cockingHandle = null;
    }
    private bool StartEditingGunParts()
    {
        if (warningStyle == null)
        {
            warningStyle = new GUIStyle(EditorStyles.label);
            warningStyle.fontSize = 18;
            warningStyle.normal.textColor = Color.yellow;
            warningStyle.hover.textColor = new Color(1f, 0.5f, 0f);
        }
        cockingHandle = firearmWrapper.GetComponentInChildren<CockingHandleWrapper>();
        editingGunParts = true;
        if (cockingHandle != null)
        {
            return false;
        }
        return true;
    }
    private bool StartEditingGunPart(int gunPartIndex)
    {
        //cockingHandle = firearmWrapper.GetComponentInChildren<CockingHandleWrapper>();
        editingGunPart = true;
        editableGunPart = gunPartIndex;
        if (cockingHandle != null)
        {
            return false;
        }
        return true;
    }
    private void StopEditingGunPart()
    {
        editableGunPart = -1;
        editingGunPart = false;
        //editingGunParts = false;
    }
    private void StopEditingGunParts()
    {
        editableGunPart = -1;
        //editingGunPart = false;
        editingGunParts = false;
        cockingHandle = null;
    }
}
