using System;
using System.Collections.Generic;
using UnityEngine;

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

        internal static void AddTween(Tween tween) => Tweens.Add(tween);


        private static readonly List<Tween> Tweens = new List<Tween>();

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < Tweens.Count; i++)
            {
                Tweens[i].Time += deltaTime;

                if (Tweens[i].Time < Tweens[i].delay)
                    continue;

                if (Tweens[i].Time >= Tweens[i].delay + Tweens[i].duration)
                {
                    Tweens[i].InternalEnd();
                    Tweens.RemoveAt(i--);
                    continue;
                }

                if (Tweens[i].ShouldCancel()) 
                    Tweens.RemoveAt(i--);
                else 
                    Tweens[i].InternalUpdate();
            }
        }
    }
}