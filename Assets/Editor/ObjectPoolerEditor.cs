using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var defaultColor = GUI.backgroundColor;

        ObjectPooler pooler = (ObjectPooler)target;

        if (pooler.pooledObjectsList == null)
        {
            return;
        }

        for (int i = 0; i < pooler.pooledObjectsList.Count; i++)
        {
            try
            {
                var objects = pooler.pooledObjectsList[i];
                var objectType = pooler.itemsToPool[i];

                CustomEditorUtilities.HorizontalInspectorLine(Color.gray, textBelow: "Objects: " + objectType.objectToPool.name);
                GUILayout.Label("Objects count: " + objects.Count);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return;
            }
        }
    }
}
