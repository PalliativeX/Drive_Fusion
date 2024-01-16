using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dreamcore.Core
{
    /// <summary>
    /// Scene auto loader. Add this file in Assets/Editor folder
    /// </summary>
    /// <description>
    /// This class adds a File > Scene Autoload menu containing options to select
    /// a "master scene" enable it to be auto-loaded when the user presses play
    /// in the editor. When enabled, the selected scene will be loaded on play,
    /// then the original scene will be reloaded on stop.
    ///
    /// Based on an idea on this thread:
    /// http://forum.unity3d.com/threads/157502-Executing-first-scene-in-build-settings-when-pressing-play-button-in-editor
    /// </description>
    [InitializeOnLoad]
    public static class SceneAutoLoader
    {
        // Static constructor binds a playmode-changed callback.
        // [InitializeOnLoad] above makes sure this gets executed.
        static SceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        // Menu items to select the "master" scene and control whether or not to load it.
        [MenuItem("ProjectTools/SceneAutoLoad/Select Starting Scene")]
        private static void SelectMasterScene()
        {
            string masterScene = EditorUtility.OpenFilePanel("Select Master Scene", Application.dataPath, "unity");
            masterScene =
                masterScene.Replace(Application.dataPath, "Assets"); //project relative instead of absolute path
            if (!string.IsNullOrEmpty(masterScene))
            {
                MasterScene = masterScene;
                LoadMasterOnPlay = true;
            }
        }

        [MenuItem("ProjectTools/SceneAutoLoad/Load Selected Scene At Startup", true)]
        private static bool ShowLoadMasterOnPlay()
        {
            return !LoadMasterOnPlay;
        }

        [MenuItem("ProjectTools/SceneAutoLoad/Load Selected Scene At Startup")]
        private static void EnableLoadMasterOnPlay()
        {
            LoadMasterOnPlay = true;
        }

        [MenuItem("ProjectTools/SceneAutoLoad/Dont Load Selected Scene At Startup", true)]
        private static bool ShowDontLoadMasterOnPlay()
        {
            return LoadMasterOnPlay;
        }

        [MenuItem("ProjectTools/SceneAutoLoad/Dont Load Selected Scene At Startup")]
        private static void DisableLoadMasterOnPlay()
        {
            LoadMasterOnPlay = false;
        }

        // NOTE: Play mode change callback handles the scene load/reload.
        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (!LoadMasterOnPlay)
            {
                return;
            }

            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // NOTE: User pressed play -- autoload master scene.
                PreviousScene = SceneManager.GetActiveScene().path;
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    try
                    {
                        EditorSceneManager.OpenScene(MasterScene);
                    }
                    catch
                    {
                        LogSceneNotFound(MasterScene);
                        EditorApplication.isPlaying = false;
                    }
                }
                else
                {
                    // NOTE: User cancelled the save operation -- cancel play as well.
                    EditorApplication.isPlaying = false;
                }
            }

            // NOTE: isPlaying check required because cannot OpenScene while playing
            if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // NOTE: User pressed stop -- reload previous scene.
                try
                {
                    EditorSceneManager.OpenScene(PreviousScene);
                }
                catch
                {
                    LogSceneNotFound(PreviousScene);
                }
            }
        }

        private static void LogSceneNotFound(string masterScene)
        {
            Debug.LogError($"Error: scene not found: {masterScene}");
        }

        // NOTE: Properties are remembered as editor preferences.
        private const string EditorPrefLoadMasterOnPlay = "SceneAutoLoader.LoadMasterOnPlay";
        private const string EditorPrefMasterScene = "SceneAutoLoader.MasterScene";
        private const string EditorPrefPreviousScene = "SceneAutoLoader.PreviousScene";

        private static bool LoadMasterOnPlay
        {
            get => EditorPrefs.GetBool(EditorPrefLoadMasterOnPlay, false);
            set => EditorPrefs.SetBool(EditorPrefLoadMasterOnPlay, value);
        }

        private static string MasterScene
        {
            get => EditorPrefs.GetString(EditorPrefMasterScene, "Master.unity");
            set => EditorPrefs.SetString(EditorPrefMasterScene, value);
        }

        private static string PreviousScene
        {
            get => EditorPrefs.GetString(EditorPrefPreviousScene, SceneManager.GetActiveScene().path);
            set => EditorPrefs.SetString(EditorPrefPreviousScene, value);
        }
    }
}