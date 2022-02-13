using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(ProceduralGeneration))]
public class ProceduralGenerationEditor : Editor
{
    private int biomValue;
    private bool onPoolingToggle = false;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var defaultColor = GUI.backgroundColor;

        ProceduralGeneration proc = (ProceduralGeneration)target;

        if (!proc.BallTransform)
            proc.BallTransform = PlayerController.Instance.transform;

        CustomEditorUtilities.HorizontalInspectorLine(Color.gray, textBelow: "Debug area");
        GUILayout.Label("Generated rings: " + proc.GeneratedRings);
        GUILayout.Label("Current biom: " + proc.BiomsGenerator.CurrentBiom.BiomName);
        GUILayout.Label("Pooling is active: " + proc.IsPooling);

        CustomEditorUtilities.HorizontalInspectorLine(Color.gray, textBelow: "Test procedural generation area");

        onPoolingToggle = EditorGUILayout.Toggle("On pooling", onPoolingToggle);

        GUI.backgroundColor = new Color32(184, 15, 10, 255);  
        if (GUILayout.Button("Reset"))
        {
            proc.ResetRings();
        }
        GUI.backgroundColor = defaultColor;

        GUI.backgroundColor = new Color32(119, 198, 110, 255);
        if (GUILayout.Button("Next ring"))
        {
            proc.IsEditorUsing = true;

            Random.InitState((int)Time.realtimeSinceStartup);
            if (proc.GeneratedRings == 0)
            {
                proc.ObjectPooler.Awake();
                proc.PrerenderRings(onPoolingToggle);
            }
            else
            {
                proc.OverlapRing(true);
                proc.NewRingEvent.Invoke();
            }
        }
        GUI.backgroundColor = defaultColor;

        GUI.backgroundColor = new Color32(137, 207, 240, 255);
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Change biom: ");

        var notNullOnly = proc.BiomsGenerator.PredefinedBioms.Where(x => x != null);

        biomValue = EditorGUILayout.Popup(biomValue, notNullOnly.Select(x => x.Biom.BiomName).ToArray());
        if (proc.BiomsGenerator.CurrentBiom != proc.BiomsGenerator.PredefinedBioms[biomValue].Biom)
        {
            proc.BiomsGenerator.CurrentBiom = proc.BiomsGenerator.PredefinedBioms[biomValue].Biom;
        }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = defaultColor;
    }
}
