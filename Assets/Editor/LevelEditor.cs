using Codice.CM.Common.Merge;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    int a;
    int b;


    [MenuItem("Window/Game Settings")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelEditor window = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        window.Show();
    }

    void OnGUI()
    {

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        EditorGUILayout.BeginToggleGroup("Base Settings", true);
        
        a = EditorGUILayout.IntField("AAA", a);
        b = EditorGUILayout.IntField("BBB", b);
  
        EditorGUILayout.EndToggleGroup();


        if (GUILayout.Button("Reload"))
        {
            GameSettings.Instance.ReloadScene();
        }


        if (GUILayout.Button("Save"))
        {
            //GameSettings.Instance.LoadDataFromEditor(BlastRadius, ComboBlastRadius, CurrentLevel, VaseHealth, StoneHealth, BoxHealth)   ;
            SaveEditorSettingsToPrefs();
        }


        if (GUILayout.Button("See Current Settings"))
        {
            LoadEditorSettingsFromPrefs();
        }


        if (GUILayout.Button("Reset"))
        {
            GameSettings.Instance.ReturnToDefaultSettings();
            a = GameSettings.Instance.A;
            b = GameSettings.Instance.B;
            SaveEditorSettingsToPrefs();
        }
    }


    void SaveEditorSettingsToPrefs()
    {
        EditorPrefs.SetInt("a", a);
        EditorPrefs.SetInt("b", b);
    }

    void LoadEditorSettingsFromPrefs()
    {
        a = EditorPrefs.GetInt("a", a);
        b = EditorPrefs.GetInt("b", b);

    }
}