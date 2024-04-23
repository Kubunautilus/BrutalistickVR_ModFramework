using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeleeScriptableObject))]
public class MeleeScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MeleeScriptableObject script = (MeleeScriptableObject)target;

        if (script.hitSoundsType == HitSoundsType.DoubleMaterial)
        {

            EditorGUILayout.BeginHorizontal();
            var serializedObject = new SerializedObject(target);
            var firstTag = serializedObject.FindProperty("firstColliderAudioTag");
            EditorGUILayout.PropertyField(firstTag);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var secondTag = serializedObject.FindProperty("secondColliderAudioTag");
            EditorGUILayout.PropertyField(secondTag);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var property = serializedObject.FindProperty("firstColliderTagAudioClips");
            EditorGUILayout.PropertyField(property, true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var property2 = serializedObject.FindProperty("secondColliderTagAudioClips");
            EditorGUILayout.PropertyField(property2, true);
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            var serializedObject = new SerializedObject(target);
            var property = serializedObject.FindProperty("firstColliderTagAudioClips");
            serializedObject.Update();
            EditorGUILayout.PropertyField(property, new GUIContent("Collision Audio Clips"), true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
        }
        if (script.meleeWeaponType == MeleeWeaponType.Sharp)
        {

            EditorGUILayout.BeginHorizontal();
            var serializedObject = new SerializedObject(target);
            var sliceThreshold = serializedObject.FindProperty("sliceThreshold");
            EditorGUILayout.PropertyField(sliceThreshold);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var slicePower = serializedObject.FindProperty("slicePower");
            EditorGUILayout.PropertyField(slicePower);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var stabberSharpness = serializedObject.FindProperty("stabberSharpness");
            EditorGUILayout.PropertyField(stabberSharpness);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var tipAndBaseCanStab = serializedObject.FindProperty("tipAndBaseCanStab");
            EditorGUILayout.PropertyField(tipAndBaseCanStab);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var canStabberRunThrough = serializedObject.FindProperty("canStabberRunThrough");
            EditorGUILayout.PropertyField(canStabberRunThrough);
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}
