using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AnimFlex.Core;
using AnimFlex.Sequencer;
using UnityEditor;
using UnityEditor.SceneManagement;
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

        public static void StartPreviewMode()
        {
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
            lastTickTime = 0;
            isActive = true;
            Debug.Log("Preview started.");

        }

        private static void EditorTick()
        {
            AnimFlexCore.Instance.Tick((float)stopWatch.Elapsed.TotalSeconds - lastTickTime);
            lastTickTime = (float)stopWatch.Elapsed.TotalSeconds;
        }

        public static void StopPreviewMode()
        {
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

            
            GC.Collect();
            Debug.Log($"Preview stopped");
        }

        public static void PreviewSequence(Sequence sequence)
        {
            if(isActive)
                StopPreviewMode();
            StartPreviewMode();
            sequence.Play();
            sequence.onComplete += StopPreviewMode;
        }
    }
}