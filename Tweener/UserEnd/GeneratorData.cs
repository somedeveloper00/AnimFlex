using System;
using UnityEngine;
using UnityEngine.Events;

namespace AnimFlex.Tweener
{
    [Serializable]
    public class GeneratorData
    {
        [Tooltip("The object which this tween applies to")]
        public GameObject fromObject;
        
        [Tooltip("The type of tween. for example, if you choose Position, you'll be modifying the rest of the options in a way that'll animate the Position" +
                 ". \n\n Advanced note: the way it will detect which Component to modify is totally automatic, And it'll Get them when the tween plays." +
                 " so if you want to add a component, say a MeshRenderer, and then play some tween on that, make sure to add the component first. (because the tween generation is not " +
                 "frame scheduled; it happens right away) ")]
        public TweenerType tweenerType;
        
        public Vector3 targetVector3;
        public Vector2 targetVector2;
        public float targetFloat;
        public Color targetColor;
        public Transform targetTransform;
        public Quaternion targetQuaternion;
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
        public float duration;
        
        [Tooltip("The initial delay for the tween.\n" +
                 "advanced: while in the initial delay, the tweener object acts as if it's tweening and it's on T zero (so if you're for example tweening it's Position, " +
                 "you can't modify the same object's position from other scripts, because the tween is constantly updating it every frame/tick")]
        public float delay;

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

        public enum TweenerType
        {
            LocalPosition,
            Position,
            LocalRotation,
            Rotation,
            Scale,
            Fade,
            Color
        }
    }
}