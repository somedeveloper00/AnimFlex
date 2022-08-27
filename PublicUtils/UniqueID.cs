using System;
using System.Linq;
using UnityEngine;

namespace AnimFlex.Editor
{
    public class UniqueID : MonoBehaviour
    {
        private static int _lastID = 0;
        
        [SerializeField, HideInInspector] private int id;

        private void Awake() => id = ++_lastID;
        
        public int GetID() => id;
        public static bool FindByID(int id, out GameObject gameObject)
        {
            var objs = FindObjectsOfType<UniqueID>();
            if (objs.Length > 0)
            {
                gameObject = objs.First(uid => uid.id == id).gameObject;
                return true;
            }

            gameObject = null;
            return false;
        }
    }
}