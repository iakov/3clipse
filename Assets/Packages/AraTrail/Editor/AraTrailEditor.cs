using UnityEditor;


#if UNITY_EDITOR

namespace Ara{
    [CustomEditor(typeof (AraTrail))]
    [CanEditMultipleObjects]
    internal class AraTrailEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject,"m_Script");
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif

