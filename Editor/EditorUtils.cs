using System;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    public static class EditorUtils
    {
        private const string EDITOR_RESOURCES_INDEXER_NAME = "AnimFlexEditorResources.ind";

        
        /// <summary>
        /// returns the path relative to the indexer file located at the root of the plugin 
        /// </summary>
        public static string GetPathRelative(string path)
        {
            string r = "Assets/";
            
            // find the indexer file
            foreach (var fpath in Directory.EnumerateFiles(Application.dataPath, "**", SearchOption.AllDirectories))
            {
                var file = new FileInfo(fpath);
                if (file.Name == EDITOR_RESOURCES_INDEXER_NAME)
                {
                    var root_dir = Path.GetRelativePath(Application.dataPath, file.Directory.FullName);
                    r = "Assets/" + root_dir.Replace("\\", "/") + "/" + path.Replace("\\", "/");
                    return r;
                }
            }

            throw new FileNotFoundException(
                $"The indexer file not found: " +
                $"create a file named AnimFlexEditor.ind at the root of the editor resources of AnimFlex.");
        }
    }
}