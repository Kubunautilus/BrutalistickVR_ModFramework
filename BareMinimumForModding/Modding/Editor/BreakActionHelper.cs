using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BreakActionHelper : EditorWindow
{
    [SerializeField]
    private ModFirearmWrapper firearmWrapper;
    Vector2 scrollPos;

    private bool editingMagPos;
    private GameObject magObj;

    private bool editingHammerPos;
    private bool editingBarrels, slideNotFound;
    private BarrelWrapper barrelWrapper;
    private GUIStyle warningStyle;
    private int barrelCount = 2;
    private float latchTargetRotation = -9f;
    private Vector3 latchRotationAxis = Vector3.up;
    private Vector3 bulletOffsetInBarrel;
    private Vector3 bulletRotationInBarrel;
    private Transform chambersPosParent;
    private Transform[] barrelPositions;
    private GameObject[] bulletPreviewObjects;
    [MenuItem("Modding Tools/Break-Action Helper")]
    private static void Init()
    {
        var windowType = typeof(SceneView);
        var window = GetWindow<BreakActionHelper>(windowType);
    }
    private void OnGUI()
    {
        var thisSerialized = new SerializedObject(this);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        SerializedProperty firearmWrapperProperty = thisSerialized.FindProperty("firearmWrapper");
        EditorGUILayout.PropertyField(firearmWrapperProperty, true);
        if (firearmWrapper)
        {
            if (!editingBarrels && firearmWrapper.barrelWrapper)
            {
                if (GUILayout.Button("Edit Barrel Options"))
                {
                    StartEditingBarrels();
                }
            }
            else if (editingBarrels)
            {
                if (GUILayout.Button("Save Barrel Options"))
                {
                    StopEditingBarrels();
                }
            }
            if (editingBarrels && barrelWrapper != null)
            {
                GUILayout.Label("Editing Barrel Options...");
                //SerializedProperty magProperty = thisSerialized.FindProperty("magazineObject");

                EditorGUILayout.BeginHorizontal();
                bulletOffsetInBarrel = EditorGUILayout.Vector3Field("Bullet Offset In Barrel", bulletOffsetInBarrel);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                bulletRotationInBarrel = EditorGUILayout.Vector3Field("Bullet Rotation In Barrel", bulletRotationInBarrel);
                EditorGUILayout.EndHorizontal();

            }
            if (!firearmWrapper.barrelLatchObject.latchObject)
            {
                GUILayout.Label("Assign the Latch Object to Edit Position");
            }
            else if (!editingHammerPos)
            {
                if (GUILayout.Button("Edit Latch Position"))
                {
                    StartEditingLatchPos();
                }
            }
            else
            {
                GUILayout.Label("Editing Latch...");
                if (GUILayout.Button("Stop Editing Latch Position"))
                {
                    StopEditingLatchPos();
                }
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Latch Target Rotation");
                latchTargetRotation = EditorGUILayout.FloatField(latchTargetRotation);
                EditorGUILayout.EndHorizontal();
                latchRotationAxis = EditorGUILayout.Vector3Field("Latch Rotation Axis", latchRotationAxis);
                if (GUILayout.Button("Go To Start Rotation"))
                {
                    firearmWrapper.barrelLatchObject.latchObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                }
                if (GUILayout.Button("Go To Target Rotation"))
                {
                    firearmWrapper.barrelLatchObject.latchObject.transform.localRotation = Quaternion.Euler(latchRotationAxis * latchTargetRotation);
                }
                if (GUILayout.Button("Save Target Rotation"))
                {
                    firearmWrapper.barrelLatchObject.targetRotation = latchTargetRotation;
                    firearmWrapper.barrelLatchObject.rotationAxis = latchRotationAxis;
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
        }
        thisSerialized.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView();
    }
    private void StartEditingLatchPos()
    {
        editingHammerPos = true;
        Selection.activeGameObject = firearmWrapper.hammerObject.hammerObject;
    }
    private void StopEditingLatchPos()
    {
        editingHammerPos = false;
    }
    private bool StartEditingBarrels()
    {
        if (warningStyle == null)
        {
            warningStyle = new GUIStyle(EditorStyles.label);
            warningStyle.fontSize = 18;
            warningStyle.normal.textColor = Color.yellow;
            warningStyle.hover.textColor = new Color(1f, 0.5f, 0f);
        }
        barrelWrapper = firearmWrapper.barrelWrapper;
        if (barrelWrapper != null)
        {
            if (barrelWrapper.barrelPositions != null)
            {
                if (barrelWrapper.barrelPositions.Length > 0)
                {
                    barrelCount = barrelWrapper.barrelPositions.Length;
                    //chamberDistanceFromCenter = Vector3.Distance(cylinderWrapper.chamberPositions[0].transform.position, cylinderWrapper.cylinderCenter.transform.position);
                    bulletOffsetInBarrel = barrelWrapper.chamberedBulletPosition;
                    bulletRotationInBarrel = barrelWrapper.chamberedBulletRotation;
                    barrelPositions = barrelWrapper.barrelPositions;
                    bulletPreviewObjects = new GameObject[barrelCount];
                    BulletWrapper bulletWrapper = barrelWrapper.bulletWrapperPrefab.GetComponent<BulletWrapper>();
                    for (int i = 0; i < barrelPositions.Length; i++)
                    {
                        bulletPreviewObjects[i] = Instantiate(bulletWrapper.roundPrefab);
                        bulletPreviewObjects[i].transform.parent = barrelPositions[i].transform;
                    }
                }
            }
        }
        editingBarrels = true;
        if (barrelWrapper != null)
        {
            return false;
        }
        return true;
    }
    private void Update()
    {
        if (barrelWrapper != null)
        {
            if (barrelPositions == null)
            {
                return;
            }
            if (barrelPositions.Length != barrelCount || barrelPositions[0] == null)
            {
                for (int i = 0; i < barrelPositions.Length; i++)
                {
                    if (bulletPreviewObjects[i] != null)
                    {
                        DestroyImmediate(bulletPreviewObjects[i].gameObject);
                    }
                }
                //barrelPositions = new Transform[barrelCount];
                bulletPreviewObjects = new GameObject[barrelCount];
                BulletWrapper bulletWrapper = barrelWrapper.bulletWrapperPrefab.GetComponent<BulletWrapper>();
                for (int i = 0; i < barrelPositions.Length; i++)
                {
                    //barrelPositions[i] = new GameObject($"Chamber ({i})").transform;
                    //barrelPositions[i].transform.parent = chambersPosParent.transform;
                    bulletPreviewObjects[i] = Instantiate(bulletWrapper.roundPrefab);
                    bulletPreviewObjects[i].transform.parent = barrelPositions[i].transform;
                }
            }
            for (int i = 0; i < barrelCount; i++)
            {
                bulletPreviewObjects[i].transform.localPosition = bulletOffsetInBarrel;
                bulletPreviewObjects[i].transform.localRotation = Quaternion.Euler(bulletRotationInBarrel);
            }
        }
    }
    private void StopEditingBarrels()
    {
        if (barrelWrapper != null)
        {
            barrelWrapper.chamberedBulletPosition = bulletOffsetInBarrel;
            barrelWrapper.chamberedBulletRotation = bulletRotationInBarrel;
            for (int i = 0; i < barrelCount; i++)
            {
                DestroyImmediate(bulletPreviewObjects[i]);
            }
            barrelPositions = null;
            bulletPreviewObjects = null;
        }
        editingBarrels = false;
        barrelWrapper = null;
    }
}
