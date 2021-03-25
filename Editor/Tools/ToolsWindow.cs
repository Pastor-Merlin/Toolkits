using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Toolkits.Editor
{
    public class ToolsWindow : EditorWindow
    {
        //第一个参数为路径，第二个参数是否使验证函数，第三个参数优先层级（默认层级为1000）
        [MenuItem("Tools/DebugWindow", false, 0)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ToolsWindow), true, "调试编辑器");
        }

        #region EditorWindow

        protected virtual void OnEnable()
        {
            //TODO 
        }

        protected virtual void OnDisable()
        {
            //TODO 
        }

        protected virtual void OnSelectionChange()
        {
            this.Repaint();
        }

        public virtual void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.normal.background = null;
            style.alignment = TextAnchor.UpperLeft;
            style.normal.textColor = Color.black;
            EditorGUILayout.LabelField("启动设置", style);
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(!Application.isPlaying ? "Play" : "Stop", GUILayout.Width(120)))
                {
                    EditorApplication.isPlaying = !Application.isPlaying;
                    if (!Application.isPlaying)
                    {
                        //TODO  启动准备
                        Debug.LogFormat("<color=#00ff00>{0}</color>", "准备启动。。。");
                    }
                    else
                    {
                        //TODO  关闭准备
                        Debug.LogFormat("<color=#ff0000>{0}</color>", "准备关闭。。。");
                    }
                }

                if (EditorApplication.isPlaying)
                {
                    if (GUILayout.Button("Pause", GUILayout.Width(120)))
                    {
                        EditorApplication.isPaused = !EditorApplication.isPaused;
                    }
                }

                if (GUILayout.Button("Refresh", GUILayout.Width(120)))
                {
                    ClearConsole();
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        #endregion

        #region PUBLIC FUNCTION
     
        public static void ClearConsole()
        {
            Type log = typeof(EditorWindow).Assembly.GetType("UnityEditor.LogEntries");
            var clearMethod = log.GetMethod("Clear");
            clearMethod?.Invoke(null, null);
        }

        #endregion
    }

}


