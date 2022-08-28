using System;
using System.Diagnostics;
using AnimFlex.Core;
using AnimFlex.Tweener;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AnimFlex.Editor
{
    public static class PreviewUtils
    {
        private static Stopwatch stopWatch = new();
        private static float lastTickTime = 0;

        public static bool isActive { get; private set; } = false;

        private static int lastSelectedID;

        private static event Action onEnd;

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
            stopWatch.Restart();
            lastTickTime = (float)EditorApplication.timeSinceStartup;
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
            if(EditorApplication.timeSinceStartup - lastTickTime >= 0.2f) return;
            AnimFlexCore.Instance.Tick((float)EditorApplication.timeSinceStartup - lastTickTime);
            lastTickTime = (float)EditorApplication.timeSinceStartup;
        }

        public static void StopPreviewMode()
        {
            if(!isActive) return;
            
            EditorApplication.update -= EditorTick;
            
            stopWatch.Stop();
            isActive = false;
            
            // restore selection
            EditorSceneManager.sceneOpened += (_, _) =>
            {
                // get last selected gameObject
                if (UniqueID.FindByID(lastSelectedID, out var activeGameObject))
                {
                    if(activeGameObject == null) return;
                    
                    // remove id component and save
                    Object.DestroyImmediate(activeGameObject.GetComponent<UniqueID>());
                    EditorUtility.SetDirty(activeGameObject);
                    EditorSceneManager.SaveScene(activeGameObject.scene);
                    
                    Selection.activeGameObject = activeGameObject;
                }
            };
            
            // discard new changes
            EditorSceneManager.OpenScene(EditorSceneManager.GetActiveScene().path, OpenSceneMode.Single);

            SceneView.duringSceneGui -= OnSceneGUI; 
            
            GC.Collect();
            Debug.Log($"Preview stopped");
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

            GUI.color = Styles.TweenerBoxColor;

            EditorGUI.DrawRect(new Rect(0, 0, 200, 60), Styles.TweenerBoxColor);
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                GUI.color = Color.white;
                GUILayout.Label("AnimFlex Playing...", Styles.Label);
                if (GUILayout.Button("Stop", Styles.Button))
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