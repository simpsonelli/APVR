using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RedTeam19
{ 
    [CustomEditor(typeof(VRScreenEffects))]
    public class VRScreenEffectsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            VRScreenEffects vrsfx = (VRScreenEffects)target;

            GUILayout.Label("Run the Effects");

            if (GUILayout.Button("Screen Flash"))
            {
                vrsfx.ScreenFlash();
            }

            if (GUILayout.Button("Fade Out"))
            {
                vrsfx.FadeOut(); 
            }

            if (GUILayout.Button("Fade In"))
            {
                vrsfx.FadeIn();
            }
        }
    }
}
