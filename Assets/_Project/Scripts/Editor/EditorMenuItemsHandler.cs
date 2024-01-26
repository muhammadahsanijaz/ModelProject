
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace MoonKart
{
    public class EditorMenuItemsHandler : UnityEditor.Editor
    {
        private static ScriptableObject _toolbar;
        private static string[] _scenePaths;
        private static string[] _sceneNames;

        static EditorMenuItemsHandler()
        {
            EditorApplication.delayCall += () => {
                EditorApplication.update -= Update;
                EditorApplication.update += Update;
            };
        }

        private const string namespaceName = "ModelProject";
        [MenuItem(namespaceName + "/SelectVehicle")]
        static void SelectMainVehicle()
        {
            Selection.activeObject = Resources.Load<GameObject>("DB/Karts/Moon_Kart_1");
        }

        [MenuItem(namespaceName + "/CarSetting")]
        static void SelectCarSetting()
        {
            Selection.activeObject = Resources.Load<MoonKart.PlayerSettings>("Settings/CarSettings");
        }

        [MenuItem(namespaceName + "/CardsLibrary")]
        static void SelectCardLibrary()
        {
            //Selection.activeObject = Resources.Load<CardsLibrary>("Settings/CardsLibrary");
        }

        [MenuItem(namespaceName + "/ToogleGizmo")]
        static void ToggleGizmo()
        {
            //  StaticReferenceManager.ShowGizmo = !StaticReferenceManager.ShowGizmo;
        }

        [MenuItem(namespaceName + "/ClearAllData")]
        static void ClearAllData()
        {
            // Resources.Load<CardsLibrary>("Settings/CardsLibrary").ResetAllCardStatestAll();
        }

        [MenuItem(namespaceName + "/SelectEditorScript")]
        static void SelectMainThisScript()
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/_Project/Scripts/Editor/EditorMenuItemsHandler.cs");
        }

        private static void Update()
        {
            if (_toolbar == null)
            {
                Assembly editorAssembly = typeof(UnityEditor.Editor).Assembly;

                UnityEngine.Object[] toolbars = UnityEngine.Resources.FindObjectsOfTypeAll(editorAssembly.GetType("UnityEditor.Toolbar"));
                _toolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
                if (_toolbar != null)
                {
                    var root = _toolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
                    var rawRoot = root.GetValue(_toolbar);
                    var mRoot = rawRoot as VisualElement;
                    RegisterCallback("ToolbarZoneRightAlign", OnGUI);

                    void RegisterCallback(string root, Action cb)
                    {
                        var toolbarZone = mRoot.Q(root);
                        if (toolbarZone != null)
                        {
                            var parent = new VisualElement()
                            {
                                style = {
                flexGrow = 1,
                flexDirection = FlexDirection.Row,
              }
                            };
                            var container = new IMGUIContainer();
                            container.onGUIHandler += () =>
                            {
                                cb?.Invoke();
                            };
                            parent.Add(container);
                            toolbarZone.Add(parent);
                        }
                    }
                }
            }
            

            if (_scenePaths == null || _scenePaths.Length != EditorBuildSettings.scenes.Length)
            {
                List<string> scenePaths = new List<string>();
                List<string> sceneNames = new List<string>();

                foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
                {
                    if (scene.path == null || scene.path.StartsWith("Assets") == false)
                        continue;

                    string scenePath = Application.dataPath + scene.path.Substring(6);

                    scenePaths.Add(scenePath);
                    sceneNames.Add(Path.GetFileNameWithoutExtension(scenePath));
                }

                _scenePaths = scenePaths.ToArray();
                _sceneNames = sceneNames.ToArray();
            }
        }

        private static void OnGUI()
        {
            using (new EditorGUI.DisabledScope(Application.isPlaying))
            {

                {
                    string sceneName = EditorSceneManager.GetActiveScene().name;
                    int sceneIndex = -1;

                    for (int i = 0; i < _sceneNames.Length; ++i)
                    {
                        if (sceneName == _sceneNames[i])
                        {
                            sceneIndex = i;
                            break;
                        }
                    }

                    int newSceneIndex = EditorGUILayout.Popup(sceneIndex, _sceneNames, GUILayout.Width(200.0f));
                    if (newSceneIndex != sceneIndex)
                    {
                        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        {
                            EditorSceneManager.OpenScene(_scenePaths[newSceneIndex], OpenSceneMode.Single);
                        }
                    }
                }
            }
        }


    }
}
