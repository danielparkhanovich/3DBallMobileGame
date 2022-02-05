using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomEditorUtilities : Editor
{
    public static void HorizontalInspectorLine(Color color, string textAbove="", string textBelow="")
    {
        // create your style
        GUIStyle horizontalLine;
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 4, 4);
        horizontalLine.fixedHeight = 1;

        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };

        GUILayout.Label(textAbove, style);
        SimpleHorizontalLine(color, horizontalLine);
        GUILayout.Label(textBelow, style);
  
    }
    private static void SimpleHorizontalLine(Color color, GUIStyle style)
    {
        var c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, style);
        GUI.color = c;
    }
}
