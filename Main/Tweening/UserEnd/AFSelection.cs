using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimFlex.Tweening {
    public abstract class AFSelection {
        public Transform transform;

        [Tooltip( "The type of selection.\n" +
                  "**Direct** : The object is selected.\n\n" +
                  "**Get Children** : 1st row children of the object will be selected.\n\n" +
                  "**Get All Children** : All children of object will be selected.\n\n" +
                  "**Ignore Direct** : The object will be ignored.\n\n" +
                  "**Ignore Children** : 1st row children of the object will be selected\n\n" +
                  "**Ignore All Children** : All children of the object will be selected\n\n" +
                  "\n" +
                  "*(advanced: Selections will be executed from top to bottom. and no repeated object will be selected.)*\n"
                  )]
        public SelectionType type = SelectionType.GetChildren;

        public abstract Type GetValueType();

        public static TFrom[] GetSelectedObjects<TFrom>(AFSelection<TFrom>[] selections) where TFrom : Component {
            var r = new HashSet<TFrom>();
            
            for (var i = 0; i < selections.Length; i++) {
                switch (selections[i].type) {
                    case SelectionType.Direct:
                        r.Add( selections[i].transform.GetComponent<TFrom>() );
                        break;
                    case SelectionType.GetChildren: {
                        for (int childIndex = 0; childIndex < selections[i].transform.childCount; childIndex++) {
                            var child = selections[i].transform.GetChild( childIndex );
                            if (!child.gameObject.activeInHierarchy)
                                continue;
                            if (child.TryGetComponent<TFrom>( out var comp ))
                                r.Add( comp );
                        }

                        break;
                    }
                    case SelectionType.GetAllChildren: {
                        foreach (var obj in selections[i].transform.GetComponentsInChildren<TFrom>())
                            if (obj.gameObject.activeInHierarchy)
                                r.Add( obj );
                        break;
                    }
                    case SelectionType.IgnoreDirect:
                        r.Remove( selections[i].transform.GetComponent<TFrom>() );
                        break;
                    case SelectionType.IgnoreChildren: {
                        for (int childIndex = 0; childIndex < selections[i].transform.childCount; childIndex++) {
                            var child = selections[i].transform.GetChild( childIndex );
                            if (!child.gameObject.activeInHierarchy)
                                continue;
                            if (child.TryGetComponent<TFrom>( out var comp ))
                                r.Add( comp );
                        }

                        break;
                    }
                    case SelectionType.IgnoreAllChildren: {
                        foreach (var obj in selections[i].transform.GetComponentsInChildren<TFrom>())
                            if (obj.gameObject.activeInHierarchy)
                                r.Add( obj );
                        break;
                    }
                }
            }

            return r.ToArray();
        }

        public enum SelectionType {
            Direct,
            GetChildren,
            GetAllChildren,
            IgnoreDirect,
            IgnoreChildren,
            IgnoreAllChildren
        }
    }

    [Serializable]
    public class AFSelection<TFrom> : AFSelection where TFrom : Component {
        public override Type GetValueType() => typeof(TFrom);
    }
}