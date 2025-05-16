using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static HandPoseScriptableObject;

public class MagazineHelper : EditorWindow
{
    [SerializeField]
    private GameObject magazineObject;
    [SerializeField]
    private GameObject bulletWrapperPrefab;
    private BulletWrapper bulletWrapper;
    private GameObject roundPrefab;
    [SerializeField]
    private int magazineCapacity;
    [SerializeField]
    private int maxRoundsToRender;
    [SerializeField]
    private float offsetPerRound;
    [SerializeField]
    private Transform firstRoundPos;
    [SerializeField]
    private Vector3 roundDirection = Vector3.down;
    [SerializeField]
    private Vector3 progressiveDirectionChange;
    [SerializeField]
    private Vector3 alternatingOffset;
    [SerializeField]
    private Vector3 progressiveRotation;
    [SerializeField]
    private Vector3 firstRoundRotation;

    private int lastRecordedCapacity;
    private int lastRecordedRoundsToRender;
    private GameObject[] instantiatedRounds;

    Vector2 scrollPos;
    
    [MenuItem("Modding Tools/Magazine Helper")]
    private static void Init()
    {
        var windowType = typeof(SceneView);
        var window = GetWindow<MagazineHelper>(windowType);
    }
    private void OnGUI()
    {
        var thisSerialized = new SerializedObject(this);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        if (GUILayout.Button("Set First Round Position"))
        {
            if (firstRoundPos == null)
            {
                GameObject firstRoundObj = new GameObject();
                firstRoundObj.name = "FirstRoundPosition";
                firstRoundPos = firstRoundObj.transform;
                firstRoundPos.parent = magazineObject.transform;
                firstRoundPos.localPosition = new Vector3(0, 0, 0);
                firstRoundPos.localRotation = Quaternion.identity;
            }
            UnityEditor.Selection.activeGameObject = firstRoundPos.gameObject;
        }
        SerializedProperty magProperty = thisSerialized.FindProperty("magazineObject");
        EditorGUILayout.PropertyField(magProperty, true);
        SerializedProperty bulletProperty = thisSerialized.FindProperty("bulletWrapperPrefab");
        EditorGUILayout.PropertyField(bulletProperty, true);
        SerializedProperty capacityProperty = thisSerialized.FindProperty("magazineCapacity");
        EditorGUILayout.PropertyField(capacityProperty, true);
        SerializedProperty roundsToRenderProperty = thisSerialized.FindProperty("maxRoundsToRender");
        EditorGUILayout.PropertyField(roundsToRenderProperty, true);
        SerializedProperty offsetProperty = thisSerialized.FindProperty("offsetPerRound");
        EditorGUILayout.PropertyField(offsetProperty, true);
        SerializedProperty dirProperty = thisSerialized.FindProperty("roundDirection");
        EditorGUILayout.PropertyField(dirProperty, true);
        SerializedProperty progDirProperty = thisSerialized.FindProperty("progressiveDirectionChange");
        EditorGUILayout.PropertyField(progDirProperty, true);
        SerializedProperty altOffsetProperty = thisSerialized.FindProperty("alternatingOffset");
        EditorGUILayout.PropertyField(altOffsetProperty, true);

        SerializedProperty firstRotProperty = thisSerialized.FindProperty("firstRoundRotation");
        EditorGUILayout.PropertyField(firstRotProperty, true);
        SerializedProperty progRotProperty = thisSerialized.FindProperty("progressiveRotation");
        EditorGUILayout.PropertyField(progRotProperty, true);
        

        thisSerialized.ApplyModifiedProperties();

        if (GUILayout.Button("Save Magazine Prefab As..."))
        {
            var path = EditorUtility.SaveFilePanelInProject("Save Magazine Prefab As", "magazinePrefab", "prefab", "Select the folder you want to save the magazine prefab to.");
            MagazineWrapper magWrapper = magazineObject.GetComponent<MagazineWrapper>();
            if (magWrapper == null)
                magWrapper = magazineObject.AddComponent<MagazineWrapper>();
            magWrapper.magazineCapacity = magazineCapacity;
            magWrapper.maxRoundsToRender = maxRoundsToRender;
            magWrapper.firstRoundPos = firstRoundPos;
            magWrapper.bulletWrapperPrefab = bulletWrapperPrefab;
            magWrapper.offsetPerRound = offsetPerRound;
            magWrapper.roundDirection = roundDirection;
            magWrapper.progressiveDirectionChange = progressiveDirectionChange;
            magWrapper.alternatingOffset = alternatingOffset;
            magWrapper.firstBulletRotation = firstRoundRotation;
            magWrapper.progressiveRotation = progressiveRotation;
            for (int i = 0; i < instantiatedRounds.Length; i++)
            {
                DestroyImmediate(instantiatedRounds[i]);
                lastRecordedCapacity = 0;
            }
            SaveMagazineAs(magazineObject, path);
        }
        EditorGUILayout.EndScrollView();
    }

