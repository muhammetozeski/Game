using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestScripts
{
    public class NoGameViewAtStart : MonoBehaviour
    {
        public bool KeepSceneViewActive;
        private void Awake()
        {
            if (this.KeepSceneViewActive && Application.isEditor)
            {
                UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
            }
        }
    }
}