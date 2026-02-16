using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine;

class EditorScrips : EditorWindow
{
    [MenuItem("Play/PlayMe _%h")]
    public static void RunMainScene()
    {
        if (!EditorApplication.isPlaying)
        {
            string currentSceneName = EditorSceneManager.GetActiveScene().path;
            File.WriteAllText(".lastScene", currentSceneName);

            EditorSceneManager.OpenScene("Assets/Scenes/00 Main Menu.unity");
            EditorApplication.isPlaying = true;
        }
        else if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            
            if (File.Exists(".lastScene"))
            {
                string lastScene = File.ReadAllText(".lastScene");
                Debug.Log("Returning to last scene: " + lastScene);
                EditorSceneManager.OpenScene(lastScene);
            }
        }
    }
}