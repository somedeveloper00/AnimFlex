using System;
using AnimFlex.Core;
using AnimFlex.Tweener;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AnimFlex.Editor
{
    public static class PreviewUtils
    {
        private static float lastTickTime = 0;

        public static bool isActive
        {
            get => UnityEditor.EditorPrefs.GetBool("AnimFlex_previewIsActive", false);
            private set => UnityEditor.EditorPrefs.SetBool("AnimFlex_previewIsActive", value);
        }

        private static int lastSelectedID;

        private static event Action onEnd;
        private static float startTime = 0;

        [InitializeOnLoadMethod]
        private static void StopPreviewIfActive()
        {
            if (isActive)
            {
                EditorApplication.delayCall += StopPreviewMode;
            }
        }

        public static void StartPreviewMode()
        {
            if (EditorApplication.isPlaying)
            {
                Debug.LogError("Can't start preview mode while in play mode!");
                return;
            }
            if (isActive)
            {
                StopPreviewMode();
                Debug.LogWarning($"Preview already in progress: automatically stopped the previous one.");
            }
            
            // save all changes
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            
            // keep track of selection
            if (Selection.activeGameObject != null)
            {
                lastSelectedID = Selection.activeGameObject?.AddComponent<UniqueID>().GetID() ?? 0;
                EditorUtility.SetDirty(Selection.activeGameObject);
                EditorSceneManager.SaveScene(Selection.activeGameObject.scene);
            }

            AnimFlexCore.Initialize();
            EditorApplication.update += EditorTick;
            lastTickTime = (float)EditorApplication.timeSinceStartup;
            startTime = lastTickTime;
            isActive = true;
            Debug.Log("Preview started.");

            // handling sudden play mode
            EditorApplication.playModeStateChanged += change =>
            {
                if (isActive)
                {
                    if (change == PlayModeStateChange.ExitingEditMode)
                    {
                        EditorApplication.isPlaying = false;
                        Debug.LogError($"You shouldn't enter playmode while in preview mode!");
                        StopPreviewMode();
                    }
                }
            };
            
            
            // handling inspector editing
            SceneView.duringSceneGui += OnSceneGUI;
            foreach (var component in Object.FindObjectsOfType<Component>())
            {
                var flags = component.hideFlags;
                onEnd += () => component.hideFlags = flags;
                component.hideFlags |= HideFlags.NotEditable;
            }
        }

        private static void EditorTick()
        {
            // ignoring long frames altogether (maybe user changed the window or whatever else)
            if (EditorApplication.timeSinceStartup - lastTickTime < 1f)
            {
                AnimFlexCore.Instance.Tick((float)EditorApplication.timeSinceStartup - lastTickTime);
                SceneView.RepaintAll();
            }
            lastTickTime = (float)EditorApplication.timeSinceStartup;
        }

        public static void StopPreviewMode()
        {
            if(!isActive) return;
            
            EditorApplication.update -= EditorTick;
            
            
            // restore selection
            EditorSceneManager.sceneOpened += OnEditorSceneManagerOnsceneOpened; 
            
            // discard new changes
            EditorSceneManager.OpenScene(EditorSceneManager.GetActiveScene().path, OpenSceneMode.Single);

            SceneView.duringSceneGui -= OnSceneGUI; 
            
            GC.Collect();
            isActive = false;
            Debug.Log($"Preview stopped");
        }

        private static void OnEditorSceneManagerOnsceneOpened(Scene scene, OpenSceneMode openSceneMode)
        {
            // get last selected gameObject
            if (UniqueID.FindByID(lastSelectedID, out var activeGameObject))
            {
                if (activeGameObject == null) return;
                Selection.activeGameObject = activeGameObject;
            }

            // remove all other UniqueID components 
            foreach (var comp in Object.FindObjectsOfType<UniqueID>())
            {
                EditorUtility.SetDirty(comp.gameObject);
                Object.DestroyImmediate(comp);
            }

            EditorSceneManager.SaveScene(activeGameObject.scene);
            EditorSceneManager.sceneOpened -= OnEditorSceneManagerOnsceneOpened;
        }

        private static void OnSceneGUI(SceneView sceneview)
        {
            Handles.BeginGUI();
            GUILayout.BeginArea(
                new Rect(
                    sceneview.position.width / 2f - 100,
                    sceneview.position.height - 60,
                    200,
                    60));

            GUI.color = AFStyles.BoxColor;

            EditorGUI.DrawRect(new Rect(0, 0, 200, 60), AFStyles.BoxColorDarker);
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.ExpandHeight(true)))
            {
                GUI.color = Color.white;
                GUILayout.Label("AnimFlex Playing...", AFStyles.SpecialLabel);
                if (GUILayout.Button("Stop", AFStyles.Button))
                {
                    StopPreviewMode();
                }
            }
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        
        public static void PreviewSequence(Sequence sequence)
        {
            if (isActive)
            {
                StopPreviewMode();
                return;
            }
            StartPreviewMode();
            sequence.Play();
            sequence.onComplete += StopPreviewMode;
        }

        public static void PreviewTweener(GeneratorData tweenData)
        {
            EditorApplication.delayCall += () =>
            {
                if (isActive)
                {
                    StopPreviewMode();
                    return;
                }

                StartPreviewMode();
                if (GeneratorDataUtil.TryGenerateTweener(tweenData, out var tweener) == false)
                {
                    StopPreviewMode();
                }
            };
        }
    }
}