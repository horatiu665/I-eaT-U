using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

public class BindProperty : MonoBehaviour
{
    public GameObject Origin;
    public MonoBehaviour OriginScript;
    public FieldInfo OriginField;

    public GameObject Target;
    public MonoBehaviour TargetScript;
    public FieldInfo TargetField;

    public int OriginScriptSelected;
    public int TargetScriptSelected;

    public int OriginFieldSelected;
    public int TargetFieldSelected;

    void Start()
    {
        if (OriginField.FieldType != TargetField.FieldType)
        {
            Debug.LogError("Bind property failed! Origin Field and Target are not of the same type.");
            enabled = false;
        }
    }

    void Update()
    {
        TargetField.SetValue(TargetScript, OriginField.GetValue(OriginScript));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof (BindProperty))]
class BindPropertyEditor : Editor
{
    public FieldInfo[] OriginAvailableFields;
    public FieldInfo[] TargetAvailableFields;

    void OnEnable()
    {
        var bp = target as BindProperty;
        if (!bp)
            return;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        var bp = target as BindProperty;
        if (!bp)
            return;

        bp.Origin = EditorGUILayout.ObjectField("Origin", bp.Origin, typeof(GameObject), true) as GameObject;
        if (bp.Origin)
        {
            var behavs = bp.Origin.transform.GetComponents<MonoBehaviour>();
            bp.OriginScriptSelected = EditorGUILayout.Popup("Origin Script", bp.OriginScriptSelected,
                behavs.Select(b => b.GetType().Name).ToArray());

            bp.OriginScript = behavs[bp.OriginScriptSelected];
        }

        if (bp.OriginScript)
        {
            OriginAvailableFields = bp.OriginScript.GetType().GetFields().ToArray();
            bp.OriginFieldSelected = EditorGUILayout.Popup("Origin Field", bp.OriginFieldSelected,
                OriginAvailableFields.Select(o => o.Name).ToArray());

            bp.OriginField = OriginAvailableFields[bp.OriginFieldSelected];
        }

        EditorGUILayout.Space();

        bp.Target = EditorGUILayout.ObjectField("Target", bp.Target, typeof(GameObject), true) as GameObject;
        if (bp.Target)
        {
            var behavs = bp.Target.transform.GetComponents<MonoBehaviour>();
            bp.TargetScriptSelected = EditorGUILayout.Popup("Target Script", bp.TargetScriptSelected,
                behavs.Select(b => b.GetType().Name).ToArray());

            bp.TargetScript = behavs[bp.TargetScriptSelected];
        }

        if (bp.TargetScript)
        {
            TargetAvailableFields = bp.TargetScript.GetType().GetFields().ToArray();
            bp.TargetFieldSelected = EditorGUILayout.Popup("Target Field", bp.TargetFieldSelected,
                TargetAvailableFields.Select(o => o.Name).ToArray());

            bp.TargetField = TargetAvailableFields[bp.TargetFieldSelected];
        }

    }
}
#endif
