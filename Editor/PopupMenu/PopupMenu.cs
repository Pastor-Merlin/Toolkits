using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Toolkits.Editor
{
    public class PopupMenu
    {
        [MenuItem("Window/MyHierarchyMenu/RemoveMissingScript")]
        static void RemoveMissingScript1()
        {
            Transform[] transforms = Selection.transforms;
            int nOpCommondAmount = 0;
            foreach (var go in transforms)
            {
                RemoveRecursively(go.gameObject, ref nOpCommondAmount);
            }
            Debug.LogFormat("RemoveMissingScript:{0}", nOpCommondAmount);
        }

        [MenuItem("Window/MyProjectMenu/RemoveMissingScript")]
        static void RemoveMissingScript2()
        {
            var activeObject = Selection.activeObject;
            if (activeObject != null && activeObject is GameObject)
            {
                int nOpCommondAmount = 0;
                RemoveRecursively((activeObject as GameObject), ref nOpCommondAmount);
                Debug.LogFormat("RemoveMissingScript:{0}", nOpCommondAmount);
            }
        }

        #region Unity 

        [InitializeOnLoadMethod]
        private static void StartInitializeOnLoadMethod()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
            EditorApplication.projectWindowItemOnGUI += OnProjectGUI;
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            //Ctrl+右键
            if (Event.current != null &&
                selectionRect.Contains(Event.current.mousePosition) &&
                Event.current.button == 1 && Event.current.type <= EventType.MouseUp &&
                Event.current.control)
            {
                GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

                if (selectedGameObject != null)
                {
                    Vector2 mousePosition = Event.current.mousePosition;
                    EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "Window/MyHierarchyMenu", null);
                    Event.current.Use();
                }
            }
        }

        private static void OnProjectGUI(string guid, Rect selectionRect)
        {
            //Ctrl+右键
            if (Event.current != null &&
                selectionRect.Contains(Event.current.mousePosition) &&
                Event.current.button == 1 && Event.current.type <= EventType.MouseUp &&
                Event.current.control)
            {
                Vector2 mousePosition = Event.current.mousePosition;
                EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "Window/MyProjectMenu", null);
                Event.current.Use();
            }
        }

        #endregion

        public static void RemoveRecursively(GameObject g, ref int nOpCommondAmount)
        {
            int nRemoveNumber = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(g);
            if (nRemoveNumber > 0)
                nOpCommondAmount++;

            foreach (Transform childT in g.transform)
            {
                RemoveRecursively(childT.gameObject, ref nOpCommondAmount);
            }
        }
    }
}