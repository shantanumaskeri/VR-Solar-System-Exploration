using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpatialStories
{
    [CustomEditor(typeof(SA_RotateBehaviour))]
    public class SA_RotateBehaviourEditor : S_AbstractActionEditor
    {
        private SA_RotateBehaviour m_rotateBehaviour;

        public override void OnEnable()
        {
            base.OnEnable();
            m_rotateBehaviour = (SA_RotateBehaviour)target;
        }

        public override void OnEditorUI()
        {
            base.OnEditorUI();
            DisplayRotationObjects();
        }

        protected override void ShowAllOptions()
        {
            CustomDisplayDelay();
        }

        public override void OnRuntimeUI()
        {
            base.OnEditorUI();
            DisplayRotationObjects();
        }

        private void DisplayRotationObjects()
        {
            S_EditorUtils.DrawSectionTitle("Object(s) to rotate", FontStyle.Normal);

            if (m_rotateBehaviour.RotateBehaviour.Count == 0)
            {
                m_rotateBehaviour.RotateBehaviour.Add(null);
            }

            // TARGET LIST
            for (int i = 0; i < m_rotateBehaviour.RotateBehaviour.Count; i++)
            {
                EditorGUILayout.BeginVertical();
                S_RotateBehaviour behaviour = m_rotateBehaviour.RotateBehaviour[i];
                if (behaviour.TargetsObject == null)
                {
                    behaviour.TargetsObject = new List<GameObject>();
                }
                S_Utils.DisplayGameObjectList(behaviour.TargetsObject, 200, true);
                EditorGUILayout.Space();
                behaviour.TargetRotation = EditorGUILayout.Vector3Field("Rotation angle (s)", behaviour.TargetRotation);
                EditorGUILayout.Space();
                behaviour.Duration = EditorGUILayout.FloatField("Time[s]", behaviour.Duration);
                behaviour.BackAndForth = EditorGUILayout.ToggleLeft("Back&Forth", behaviour.BackAndForth);
                behaviour.Loop = EditorGUILayout.ToggleLeft("Loop", behaviour.Loop);

                if (m_rotateBehaviour.RotateBehaviour.Count > 1)
                {
                    if (GUILayout.Button("-", GUILayout.MaxWidth(150)))
                    {
                        m_rotateBehaviour.RotateBehaviour.RemoveAt(i);
                    }
                }
                EditorGUILayout.EndVertical();
            }

            // "ADD" BUTTON
            EditorGUILayout.Space();
            if (GUILayout.Button("+"))
            {
                m_rotateBehaviour.RotateBehaviour.Add(null);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("-"))
                {
                    m_rotateBehaviour.RotateBehaviour.RemoveAt(m_rotateBehaviour.RotateBehaviour.Count - 1);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}