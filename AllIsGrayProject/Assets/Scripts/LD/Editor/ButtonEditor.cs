using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Button))]
public class ButtonEditor : Editor
{

    private Button targetB;

    private void OnSceneGUI()
    {
        if (!targetB)
            targetB = target as Button;
        if (targetB.doors.Length > 0)
        {
            var _color = Handles.color;
            Handles.color = targetB.color;
            foreach (var door in targetB.doors)
            {
                Handles.DrawLine(targetB.transform.position, door.transform.position, targetB.size);

            }
            Handles.color = _color;
        }
    }
    private void OnEnable()
    {
        if (!targetB)
            targetB = target as Button;
       
    }
    public override void OnInspectorGUI()
    {
        if (targetB.doors.Length == 0)
            EditorGUILayout.HelpBox("Need a door to work", MessageType.Warning);
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);

    }


}
