using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(BiomsGenerator))]
public class BiomsGeneratorEditor : Editor
{
    private Object biomData;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var defaultColor = GUI.backgroundColor;

        BiomsGenerator bg = (BiomsGenerator)target;

        serializedObject.Update();
        CustomEditorUtilities.HorizontalInspectorLine(Color.gray, textBelow: "Manual generation");

        GUILayout.BeginHorizontal();

        GUILayout.Label("Select previous biom: ");
        biomData = EditorGUILayout.ObjectField(biomData, typeof(BiomData), true);

        if (GUILayout.Button("Generate new scriptable biom"))
        {
            bg.GenerateNextBiom(((BiomData)biomData).Biom, true);
        }

        GUILayout.EndHorizontal();

        GUI.backgroundColor = defaultColor;
    }
}
