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
        if (script.multipleGrabPoints)
        {
            EditorGUILayout.BeginHorizontal();
            var multiGrabPoints = serializedObject.FindProperty("grabPointObjects");
            multiGrabPoints.isExpanded = true;
            EditorGUILayout.PropertyField(multiGrabPoints);
            var multiGrabTopAndBottom = serializedObject.FindProperty("grabTopAndBottomObjects");
            multiGrabTopAndBottom.isExpanded = true;
            multiGrabTopAndBottom.arraySize = multiGrabPoints.arraySize;
            EditorGUILayout.PropertyField(multiGrabTopAndBottom);
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
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
        if (script.hasEvents)
        {
            EditorGUILayout.BeginHorizontal();
            var secondaryClicked = serializedObject.FindProperty("onSecondaryClicked");
            //multiStabColliders.isExpanded = true;
            EditorGUILayout.PropertyField(secondaryClicked);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var secondaryReleased = serializedObject.FindProperty("onSecondaryReleased");
            EditorGUILayout.PropertyField(secondaryReleased);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}
