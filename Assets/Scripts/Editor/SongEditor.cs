using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SongEditor : EditorWindow
{
    private Vector2 scrollPos;

    public Texture2D t;

    [MenuItem ("Window/Song Editor")]
    public static void Init()
    {
        GetWindow(typeof(SongEditor));
    }

    public void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        EditorGUI.DrawPreviewTexture(new Rect(0, 0, 100, 100), t);

        EditorGUILayout.EndScrollView();
    }
}
