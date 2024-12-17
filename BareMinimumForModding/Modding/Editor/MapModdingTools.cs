using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapModdingTools : EditorWindow
{
    private bool hideAfterSettingMaterial;
    private bool addMeshCollider;
    private bool setAllWithSameMat;
    private Transform parentObject;
    Vector2 scrollPos;
    [SerializeField]
    private MaterialNamesForObjectTypes extraMaterialNamesForObjectTypes;
    [MenuItem("Modding Tools/Modding Tools Tab")]
    private static void Init()
    {
        var windowType = typeof(SceneView);
        var window = GetWindow<MapModdingTools>(windowType);
    }
    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Change Selected Objects to Dirt");
        if (GUILayout.Button("Dirt", GUILayout.Height(30), GUILayout.Width(100)))
        {
            if (setAllWithSameMat)
            {
                ModToolScripts.SetAllWithSameMaterialAsSelected(parentObject, "Dirt", addMeshCollider, hideAfterSettingMaterial);
            }
            else
                ModToolScripts.ChangeSelectedToDirt(hideAfterSettingMaterial);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Change Selected Objects to Concrete");
        if (GUILayout.Button("Concrete", GUILayout.Height(30), GUILayout.Width(100)))
        {
            if (setAllWithSameMat)
            {
                ModToolScripts.SetAllWithSameMaterialAsSelected(parentObject, "Concrete", addMeshCollider, hideAfterSettingMaterial);
            }
            else
                ModToolScripts.ChangeSelectedToConcrete(hideAfterSettingMaterial);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Change Selected Objects to Metal");
        if (GUILayout.Button("Metal", GUILayout.Height(30), GUILayout.Width(100)))
        {
            if (setAllWithSameMat)
            {
                ModToolScripts.SetAllWithSameMaterialAsSelected(parentObject, "Metal", addMeshCollider, hideAfterSettingMaterial);
            }
            else
                ModToolScripts.ChangeSelectedToMetal(hideAfterSettingMaterial);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Change Selected Objects to Wood");
        if (GUILayout.Button("Wood", GUILayout.Height(30), GUILayout.Width(100)))
        {
            if (setAllWithSameMat)
            {
                ModToolScripts.SetAllWithSameMaterialAsSelected(parentObject, "Wood", addMeshCollider, hideAfterSettingMaterial);
            }
            else
                ModToolScripts.ChangeSelectedToWood(hideAfterSettingMaterial);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Change Selected Objects to Glass");
        if (GUILayout.Button("Glass", GUILayout.Height(30), GUILayout.Width(100)))
        {
            if (setAllWithSameMat)
            {
                ModToolScripts.SetAllWithSameMaterialAsSelected(parentObject, "Glass", addMeshCollider, hideAfterSettingMaterial);
            }
            else
            ModToolScripts.ChangeSelectedToGlass(hideAfterSettingMaterial);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("The Parent to Look Up Child Materials For");
        parentObject = EditorGUILayout.ObjectField(parentObject, typeof(Transform), true) as Transform;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        var thisSerialized = new SerializedObject(this);
        SerializedProperty matNamesForObjectTypes = thisSerialized.FindProperty("extraMaterialNamesForObjectTypes");
        EditorGUILayout.PropertyField(matNamesForObjectTypes, true);
        thisSerialized.ApplyModifiedProperties();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Try to Set Parent According to Material Name");
        if (GUILayout.Button("Set Child Materials", GUILayout.Height(30), GUILayout.Width(150)))
        {
            ModToolScripts.SetAllAccordingToMaterial(parentObject, extraMaterialNamesForObjectTypes, addMeshCollider);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Hide Object in Editor\nAfter Setting Material", GUILayout.Height(30f));
        hideAfterSettingMaterial = EditorGUILayout.Toggle(hideAfterSettingMaterial, GUILayout.Height(30f));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Automatically Add Mesh Collider", GUILayout.Height(30f));
        addMeshCollider = EditorGUILayout.Toggle(addMeshCollider, GUILayout.Height(30f));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Set All Objects With Same Material", GUILayout.Height(30f));
        setAllWithSameMat = EditorGUILayout.Toggle(setAllWithSameMat, GUILayout.Height(30f));
        EditorGUILayout.EndHorizontal();
        /*EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Generate Colliders for Children");
        if (GUILayout.Button("Generate Colliders", GUILayout.Height(30), GUILayout.Width(150)))
        {
            ModToolScripts.CreateCollidersForAllChildren(parentObject, 0.1f);
        }
        EditorGUILayout.EndHorizontal();*/
        EditorGUILayout.EndScrollView();
    }
    [System.Serializable]
    public struct MaterialNamesForObjectTypes
    {
        public string[] dirtMaterialNames;
        public string[] concreteMaterialNames;
        public string[] metalMaterialNames;
        public string[] woodMaterialNames;
        public string[] glassMaterialNames;
    }
}
