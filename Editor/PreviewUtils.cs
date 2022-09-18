﻿using System;
using AnimFlex.Core;
using AnimFlex.Tweener;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#if !UNITY_2021_1_OR_NEWER
using UnityEditor.Experimental.SceneManagement;
#endif
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

        private static GlobalObjectId lastSelected;

        // event never used
#pragma warning disable CS0067
        private static event Action onEnd;
#pragma warning restore CS0067

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

            if (PrefabStageUtility.GetCurrentPrefabStage() != null)
            {
                Debug.LogError($"Previewing AnimFlex in prefab mode is not supported. Your other choice is to create an empty sample scene for previewing your assets.");
                return;
            }

            // save all changes
            while (EditorSceneManager.GetActiveScene().isDirty)
            {
                if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == false) return;
            }

            // keep track of selection
            if (Selection.activeGameObject != null)
            {
                lastSelected = GlobalObjectId.GetGlobalObjectIdSlow(Selection.activeGameObject);
            }

            AnimFlexCore.Initialize();
            EditorApplication.update += EditorTick;
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
            foreach (var component in Object.FindObjectsOfType<Component>())
            {
                var flags = component.hideFlags;
                // onEnd += () => component.hideFlags = flags;
                component.hideFlags |= HideFlags.NotEditable;
            }

            // marking all scenes dirty for later revertion
            EditorSceneManager.MarkAllScenesDirty();


            // start scene view menu
            SceneView.duringSceneGui += OnSceneGUI;
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
            // close scene view menu
            SceneView.duringSceneGui -= OnSceneGUI;

            // discard new changes
            AFEditorUtils.ReloadUnsavedDirtyScene();

            GC.Collect();
            isActive = false;
            Debug.Log($"Preview stopped");
        }

        private static void OnEditorSceneManagerOnsceneOpened(Scene scene, OpenSceneMode openSceneMode)
        {
            // get last selected gameObject
            var selectedGO = GlobalObjectId.GlobalObjectIdentifierToObjectSlow(lastSelected);
            if(selectedGO != null)
                Selection.activeGameObject = (GameObject)selectedGO;

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

        public static void PreviewTweener(TweenerGenerator generator)
        {
            EditorApplication.delayCall += () =>
            {
                if (isActive)
                {
                    StopPreviewMode();
                    return;
                }

                StartPreviewMode();
                if (generator.TryGenerateTween(out var tweener) == false)
                {
                    Debug.LogError("Could not generate the tween");
                    StopPreviewMode();
                }
            };
        }
    }
}