    private void SaveMagazineAs(GameObject magazineObj, string path)
    {
        PrefabUtility.SaveAsPrefabAssetAndConnect(magazineObj, path, InteractionMode.UserAction);
    }
    private void Update()
    {
        if (bulletWrapperPrefab != null) 
        {
            if (bulletWrapper == null)
            {
                bulletWrapper = bulletWrapperPrefab.GetComponent<BulletWrapper>();
                roundPrefab = bulletWrapper.roundPrefab;
            }
        }
        if (instantiatedRounds != null)
        {
            for (int i = 0; i < instantiatedRounds.Length; i++)
            {
                if (instantiatedRounds[i] != null)
                {
                    firstRoundPos.rotation = Quaternion.Euler(Vector3.zero);
                    if (i == 0)
                    {
                        instantiatedRounds[i].transform.position = firstRoundPos.position + offsetPerRound * i * (roundDirection + progressiveDirectionChange * i);
                        instantiatedRounds[i].transform.localRotation = Quaternion.Euler(firstRoundRotation + i * progressiveRotation);
                    }
                    else if (i % 2 == 0)
                    {
                        instantiatedRounds[i].transform.position = firstRoundPos.position + offsetPerRound * i * (roundDirection + progressiveDirectionChange * i) + alternatingOffset;
                        instantiatedRounds[i].transform.localRotation = Quaternion.Euler(firstRoundRotation + i * progressiveRotation);
                    }
                    else
                    {
                        instantiatedRounds[i].transform.position = firstRoundPos.position + offsetPerRound * i * (roundDirection + progressiveDirectionChange * i) - alternatingOffset;
                        instantiatedRounds[i].transform.localRotation = Quaternion.Euler(firstRoundRotation + i * progressiveRotation);
                    }
                    firstRoundPos.localRotation = Quaternion.identity;
                }
            }
        }
        if (lastRecordedCapacity != magazineCapacity || lastRecordedRoundsToRender != maxRoundsToRender)
        {
            if (instantiatedRounds != null)
            {
                for (int i = 0; i < instantiatedRounds.Length; i++)
                {
                    DestroyImmediate(instantiatedRounds[i]);
                }
            }
            instantiatedRounds = new GameObject[Mathf.Clamp(magazineCapacity, 0, Mathf.Clamp(maxRoundsToRender, 0, 250))];
            for (int i = 0;i < instantiatedRounds.Length; i++)
            {
                instantiatedRounds[i] = Instantiate(roundPrefab);
                instantiatedRounds[i].transform.parent = firstRoundPos;
            }
        }
        lastRecordedCapacity = magazineCapacity;
        lastRecordedRoundsToRender = maxRoundsToRender;
    }
    private void OnDestroy()
    {
        for (int i = 0; i < instantiatedRounds.Length; i++)
        {
            DestroyImmediate(instantiatedRounds[i]);
        }
    }
}
