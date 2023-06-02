using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimFlex.Tweening {
    public abstract class AFSelection {
        public Transform transform;

        [Tooltip( "The type of selection.\n" +
                  "**Direct** : The object is directly added to the list of objects to tween.\n\n" +
                  "**Get Children** : The 1st row children of this object will be added to the list of objects to tween.\n\n" +
                  "**Get All Children** : All the children of this object will be added to the list of objects to tween.\n\n" +
                  "**Ignore** : These objects will be ignored.\n\n" +
                  "+ advanced: The phase they'll be respected is Direct, GetChildren and lastly Ignore. And there'll be no repeated " +
                  "objects in the final list." )]
        public SelectionType type = SelectionType.GetChildren;

        public abstract Type GetValueType();

        public static TFrom[] GetSelectedObjects<TFrom>(AFSelection<TFrom>[] selections) where TFrom : Component {
            var r = new HashSet<TFrom>();
            for (var i = 0; i < selections.Length; i++) {
                if (selections[i].type == SelectionType.Direct)
                    r.Add( selections[i].transform.GetComponent<TFrom>() );
            }

            for (var i = 0; i < selections.Length; i++) {
                if (selections[i].type == SelectionType.GetChildren) {
                    for (int childIndex = 0; childIndex < selections[i].transform.childCount; childIndex++) {
                        var child = selections[i].transform.GetChild( childIndex );
                        if (!child.gameObject.activeInHierarchy)
                            continue;
                        if (child.TryGetComponent<TFrom>( out var comp ))
                            r.Add( comp );
                    }
                }
                else if (selections[i].type == SelectionType.GetAllChildren) {
                    foreach (var obj in selections[i].transform.GetComponentsInChildren<TFrom>())
                        if (obj.gameObject.activeInHierarchy)
                            r.Add( obj );
                }
            }

            for (var i = 0; i < selections.Length; i++) {
                if (selections[i].type == SelectionType.Ignore)
                    r.Remove( selections[i].transform.GetComponent<TFrom>() );
            }

            return r.ToArray();
        }

        public enum SelectionType {
            Direct,
            GetChildren,
            GetAllChildren,
            Ignore
        }
    }

    [Serializable]
    public class AFSelection<TFrom> : AFSelection where TFrom : Component {
        public override Type GetValueType() => typeof(TFrom);
    }
}