using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeleeWeaponWrapper))]
public class MeleeWeaponWrapperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MeleeWeaponWrapper script = (MeleeWeaponWrapper)target;
        var serializedObject = new SerializedObject(target);
        if (script.meleeWeaponSO != null)
        {
            if (script.meleeWeaponSO.meleeWeaponType == MeleeWeaponType.Sharp)
            {
                EditorGUILayout.BeginHorizontal();
                var multiStabLinesToggle = serializedObject.FindProperty("multipleStabLines");
                EditorGUILayout.PropertyField(multiStabLinesToggle);
                EditorGUILayout.EndHorizontal();
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            else
            {
                script.multipleStabLines = false;
            }
        }
        else
        {
            script.multipleStabLines = false;
        }
        if (script.multipleStabLines)
        {
            EditorGUILayout.BeginHorizontal();
            var multiStabColliders = serializedObject.FindProperty("stabColliders");
            multiStabColliders.isExpanded = true;
            EditorGUILayout.PropertyField(multiStabColliders);
            serializedObject.ApplyModifiedProperties();

            var multiStabLines = serializedObject.FindProperty("stabLines");
            multiStabLines.isExpanded = true;
            multiStabLines.arraySize = multiStabColliders.arraySize;
            EditorGUILayout.PropertyField(multiStabLines);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}
