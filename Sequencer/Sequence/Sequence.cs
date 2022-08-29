using System;
using System.Collections.Generic;
using System.Linq;
using AnimFlex.Sequencer.ClipModules;
using UnityEngine;

namespace AnimFlex.Sequencer
{
    [Flags]
    internal enum SequenceFlags
    {
        Initialized = 1 << 0, // for OnPlay event
        Paused = 1 << 1,
        Deleting = 1 << 2
    }
    [Serializable]
    public sealed partial class Sequence
    {
        [SerializeField] internal ClipNode[] nodes = Array.Empty<ClipNode>();

        public event Action onPlay = delegate { };
        public event Action onKill = delegate { };
        public event Action onComplete = delegate { };

        internal void OnPlay() => onPlay();
        internal void OnKill() => onKill();
        internal void OnComplete() => onComplete();
        
        internal SequenceFlags flags;

        private readonly List<(float t, int index)> _delayedNodesInQueue = new();
        private readonly List<(int id, Action action)> _onUpdates = new();
        private int _lastID = -1;
        private float time = 0;

        private int activeNodes = 0;
        
        internal void Kill() => flags |= SequenceFlags.Deleting;

        public void Play()
        {
            SequenceController.Instance.AddSequence(this);
            if (nodes.Length <= 0) return;
            PlayClip(0);
        }

        internal void Tick(float deltaTime)
        {
            time += deltaTime;
            
            for (var i = 0; i < _onUpdates.Count; i++) _onUpdates[i].action();

            for (var i = 0; i < _delayedNodesInQueue.Count; i++)
                if (time >= _delayedNodesInQueue[i].t)
                {
                    PlayClip(_delayedNodesInQueue[i].index);
                    _delayedNodesInQueue.RemoveAt(i--);
                }

            if (activeNodes == 0 && _delayedNodesInQueue.Count == 0)
            {
                onComplete();
            }
        }

        internal void EditorValidate()
        {
            // check for invalid next indices
            foreach (var node in nodes)
                if (node.nextIndices.Any(i => i >= nodes.Length))
                    RemoveExtraNextNodes();
            foreach (var node in nodes)
            {
                node.OnValidate();
            }
        }


        private void RemoveExtraNextNodes()
        {
            for (var i = 0; i < nodes.Length; i++)
            for (var j = 0; j < nodes[i].nextIndices.Length; j++)
            {
                if (nodes[i].nextIndices[j] < nodes.Length) continue;

                Debug.LogError("" +
                               $"Node {i} had an invalid next node index {nodes[i].nextIndices[j]}. " +
                               "It will be removed.");
                var listTmp = nodes[i].nextIndices.ToList();
                listTmp.RemoveAt(j--);
                nodes[i].nextIndices = listTmp.ToArray();
            }
        }

        private void PlayClip(int index)
        {
            var id = ++_lastID;

            Action onFinishCallback = delegate
            {
                activeNodes--;
                PlayNextClips(index);
            };
            Action onUpdateCallback = null; 

            // check for update module
            if (nodes[index].clip is IHasUpdate)
            {
                var hasUpdateMod = nodes[index].clip as IHasUpdate;
                if (hasUpdateMod == null) goto after_IHasUpdateCheck;

                onUpdateCallback += hasUpdateMod.Update;
            }

            after_IHasUpdateCheck:


            // check for end-when module
            if (nodes[index].clip is IEndWhen)
            {
                var endWhen = nodes[index].clip as IEndWhen;
                if (endWhen == null) goto after_IEndWhen_check;

                onUpdateCallback += () =>
                {
                    if (endWhen.CanEnd())
                        nodes[index].clip.End();
                };
            }

            after_IEndWhen_check:

            // assign & play
            if (onUpdateCallback != null)
            {
                _onUpdates.Add((id, onUpdateCallback));
                onFinishCallback += () => RemoveClipFromOnUpdates(id);
            }

            activeNodes++;
            nodes[index].Play(onFinishCallback);
        }

        private void RemoveClipFromOnUpdates(int id)
        {
            for (var i = 0; i < _onUpdates.Count; i++)
            {
                if (_onUpdates[i].id != id) continue;
                _onUpdates.RemoveAt(i);
                return;
            }
        }

        private void PlayNextClips(int index)
        {
            if (nodes[index].playNextAfterFinish)
            {
                if (index + 1 >= nodes.Length) return;
                SetupNodePlay(index + 1);
                return;
            }

            for (var i = 0; i < nodes[index].nextIndices.Length; i++)
                SetupNodePlay(nodes[index].nextIndices[i]);

            void SetupNodePlay(int nextIndex)
            {
                if (nodes[nextIndex].delay <= 0)
                    PlayClip(nextIndex);
                else
                    _delayedNodesInQueue.Add((time + nodes[nextIndex].delay, nextIndex));
            }
        }
    }
}