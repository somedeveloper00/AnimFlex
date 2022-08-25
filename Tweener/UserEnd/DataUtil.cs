using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimFlex.Tweener
{
    internal static class DataUtil
    {
        public static bool TryGenerateTweener(GeneratorData data, out Tweener tweener)
        {
            if (data.fromObject == null)
            {
                Debug.LogError($"fromObject was null in data. the tween generation is impossible.");
                tweener = null;
                return false;
            }
            switch (data.tweenerType)
            {
                case GeneratorData.TweenerType.LocalPosition:
                {
                    var target = data.targetVector3;
                    if (data.relative) target += data.fromObject.transform.localPosition;
                    tweener = data.fromObject.transform.AnimLocalPositionTo(target, data.ease, data.duration, data.delay);
                    break;
                }
                
                
                case GeneratorData.TweenerType.Position:
                {
                    var target = data.targetVector3;
                    if (data.relative) target += data.fromObject.transform.position;
                    tweener = data.fromObject.transform.AnimPositionTo(target, data.ease, data.duration, data.delay);
                    break;
                }
                
                
                case GeneratorData.TweenerType.LocalRotation:
                {
                    if (data.useQuaternion)
                    {
                        var target = data.targetQuaternion;
                        if (data.relative)
                        {
                            target = Quaternion.Euler(target.eulerAngles + data.fromObject.transform.localRotation.eulerAngles);
                        }
                        tweener = data.fromObject.transform.AnimLocalRotationTo(target, data.ease, data.duration, data.delay);
                    }
                    else
                    {
                        var target = data.targetVector3;
                        if (data.relative)
                        {
                            target += data.fromObject.transform.localRotation.eulerAngles;
                        }
                        tweener = data.fromObject.transform.AnimLocalRotationTo(target, data.ease, data.duration, data.delay);
                    }
                    break;
                }
                
                
                
                case GeneratorData.TweenerType.Rotation:
                {
                    if (data.useQuaternion)
                    {
                        var target = data.targetQuaternion;
                        if (data.relative)
                        {
                            target = Quaternion.Euler(target.eulerAngles + data.fromObject.transform.localRotation.eulerAngles);
                        }
                        tweener = data.fromObject.transform.AnimLocalRotationTo(target, data.ease, data.duration, data.delay);
                    }
                    else
                    {
                        var target = data.targetVector3;
                        if (data.relative)
                        {
                            target += data.fromObject.transform.localRotation.eulerAngles;
                        }
                        tweener = data.fromObject.transform.AnimLocalRotationTo(target, data.ease, data.duration, data.delay);
                    }
                    break;
                }

                
                
                case GeneratorData.TweenerType.Scale:
                {
                    var target = data.targetVector3;
                    if (data.relative) target += data.fromObject.transform.localScale;
                    tweener = data.fromObject.transform.AnimScaleTo(target, data.ease, data.duration, data.delay);
                    break;
                }
                case GeneratorData.TweenerType.Fade:
                {
                    var target = data.targetFloat;
                    if (data.fromObject.TryGetComponent<CanvasGroup>(out var canvasGroup))
                    {
                        if (data.relative) target += canvasGroup.alpha;
                        tweener = canvasGroup.AnimFadeTo(target, data.ease, data.duration, data.delay);
                    }
                    else if (data.fromObject.TryGetComponent<Renderer>(out var renderer))
                    {
                        if (data.relative) target += renderer.material.color.a;
                        tweener = renderer.AnimFadeTo(target, data.ease, data.duration, data.delay);
                    }
                    else if (data.fromObject.TryGetComponent<Graphic>(out var graphic))
                    {
                        if (data.relative) target += graphic.color.a;
                        tweener = graphic.AnimFadeTo(target, data.ease, data.duration, data.delay);
                    }
                    else if (data.fromObject.TryGetComponent<Material>(out var material))
                    {
                        if (data.relative) target += material.color.a;
                        tweener = material.AnimFadeTo(target, data.ease, data.duration, data.delay);
                    }
                    else
                    {
                        Debug.LogError($"gameObject {data.fromObject} does not have any valid component for Fade tween!");
                        tweener = null;
                        return false;
                    }
                    break;
                }
                case GeneratorData.TweenerType.Color:
                {
                    var target = data.targetColor;
                    if (data.fromObject.TryGetComponent<Renderer>(out var renderer))
                    {
                        if (data.relative) target += renderer.material.color;
                        tweener = renderer.AnimColorTo(target, data.ease, data.duration, data.delay);
                    }
                    else if (data.fromObject.TryGetComponent<Graphic>(out var graphic))
                    {
                        if (data.relative) target += graphic.color;
                        tweener = graphic.AnimColorTo(target, data.ease, data.duration, data.delay);
                    }
                    else if (data.fromObject.TryGetComponent<Material>(out var material))
                    {
                        if (data.relative) target += material.color;
                        tweener = material.AnimColorTo(target, data.ease, data.duration, data.delay);
                    }
                    else
                    {
                        Debug.LogError($"gameObject {data.fromObject} does not have any valid component for Color tween!");
                        tweener = null;
                        return false;
                    }
                    break;
                }
                default:
                    Debug.LogError($"tween type was invalid: {data.tweenerType}");
                    tweener = null;
                    return false;
            }

            // add Unity events
            tweener.onStart += data.onStart.Invoke;
            tweener.onComplete += () => data.onComplete.Invoke();
            tweener.onKill += data.onKill.Invoke;
            tweener.onUpdate += data.onUpdate.Invoke;
            
            return true;
        } 
    }
}