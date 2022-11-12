using System;
using AnimFlex.Core;
using AnimFlex.Sequencer;
using AnimFlex.Tweening;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
#if !UNITY_2021_1_OR_NEWER
using UnityEditor.Experimental.SceneManagement;
#endif

namespace AnimFlex.Editor
{
    public static class AFPreviewUtils
    {
        private static float lastTickTime;

        public static bool isActive
        {
            get => EditorPrefs.GetBool("AnimFlex_previewIsActive", false);
            private set => EditorPrefs.SetBool("AnimFlex_previewIsActive", value);
        }

        private static GlobalObjectId lastSelected;
        private static Scene startedScene; // the scene may change in a preview

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

        /// <summary>
        /// trys to start the preview system. returns true if successful
        /// </summary>
        public static bool StartPreviewMode()
        {
	        Profiler.BeginSample("AnimFlex preview start");

            if (EditorApplication.isPlaying)
            {
                Debug.LogError("Can't start preview mode while in play mode!");
                Profiler.EndSample();
                return false;
            }
            if (isActive)
            {
                StopPreviewMode();
                Debug.LogWarning("Preview already in progress: automatically stopped the previous one.");
                Profiler.EndSample();
                return false;
            }
            if (PrefabStageUtility.GetCurrentPrefabStage() != null)
            {
                Debug.LogError("Previewing AnimFlex in prefab mode is not supported. Your other choice is to create an empty sample scene for previewing your assets.");
                Profiler.EndSample();
                return false;
            }
            if (EditorSceneManager.sceneCount > 1)
            {
	            Debug.LogError("Previewing AnimFlex while having multiple scenes opened, is not supported yet.");
	            Profiler.EndSample();
	            return false;
            }

            // save all changes
            while (EditorSceneManager.GetActiveScene().isDirty)
            {
                if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == false) return false;
            }

            // keep track of started scene. because scene may change during preview
            startedScene = EditorSceneManager.GetActiveScene();

            // keep track of selection
            if (Selection.activeGameObject != null)
            {
                lastSelected = GlobalObjectId.GetGlobalObjectIdSlow(Selection.activeGameObject);
            }



            AnimFlexCore.Initialize();
            EditorApplication.update += EditorTick;
            lastTickTime = (float)EditorApplication.timeSinceStartup;
            isActive = true;

            // handling sudden play mode
            EditorApplication.playModeStateChanged += change =>
            {
                if (isActive)
                {
                    if (change == PlayModeStateChange.ExitingEditMode)
                    {
                        EditorApplication.isPlaying = false;
                        Debug.LogError("You shouldn't enter playmode while in preview mode!");
                        StopPreviewMode();
                    }
                }
            };

            // handling inspector editing
            foreach (var component in Object.FindObjectsOfType<Component>())
            {
                var flags = component.hideFlags;
                component.hideFlags |= HideFlags.NotEditable;
            }




            // start scene view menu
            SceneView.duringSceneGui += OnSceneGUI;

            Debug.Log("Preview started.");
            Profiler.EndSample();
            return true;
        }

        private static void EditorTick()
        {
	        Profiler.BeginSample("AnimFlex Preview Tick");

            // ignoring long frames altogether (maybe user changed the window or whatever else)
            if (EditorApplication.timeSinceStartup - lastTickTime < 1f)
            {
                AnimFlexCore.Instance.Tick((float)EditorApplication.timeSinceStartup - lastTickTime);
                SceneView.RepaintAll();
            }
            lastTickTime = (float)EditorApplication.timeSinceStartup;

            Profiler.EndSample();
        }

        public static void StopPreviewMode()
        {
	        Profiler.BeginSample("AnimFlex preview stop");
            if(!isActive) return;

            EditorApplication.update -= EditorTick;

            // restore selection
            EditorSceneManager.sceneOpened += OnEditorSceneManagerOnsceneOpened;
            // close scene view menu
            SceneView.duringSceneGui -= OnSceneGUI;

            // discard new changes & end preview
            EditorApplication.delayCall += () =>
            {
	            isActive = false;
	            EditorSceneManager.OpenScene(startedScene.path, OpenSceneMode.Single);
				GC.Collect();
				Debug.Log("Preview stopped");
            };

            Profiler.EndSample();
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

            GUI.color = AFStyles.BoxColorDarker;

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

            if (StartPreviewMode())
            {
	            sequence.Play();
	            sequence.onComplete += StopPreviewMode;
            }
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

                if (StartPreviewMode())
                {
	                if (generator.TryGenerateTween(out var tweener) == false)
	                {
	                    Debug.LogError("Could not generate the tween");
	                    StopPreviewMode();
	                }
                }
            };
        }
    }
}
