using System;
using System.Collections.Generic;
using System.Linq;
using AnimFlex.Clipper.ClipModules;
using UnityEngine;

namespace AnimFlex.Clipper
{
    public sealed partial class ClipSequence : MonoBehaviour
    {
        public bool playOnStart = true;

        [SerializeField] internal ClipNode[] nodes = Array.Empty<ClipNode>();

        private readonly List<(float t, int index)> _delayedNodesInQueue = new List<(float t, int index)>();
        private readonly List<(int id, Action action)> _onUpdates = new List<(int id, Action action)>();
        private int _lastID = -1;

        private void Start()
        {
            if (playOnStart) Play();
        }

        private void Update()
        {
            for (var i = 0; i < _onUpdates.Count; i++) _onUpdates[i].action();

            for (var i = 0; i < _delayedNodesInQueue.Count; i++)
                if (Time.time >= _delayedNodesInQueue[i].t)
                {
                    PlayClip(_delayedNodesInQueue[i].index);
                    _delayedNodesInQueue.RemoveAt(i--);
                }
        }

        private void OnValidate()
        {
            // check for invalid next indices
            foreach (var node in nodes)
                if (node.nextIndices.Any(i => i >= nodes.Length))
                    RemoveExtraNextNodes();
        }

        public void Play()
        {
            if (nodes.Length <= 0) return;
            PlayClip(0);
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

            Action onFinishCallback = delegate { PlayNextClips(index); };
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
                    _delayedNodesInQueue.Add((Time.time + nodes[nextIndex].delay, nextIndex));
            }
        }
    }
}