using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RevolverHelper : EditorWindow
{
    [SerializeField]
    private ModFirearmWrapper firearmWrapper;
    Vector2 scrollPos;

    private bool editingMagPos;
    private GameObject magObj;

    private bool editingHammerPos;
    private bool editingCylinder, slideNotFound;
    private CylinderWrapper cylinderWrapper;
    private GUIStyle warningStyle;
    private int chamberCount = 6;
    private float chamberDistanceFromCenter= 0.025f;
    private float angleOffsetOfBullets;
    private float openRotationAngle = 90f, closedRotationAngle = 0f;
    private Vector3 bulletOffsetInChamber;
    private Vector3 bulletRotationInChamber;
    private Transform chambersPosParent;
    private Transform[] chamberPositions;
    private GameObject[] bulletPreviewObjects;
    [MenuItem("Modding Tools/Revolver Helper")]
    private static void Init()
    {
        var windowType = typeof(SceneView);
        var window = GetWindow<RevolverHelper>(windowType);
    }
    private void OnGUI()
    {
        var thisSerialized = new SerializedObject(this);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        SerializedProperty firearmWrapperProperty = thisSerialized.FindProperty("firearmWrapper");
        EditorGUILayout.PropertyField(firearmWrapperProperty, true);
        if (firearmWrapper)
        {
            if (!editingCylinder && firearmWrapper.cylinderWrapper)
            {
                if (GUILayout.Button("Edit Cylinder Options"))
                {
                    StartEditingCylinder();
                }
            }
            else if (editingCylinder)
            {
                if (GUILayout.Button("Save Cylinder Options"))
                {
                    StopEditingCylinder();
                }
            }
            if (editingCylinder && cylinderWrapper != null)
            {
                GUILayout.Label("Editing Cylinder Options...");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Go To Open Cylinder Rotation"))
                {
                    cylinderWrapper.cylinderHolderParent.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, openRotationAngle));
                }
                if (GUILayout.Button("Go To Closed Cylinder Rotation"))
                {
                    cylinderWrapper.cylinderHolderParent.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, closedRotationAngle));
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Save Open Cylinder Rotation"))
                {
                    cylinderWrapper.openRotationAngle = openRotationAngle;
                }
                if (GUILayout.Button("Save Closed Cylinder Rotation"))
                {
                    cylinderWrapper.closedRotationAngle = closedRotationAngle;
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("Edit Cylinder Center"))
                {
                    if (cylinderWrapper.cylinderCenter == null)
                    {
                        GameObject centerObj = new GameObject();
                        centerObj.name = "CenterOfCylinder";
                        centerObj.transform.parent = cylinderWrapper.cylinderObject;
                        centerObj.transform.localPosition = Vector3.zero;
                        cylinderWrapper.cylinderCenter = centerObj.transform;
                    }
                    Selection.activeGameObject = cylinderWrapper.cylinderCenter.gameObject;
                }
                //SerializedProperty magProperty = thisSerialized.FindProperty("magazineObject");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Chamber Count");
                chamberCount = EditorGUILayout.IntSlider(chamberCount, 1, 36);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Chamber Distance From Cylinder Center");
                chamberDistanceFromCenter = EditorGUILayout.FloatField(chamberDistanceFromCenter);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                bulletOffsetInChamber = EditorGUILayout.Vector3Field("Bullet Offset In Chamber", bulletOffsetInChamber);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                bulletRotationInChamber = EditorGUILayout.Vector3Field("Bullet Rotation In Chamber", bulletRotationInChamber);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                angleOffsetOfBullets = EditorGUILayout.FloatField("Angle Offset For Chambers", angleOffsetOfBullets);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Open Cylinder Rotation");
                openRotationAngle = EditorGUILayout.FloatField(openRotationAngle);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Closed Cylinder Rotation");
                closedRotationAngle = EditorGUILayout.FloatField(closedRotationAngle);
                EditorGUILayout.EndHorizontal();
            }
            if (!firearmWrapper.hammerObject.hammerObject)
            {
                GUILayout.Label("Assign the Hammer Object to Edit Position");
            }
            else if (!editingHammerPos)
            {
                if (GUILayout.Button("Edit Hammer Position"))
                {
                    StartEditingHammerPos();
                }
            }
            else
            {
                GUILayout.Label("Editing Hammer...");
                if (GUILayout.Button("Stop Editing Hammer Position"))
                {
                    StopEditingHammerPos();
                }
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Go To Start Rotation"))
                {
                    firearmWrapper.hammerObject.hammerObject.transform.localRotation = Quaternion.Euler(firearmWrapper.hammerObject.startRotation);
                }
                if (GUILayout.Button("Go To End Rotation"))
                {
                    firearmWrapper.hammerObject.hammerObject.transform.localRotation = Quaternion.Euler(firearmWrapper.hammerObject.endRotation);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("Save Start Rotation"))
                {
                    firearmWrapper.hammerObject.startRotation = firearmWrapper.hammerObject.hammerObject.transform.localRotation.eulerAngles;
                }
                if (GUILayout.Button("Save End Rotation"))
                {
                    firearmWrapper.hammerObject.endRotation = firearmWrapper.hammerObject.hammerObject.transform.localRotation.eulerAngles;
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
        }
        thisSerialized.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView();
    }
    private void StartEditingHammerPos()
    {
        editingHammerPos = true;
        Selection.activeGameObject = firearmWrapper.hammerObject.hammerObject;
    }
    private void StopEditingHammerPos()
    {
        editingHammerPos = false;
    }
    private bool StartEditingCylinder()
    {
        if (warningStyle == null)
        {
            warningStyle = new GUIStyle(EditorStyles.label);
            warningStyle.fontSize = 18;
            warningStyle.normal.textColor = Color.yellow;
            warningStyle.hover.textColor = new Color(1f, 0.5f, 0f);
        }
        cylinderWrapper = firearmWrapper.cylinderWrapper;
        if (cylinderWrapper != null)
        {
            if (cylinderWrapper.chamberPositions != null)
            {
                if (cylinderWrapper.chamberPositions.Length > 0)
                {
                    chamberCount = cylinderWrapper.chamberPositions.Length;
                    chamberDistanceFromCenter = Vector3.Distance(cylinderWrapper.chamberPositions[0].transform.position, cylinderWrapper.cylinderCenter.transform.position);
                    bulletOffsetInChamber = cylinderWrapper.chamberedBulletPosition;
                    bulletRotationInChamber = cylinderWrapper.chamberedBulletRotation;
                    angleOffsetOfBullets = GetOffsetAngle(chamberDistanceFromCenter, cylinderWrapper.chamberPositions[0].localPosition);
                    chambersPosParent = cylinderWrapper.cylinderObject.transform.Find("Chambers");
                    if (chambersPosParent == null)
                    {
                        GameObject chamberParentObject = new GameObject("Chambers");
                        chamberParentObject.transform.parent = cylinderWrapper.cylinderObject;
                        chamberParentObject.transform.localPosition = cylinderWrapper.cylinderCenter.localPosition;
                        chamberParentObject.transform.localRotation = Quaternion.identity;
                        chamberParentObject.transform.localScale = Vector3.one;
                        chambersPosParent = chamberParentObject.transform;
                    }
                    chamberPositions = cylinderWrapper.chamberPositions;
                    bulletPreviewObjects = new GameObject[chamberCount];
                    BulletWrapper bulletWrapper = cylinderWrapper.bulletWrapperPrefab.GetComponent<BulletWrapper>();
                    for (int i = 0; i < chamberPositions.Length; i++)
                    {
                        bulletPreviewObjects[i] = Instantiate(bulletWrapper.roundPrefab);
                        bulletPreviewObjects[i].transform.parent = chamberPositions[i].transform;
                    }
                }
            }
        }
        editingCylinder = true;
        if (cylinderWrapper != null)
        {
            return false;
        }
        return true;
    }
    private void Update()
    {
        if (cylinderWrapper != null)
        {
            if (chamberPositions == null)
            {
                if (cylinderWrapper.cylinderCenter != null && cylinderWrapper.cylinderObject != null) 
                {
                    chambersPosParent = cylinderWrapper.cylinderObject.transform.Find("Chambers");
                    if (chambersPosParent == null)
                    {
                        GameObject chamberParentObject = new GameObject("Chambers");
                        chamberParentObject.transform.parent = cylinderWrapper.cylinderObject;
                        chamberParentObject.transform.localPosition = cylinderWrapper.cylinderCenter.localPosition;
                        chamberParentObject.transform.localRotation = Quaternion.identity;
                        chamberParentObject.transform.localScale = Vector3.one;
                        chambersPosParent = chamberParentObject.transform;
                    }
                }
                chamberPositions = new Transform[chamberCount];
                bulletPreviewObjects = new GameObject[chamberCount];
            }
            if (chambersPosParent != null)
            {
                if (chamberPositions.Length != chamberCount || chamberPositions[0] == null)
                {
                    for (int i = 0; i < chamberPositions.Length; i++)
                    {
                        if (chamberPositions[i] != null)
                        {
                            DestroyImmediate(chamberPositions[i].gameObject);
                            DestroyImmediate(bulletPreviewObjects[i].gameObject);
                        }
                    }
                    chamberPositions = new Transform[chamberCount];
                    bulletPreviewObjects = new GameObject[chamberCount];
                    BulletWrapper bulletWrapper = cylinderWrapper.bulletWrapperPrefab.GetComponent<BulletWrapper>();
                    for (int i = 0; i < chamberPositions.Length; i++)
                    {
                        chamberPositions[i] = new GameObject($"Chamber ({i})").transform;
                        chamberPositions[i].transform.parent = chambersPosParent.transform;
                        bulletPreviewObjects[i] = Instantiate(bulletWrapper.roundPrefab);
                        bulletPreviewObjects[i].transform.parent = chamberPositions[i].transform;
                    }
                }
                float angleOffset = -360f / chamberCount;
                for (int i = 0; i < chamberCount; i++)
                {
                    chamberPositions[i].localPosition = GetPointOnCircle(chamberDistanceFromCenter, angleOffset * i + angleOffsetOfBullets);
                    bulletPreviewObjects[i].transform.localPosition = bulletOffsetInChamber;
                    bulletPreviewObjects[i].transform.localRotation = Quaternion.Euler(bulletRotationInChamber);
                }
            }
        }
    }
    private void StopEditingCylinder()
    {
        if (cylinderWrapper != null) 
        {
            cylinderWrapper.chamberedBulletPosition = bulletOffsetInChamber;
            cylinderWrapper.chamberedBulletRotation = bulletRotationInChamber;
            cylinderWrapper.chamberPositions = new Transform[chamberCount];
            for (int i = 0; i < chamberCount; i++) 
            {
                cylinderWrapper.chamberPositions[i] = chamberPositions[i];
                DestroyImmediate(bulletPreviewObjects[i]);
            }
            chamberPositions = null;
            bulletPreviewObjects = null;
        }
        editingCylinder = false;
        cylinderWrapper = null;
    }
    private Vector3 GetPointOnCircle(float radius, float angleInDegrees)
    {
        float angleRad = (angleInDegrees - 90f) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad)) * radius;
        return offset;
    }
    private float GetOffsetAngle(float radius, Vector3 offset)
    {
        float angleRad = Mathf.Atan2(offset.z, offset.x);
        float angleDeg = Mathf.Round((angleRad * Mathf.Rad2Deg + 90f) * 1000f) / 1000f;
        //if (angleDeg < 0) angleDeg += 360f; 
        return angleDeg;
    }
}
