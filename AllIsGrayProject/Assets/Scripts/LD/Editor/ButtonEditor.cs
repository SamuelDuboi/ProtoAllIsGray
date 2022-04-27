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
        if (targetB.door)
        {
            var _color = Handles.color;
            Handles.color = targetB.color;
            Handles.DrawLine(targetB.transform.position, targetB.door.transform.position, targetB.size);
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
        if (!targetB.door)
            EditorGUILayout.HelpBox("Need a door to work", MessageType.Warning);
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);

    }


}
