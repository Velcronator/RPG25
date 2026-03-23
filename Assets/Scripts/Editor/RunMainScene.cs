using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

class EditorScrips : EditorWindow
{
    [MenuItem("Play/PlayMe _%h")]
    public static void RunMainScene()
    {
        if (!EditorApplication.isPlaying)
        {
            Scene activeScene = EditorSceneManager.GetActiveScene();
            if (activeScene.isDirty)
            {
                EditorSceneManager.SaveScene(activeScene);
            }

            string currentSceneName = activeScene.path;
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