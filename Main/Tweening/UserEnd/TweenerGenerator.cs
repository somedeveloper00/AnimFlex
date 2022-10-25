using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Component = UnityEngine.Component;

namespace AnimFlex.Tweening
{
    [Serializable]
    public abstract class TweenerGenerator
    {
        internal abstract Type GetFromValueType();
        internal abstract Type GetToValueType();
        internal abstract void Reset(GameObject gameObject);
        internal abstract bool TryGenerateTween(out Tweener tweener);

        #region Data

        public AnimationCurve customCurve;

        [Tooltip("You can choose between a variety of different built-in curves, but if you want to use special curves that you can't find on the list, " +
                 "you can use the Custom curve option (click on the Prebuilt button). \n also, if you want your curve to appear in the next version of AnimFlex so" +
                 " everyone can use it, make sure to contact us about it! we'll be so excited to add new cool curves :p ")]
        public bool useCurve = false;

        [Tooltip("advanced only: ONLY USE QUATERNION IF YOU'RE AN ARTIFICIAL INTELLIGENCE FROM ANOTHER PLANET.\n(yeah, no, seriously, others may not know" +
                 " what it means, so it's better to not use it)")]
        public bool useQuaternion;

        [Tooltip("It adds the end values to the current state that the game object is at the moment.\n so for example if your game object is at (1, 0, 0) and your" +
                 " end value is set to (2, 0, 0), if you use the Relative option, the end point will be at (3, 0, 0). the same goes for other types of tweens as well")]
        public bool relative;

        [Tooltip("if you want your tween to follow a specific game object, you can use the Transform target instead of typing in a constant number")]
        public bool useTargetTransform = false;

        [Tooltip("From: tween goes towards the target value. \n" +
                 "To: tween goes from the target value towards the current state")]
        public bool from = false;

        [Tooltip("The easy function to use for animating the object. you can find more about each one by just trying them :p  \n" +
                 "also if you have a new curve in mind that is not present in the current version of AnimFlex, make sure to contact us and send your function!")]
        public Ease ease;

        [Tooltip("The duration it takes for the animation to finish. \n" +
                 "yes, the animations (a.k.a. Tweeners) are not speed-based, they're time based. if you need speed-based animations, please contact us so we know it's" +
                 " a needed feature.")]
        public float duration = 1;

        [Tooltip("The initial delay for the tween.\n" +
                 "advanced: while in the initial delay, the tweener object acts as if it's tweening and it's on T zero (so if you're for example tweening it's Position, " +
                 "you can't modify the same object's position from other scripts, because the tween is constantly updating it every frame/tick")]
        public float delay = 0.2f;

        [Tooltip("In PingPong mode, the tweener will have a ping-pong ball behaviour. note that the duration will stay the same")]
        public bool pingPong;

        [Tooltip("indicates the number of times the tweener will have to loop until it's completed.")]
        public int loops;

        [Tooltip("the delay in between each loop")]
        public float loopDelay;

        [Tooltip("The events to call when the tween plays.\n" +
                 "advanced: gets called the next frame of a .PlayOrRestart() call")]
        public UnityEvent onStart;

        [Tooltip("The event to call on every frame. \n" +
                 "advanced: it gets called after the onStart, but in the same frame so")]
        public UnityEvent onUpdate;

        [Tooltip("The event to call when the tween ends.\n" +
                 "advanced: it gets called after the onUpdate event, but in the same frame so.\n" +
                 "+ it's not illegal for a tween to start and complete in one frame")]
        public UnityEvent onComplete;

        [Tooltip("The event to call when the tween is killed, whether it's completed or not.\n" +
                 "advanced: it gets called after the onComplete, but in the same frame so")]
        public UnityEvent onKill;

        #endregion

    }

    public abstract class TweenerGenerator<TFrom, TTo> : TweenerGenerator where TFrom : Component
    {
        [Tooltip("The object which this tween applies to")]
        public TFrom fromObject;

        public TTo target;

        protected abstract Tweener GenerateTween(AnimationCurve curve);

        internal override bool TryGenerateTween(out Tweener tweener)
        {
            tweener = null;

            if (fromObject == null)
            {
                Debug.LogError($"fromObject was null. The tween generation is impossible.");
                return false;
            }

            AnimationCurve curve = useCurve ? customCurve : null;

            tweener = GenerateTween(curve);


            // add Unity events
            tweener.onStart += onStart.Invoke;
            tweener.onComplete += () => onComplete.Invoke();
            tweener.onKill += onKill.Invoke;
            tweener.onUpdate += onUpdate.Invoke;
            tweener.@from = @from;
            tweener.loops = loops;
            tweener.loopDelay = loopDelay;
            tweener.pingPong = pingPong;

            return true;
        }

