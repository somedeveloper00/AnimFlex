using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AnimFlex.Tweening
{
    internal static class EasingUtilities
    {
        private const int Resolution = 200;
        private static readonly Dictionary<int, float[]> CachedEvaluations = new Dictionary<int, float[]>();

        public static float Evaluate(Easing easing, float t)
        {
            if (!CachedEvaluations.ContainsKey(easing.easingIdentifier)) 
                CreateCacheForEasing(easing.easingIdentifier);
            return
                CachedEvaluations[easing.easingIdentifier]
                    [(int)(t * CachedEvaluations[easing.easingIdentifier].Length)];
        }

        public static EasingIdentifierAttribute[] GetOrCreateAllEasingIDs()
        {
            if (_easingIdentifierAttributes.Length == 0)
            {
                var results = new List<EasingIdentifierAttribute>();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var type = assembly.GetType(typeof(EasingFuncs).FullName ?? string.Empty);
                    if(type == null) 
                        continue;
                    
                    var methods = type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
                    foreach (var method in methods)
                    {
                        var attr = (EasingIdentifierAttribute)method
                            .GetCustomAttributes(typeof(EasingIdentifierAttribute), true)
                            .FirstOrDefault();
                        if(attr != null)
                            results.Add(attr);
                    }

                    break;
                }

                _easingIdentifierAttributes = results.ToArray();
            }


            return _easingIdentifierAttributes;
        }

        // cached attributes data. useful for inspector to convert IDs into string for show
        private static EasingIdentifierAttribute[] _easingIdentifierAttributes = Array.Empty<EasingIdentifierAttribute>();
        
        // cached methods. for actual evaluation and to escape Reflection while having an expandable easing workflow
        private static readonly Dictionary<int, Func<float, float>> CachedEasingFunctions = new Dictionary<int, Func<float, float>>();

        private static void CreateCacheForEasing(int easingIdentifier)
        {
            if (CachedEasingFunctions.Count == 0)
            {
                CreateEasingFunctionsCache();
            }
            CachedEvaluations[easingIdentifier] = new float[Resolution];
            var increament = 1f / Resolution;
            var easingMethod = CachedEasingFunctions[easingIdentifier];
            
            for (int i = 0; i < Resolution; i++)
            {
                CachedEvaluations[easingIdentifier][i] = easingMethod(i * increament);
            }
            
            // just to ensure it's complete
            CachedEvaluations[easingIdentifier][Resolution - 1] = 1;
        }

        private static void CreateEasingFunctionsCache()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(typeof(EasingFuncs).FullName ?? string.Empty);
                if(type == null) 
                    continue;
                    
                var methods = type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
                foreach (var method in methods)
                {
                    var attr = (EasingIdentifierAttribute)method
                        .GetCustomAttributes(typeof(EasingIdentifierAttribute), true)
                        .FirstOrDefault();

                    if (attr != null)
                        CachedEasingFunctions.Add(attr.ID,
                            (Func<float, float>)Delegate.CreateDelegate(typeof(Func<float, float>), method));
                }
                return;
            }
        }
    }
}