using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeleeWeaponWrapper))]
public class MeleeWeaponWrapperEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = target as MeleeWeaponWrapper;
        Transform targetTransform = t.transform;
        for (int i = 0; i < t.slashLines.Length; i++)
        {
            DrawSlashAndStabGizmosForSlashLine(t.slashLines[i], t);
        }
    }
    public override void OnInspectorGUI()
    {
        var serializedObject = new SerializedObject(target);
        serializedObject.Update();
        base.OnInspectorGUI();
        MeleeWeaponWrapper script = (MeleeWeaponWrapper)target;
        
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
            serializedObject.Update();
        }
        //if (script.meleeWeaponSO != null)
        //{
        //    if (script.meleeWeaponSO.meleeWeaponType == MeleeWeaponType.Sharp)
        //    {
        //        EditorGUILayout.BeginHorizontal();
        //        var multiStabLinesToggle = serializedObject.FindProperty("multipleStabLines");
        //        EditorGUILayout.PropertyField(multiStabLinesToggle);
        //        EditorGUILayout.EndHorizontal();
        //        serializedObject.Update();
        //    }
        //    else
        //    {
        //        script.multipleStabLines = false;
        //    }
        //}
        //else
        //{
        //    script.multipleStabLines = false;
        //}
        //if (script.multipleStabLines)
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    var multiStabColliders = serializedObject.FindProperty("stabColliders");
        //    multiStabColliders.isExpanded = true;
        //    EditorGUILayout.PropertyField(multiStabColliders);

        //    var multiStabLines = serializedObject.FindProperty("stabLines");
        //    multiStabLines.isExpanded = true;
        //    multiStabLines.arraySize = multiStabColliders.arraySize;
        //    EditorGUILayout.PropertyField(multiStabLines);
        //    EditorGUILayout.EndHorizontal();
        //    serializedObject.Update();
        //}
        if (script.hasEvents)
        {
            EditorGUILayout.BeginHorizontal();
            var secondaryClicked = serializedObject.FindProperty("onSecondaryClicked");
            //multiStabColliders.isExpanded = true;
            EditorGUILayout.PropertyField(secondaryClicked);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var secondaryReleased = serializedObject.FindProperty("onSecondaryReleased");
            EditorGUILayout.PropertyField(secondaryReleased);
            EditorGUILayout.EndHorizontal();
            serializedObject.Update();
        }
        //EditorGUILayout.Space();

        //SerializedProperty useNewSharpnessSettings =
        //    serializedObject.FindProperty("useNewSharpnessSettings");
        //EditorGUILayout.PropertyField(useNewSharpnessSettings);

        //if (useNewSharpnessSettings.boolValue)
        //{
        //    EditorGUI.indentLevel++;

        //    SerializedProperty bladeColliders =
        //        serializedObject.FindProperty("bladeColliders");
        //    bladeColliders.isExpanded = true;
        //    EditorGUILayout.PropertyField(bladeColliders, true);

        //    EditorGUILayout.PropertyField(
        //        serializedObject.FindProperty("bladeThickness"));

        //    SerializedProperty slashLines =
        //        serializedObject.FindProperty("slashLines");
            
        //    //EditorGUILayout.PropertyField(slashLines, true);

        //    EditorGUILayout.PropertyField(
        //        serializedObject.FindProperty("isSlashDualSided"));

        //    EditorGUILayout.PropertyField(
        //        serializedObject.FindProperty("slashForwardAngleRange"));

        //    EditorGUILayout.PropertyField(
        //        serializedObject.FindProperty("slashUpAngleRange"));

        //    EditorGUILayout.PropertyField(
        //        serializedObject.FindProperty("stabAngleRange"));

        //    EditorGUI.indentLevel--;
        //}

        serializedObject.ApplyModifiedProperties();
    }
    private void DrawSlashAndStabGizmosForSlashLine(MeleeWeaponWrapper.SlashLineInfo slashLineInfo, MeleeWeaponWrapper t)
    {
        if (slashLineInfo.slashBottom == null || slashLineInfo.slashTop == null || slashLineInfo.slashForward == null)
        {
            return;
        }
        Color fullSlashColor = Color.green;
        fullSlashColor.a = 0.4f;
        Color halfSlashColor = Color.yellow;
        halfSlashColor.a = 0.4f;
        Color fullStabColor = Color.cyan;
        fullStabColor.a = 0.4f;
        Color halfStabColor = new Color(1f, 0.6f, 0f, 0.4f);

        Handles.color = fullSlashColor;
        Vector3 slashArcCenter = (slashLineInfo.slashBottom.position + slashLineInfo.slashTop.position) / 2;
        Vector3 slashForwardDir = (slashLineInfo.slashForward.position - slashLineInfo.slashBottom.position).normalized;
        Vector3 slashUpDir = (slashLineInfo.slashTop.position - slashLineInfo.slashBottom.position).normalized;
        Vector3 slashPerpendicularDir = Vector3.Cross(slashForwardDir, slashUpDir).normalized;
        Handles.DrawSolidArc(slashArcCenter, slashUpDir, slashForwardDir, t.slashForwardAngleRange.x, 0.2f); // Slash angles relative to forward direction
        Handles.DrawSolidArc(slashArcCenter, slashUpDir, slashForwardDir, -t.slashForwardAngleRange.x, 0.2f);

        Handles.DrawSolidArc(slashLineInfo.slashTop.position, slashPerpendicularDir, slashForwardDir, t.slashUpAngleRange.x, 0.2f); // Slash angles relative to edge flow
        if (t.isSlashDualSided)
        {
            Handles.DrawSolidArc(slashLineInfo.slashTop.position, -slashPerpendicularDir, -slashForwardDir, t.slashUpAngleRange.x, 0.2f);
            Handles.DrawSolidArc(slashArcCenter, slashUpDir, -slashForwardDir, t.slashForwardAngleRange.x, 0.2f);
            Handles.DrawSolidArc(slashArcCenter, slashUpDir, -slashForwardDir, -t.slashForwardAngleRange.x, 0.2f);
        }


        Handles.color = halfSlashColor;
        Vector3 halfSlashDir = Vector3.RotateTowards(slashForwardDir, Vector3.Cross(slashForwardDir, slashUpDir), Mathf.Deg2Rad * t.slashForwardAngleRange.x, 0);
        Vector3 negativeHalfSlashDir = Vector3.RotateTowards(slashForwardDir, Vector3.Cross(slashUpDir, slashForwardDir), Mathf.Deg2Rad * t.slashForwardAngleRange.x, 0);
        Handles.DrawSolidArc(slashArcCenter, slashUpDir, halfSlashDir, -(t.slashForwardAngleRange.y - t.slashForwardAngleRange.x), 0.2f); // Slash angles relative to forward direction
        Handles.DrawSolidArc(slashArcCenter, slashUpDir, negativeHalfSlashDir, (t.slashForwardAngleRange.y - t.slashForwardAngleRange.x), 0.2f);

        Vector3 halfSlashUpDir = Vector3.RotateTowards(slashForwardDir, slashUpDir, Mathf.Deg2Rad * t.slashUpAngleRange.x, 0);
        Vector3 negativeHalfSlashUpDir = Vector3.RotateTowards(-slashForwardDir, slashUpDir, Mathf.Deg2Rad * t.slashUpAngleRange.x, 0);

        Handles.DrawSolidArc(slashLineInfo.slashTop.position, slashPerpendicularDir, halfSlashUpDir, (t.slashUpAngleRange.y - t.slashUpAngleRange.x), 0.2f); // Slash angles relative to edge flow
        if (t.isSlashDualSided)
        {
            Handles.DrawSolidArc(slashLineInfo.slashTop.position, -slashPerpendicularDir, negativeHalfSlashUpDir, (t.slashUpAngleRange.y - t.slashUpAngleRange.x), 0.2f);
            Handles.DrawSolidArc(slashArcCenter, slashUpDir, -halfSlashDir, -(t.slashForwardAngleRange.y - t.slashForwardAngleRange.x), 0.2f);
            Handles.DrawSolidArc(slashArcCenter, slashUpDir, -negativeHalfSlashDir, (t.slashForwardAngleRange.y - t.slashForwardAngleRange.x), 0.2f);
        }



        Handles.color = fullStabColor;
        Vector3 stabDirection = (slashLineInfo.slashTop.position - slashLineInfo.slashBottom.position).normalized;
        Handles.DrawSolidArc(slashLineInfo.slashTop.position, slashPerpendicularDir, stabDirection, t.stabAngleRange.x, 0.2f);
        Handles.DrawSolidArc(slashLineInfo.slashTop.position, slashPerpendicularDir, stabDirection, -t.stabAngleRange.x, 0.2f);

        Handles.color = halfStabColor;
        Vector3 halfStabDir = Vector3.RotateTowards(stabDirection, slashForwardDir, Mathf.Deg2Rad * t.stabAngleRange.x, 0);
        Vector3 negativeHalfStabDir = Vector3.RotateTowards(stabDirection, -slashForwardDir, Mathf.Deg2Rad * t.stabAngleRange.x, 0);
        Handles.DrawSolidArc(slashLineInfo.slashTop.position, slashPerpendicularDir, halfStabDir, -(t.stabAngleRange.y - t.stabAngleRange.x), 0.2f);
        Handles.DrawSolidArc(slashLineInfo.slashTop.position, slashPerpendicularDir, negativeHalfStabDir, (t.stabAngleRange.y - t.stabAngleRange.x), 0.2f);


        float slashResolution = 0.03f;
        float distanceFromBottomToTop = Vector3.Distance(slashLineInfo.slashBottom.position, slashLineInfo.slashTop.position);
        int slashResolutionCount = (int)(distanceFromBottomToTop / slashResolution);
        //Debug.Log($"Slash Resolution: {slashResolution}, Count: {slashResolutionCount}");
        Handles.color = Color.red;
        for (int i = 0; i <= slashResolutionCount; i++)
        {
            float distanceFromTop = distanceFromBottomToTop - i * slashResolution;
            float widthMultiplier = Mathf.Clamp01(distanceFromTop / t.meleeWeaponSO.stabDepthForMaxStabMultiplier);
            Vector3 pointOnSlashLine;
            if (i == slashResolutionCount)
            {
                pointOnSlashLine = slashLineInfo.slashTop.position;
            }
            else
            {
                pointOnSlashLine = slashLineInfo.slashBottom.position + i * slashResolution * slashUpDir;
            }
            Handles.DrawLine(pointOnSlashLine + 0.5f * widthMultiplier * t.bladeThickness * slashForwardDir, pointOnSlashLine - 0.5f * widthMultiplier * t.bladeThickness * slashForwardDir);
        }
    }
}