        internal override void Reset(GameObject gameObject)
        {
            fromObject = gameObject.GetComponent<TFrom>();
        }

        internal override Type GetFromValueType() => typeof(TFrom);
        internal override Type GetToValueType() => typeof(TTo);
    }

    // empty class for easier inspector coding :(
    internal abstract class MultiTweenerGenerator : TweenerGenerator { }


    internal abstract class MultiTweenerGenerator<TFrom, TTo> : MultiTweenerGenerator where TFrom : Component
    {

        [Tooltip("The objects that'll determine what to Tween and what to ignore.")]
        public AFSelection<TFrom>[] selections;
        public TTo target;

        [Tooltip("The delay between each tween")]
        public float multiDelay = 0.2f;

        protected abstract Tweener GenerateTween(TFrom fromObject, AnimationCurve curve, float delay);

        /// <summary>
        /// returns the last generated tweener
        /// </summary>
        internal override bool TryGenerateTween(out Tweener tweener)
        {
            tweener = null;

            var forObjects = AFSelection.GetSelectedObjects(selections);

            if (forObjects == null)
            {
                Debug.LogError($"fromObject was null. The tween generation is impossible.");
                return false;
            }

            AnimationCurve curve = useCurve ? customCurve : null;

            for (int i = 0; i < forObjects.Length; i++)
            {
                tweener = GenerateTween(forObjects[i], curve, delay + multiDelay * i);
                tweener.@from = @from;
                tweener.loops = loops;
                tweener.loopDelay = loopDelay;
                tweener.pingPong = pingPong;
            }


            // add Unity events
            if (tweener != null)
            {
	            tweener.onStart += onStart.Invoke;
	            tweener.onComplete += () => onComplete.Invoke();
	            tweener.onKill += onKill.Invoke;
	            tweener.onUpdate += onUpdate.Invoke;
				return true;
            }

			return false;
        }



        internal override void Reset(GameObject gameObject)
        {

        }

        internal override Type GetFromValueType() => typeof(TFrom);
        internal override Type GetToValueType() => typeof(TTo);
    }

    // empty class for easier inspector
    internal abstract class AFSelection
    {
        public enum SelectionType { Direct, GetChildren, GetAllChildren, Ignore }

        public Transform transform;

        [Tooltip("The type of selection.\n" +
                 "**Direct** : The object is directly added to the list of objects to tween.\n\n" +
                 "**Get Children** : The 1st row children of this object will be added to the list of objects to tween.\n\n" +
                 "**Get All Children** : All the children of this object will be added to the list of objects to tween.\n\n" +
                 "**Ignore** : These objects will be ignored from this MultiTweener.\n\n" +
                 "+ advanced: The phase they'll be respected is Direct, GetChildren and lastly Ignore. And there'll be no repeated " +
                 "objects in the final list.")]
        public SelectionType type = SelectionType.GetChildren;

        public abstract Type GetValueType();

        public static TFrom[] GetSelectedObjects<TFrom>(AFSelection<TFrom>[] selections) where TFrom : Component
        {
            var r = new HashSet<TFrom>();
            for (var i = 0; i < selections.Length; i++)
            {
                if (selections[i].type == SelectionType.Direct)
                    r.Add(selections[i].transform.GetComponent<TFrom>());
            }
            for (var i = 0; i < selections.Length; i++)
            {
                if (selections[i].type == SelectionType.GetChildren)
                {
                    for (int childIndex = 0; childIndex < selections[i].transform.childCount; childIndex++)
                    {
	                    var child = selections[i].transform.GetChild(childIndex);
	                    if(!child.gameObject.activeInHierarchy)
		                    continue;
	                    if (child.TryGetComponent<TFrom>(out var comp))
		                    r.Add(comp);
                    }
                }
                else if (selections[i].type == SelectionType.GetAllChildren)
                {
                    foreach (var obj in selections[i].transform.GetComponentsInChildren<TFrom>())
	                    if(obj.gameObject.activeInHierarchy)
							r.Add(obj);
                }
            }

            for (var i = 0; i < selections.Length; i++)
            {
                if (selections[i].type == SelectionType.Ignore)
                    r.Remove(selections[i].transform.GetComponent<TFrom>());
            }

            return r.ToArray();
        }
    }

    [Serializable]
    internal class AFSelection<TFrom> : AFSelection where TFrom : Component
    {
        public override Type GetValueType() => typeof(TFrom);
    }
}
