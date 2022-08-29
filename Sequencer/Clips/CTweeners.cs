using System.ComponentModel;
using AnimFlex.Tweener;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Tweeners/Simple Tweener")]
    public class CTweener : Clip
    {
        public GeneratorData data;
        private Tweener.Tweener _tweener;

        protected override void OnStart()
        {
            if (GeneratorDataUtil.TryGenerateTweener(data, out var tweener))
            {
                tweener.onComplete += End;
            }
            else
            {
                Debug.LogError($"Could not generate tweener. unexpected error happened!");
                End();
            }
        }
    }
}