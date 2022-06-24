using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using AnimFlex.Tweening.Editor;
using UnityEditor;
#endif

namespace AnimFlex.Tweening
{
    public class TweeningUpdater : MonoBehaviour
    {
        private static TweeningUpdater _instance;

        private static TweeningUpdater GetOrCreateInstance()
        {
            if (_instance == null)
                _instance = new GameObject("[animFlexUpdater]").AddComponent<TweeningUpdater>();
            return _instance;
        }

        // creates the instance inside the scene
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnSceneLoad() => GetOrCreateInstance();

        internal static void PlayTween(Tween tween)
        {
            Tweens.Add(tween);
            tween.AddOnEnd(() => Tweens.Remove(tween));
            
#if UNITY_EDITOR
            // reverting the tween on finish
            tween.AddOnEnd(() =>
            {
                tween.Time = tween.delay;
                tween.InternalUpdate();
            });
            // start editor play
            StartEditorPlay();
#endif
        }


        private static readonly List<Tween> Tweens = new List<Tween>();

        private void Update()
        {
            InternalUpdate();
        }

        private void InternalUpdate()
        {
            float deltaTime = Time.deltaTime;
#if UNITY_EDITOR
            deltaTime = (float)(EditorApplication.timeSinceStartup - _lastUpdateTime);
            _lastUpdateTime = (float)EditorApplication.timeSinceStartup;
#endif

            for (int i = 0; i < Tweens.Count; i++)
            {
                Tweens[i].Time += deltaTime;

                if (Tweens[i].IsFinished)
                {
                    Tweens.RemoveAt(i--);
                    continue;
                }
                
                if (Tweens[i].Time < Tweens[i].delay)
                    continue;

                if (Tweens[i].Time >= Tweens[i].delay + Tweens[i].duration)
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        // revert changes
                        Tweens[i].Time = Tweens[i].delay;
                        Tweens[i].InternalUpdate();
                    }
#endif
                    // the end method will remove the element from list as well
                    Tweens[i].InternalEnd();
                    continue;
                }

                if (Tweens[i].ShouldCancel()) 
                    Tweens.RemoveAt(i--);
                else 
                    Tweens[i].InternalUpdate();
            }

#if UNITY_EDITOR
            // check if should stop
            if(Tweens.Count == 0)
                EndEditorPlay();
#endif
        }

#if UNITY_EDITOR
        private static bool _runningInEditor = false;
        private static float _lastUpdateTime;
        
        // editor playable tweens
        private static void StartEditorPlay()
        {
            if(_runningInEditor || Application.isPlaying) return;
            
            var instance = GetOrCreateInstance();
            _runningInEditor = true;
            EditorApplication.update += instance.InternalUpdate;
            _lastUpdateTime = (float)EditorApplication.timeSinceStartup;
        }
        public static void EndEditorPlay()
        {
            if(!_runningInEditor || Application.isPlaying) return;
            
            var instance = GetOrCreateInstance();
            DestroyImmediate(instance.gameObject);
            _runningInEditor = false;
            EditorApplication.update -= instance.InternalUpdate;
            
            // revert any leftover tweens
            foreach (var tween in Tweens)
            {
                tween.Time = tween.delay;
                tween.InternalUpdate();
            }
            
            // remove all tweens
            Tweens.Clear();
        }
#endif
    }
}