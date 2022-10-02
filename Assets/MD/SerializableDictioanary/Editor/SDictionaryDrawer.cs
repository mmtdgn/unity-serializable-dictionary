using UnityEngine;
using UnityEditor;

namespace MD.SerializableDictionary
{
    [CustomPropertyDrawer(typeof(SDictionaryElementBase), true)]
    public class SDictionaryElementDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.backgroundColor = SDictionaryConstants.S_BG_COLOR;
            GUI.contentColor = SDictionaryConstants.S_CONTENT_COLOR;

            var RectLeft = new Rect(100, position.y, position.width / 2.75f, position.height);
            var RectRight = new Rect(RectLeft.xMax + 10, position.y, position.max.x - RectLeft.xMax - 30, position.height);

            SerializedProperty _Key = property.FindPropertyRelative("Key");
            SerializedProperty _Value = property.FindPropertyRelative("Val");

            EditorGUI.PropertyField(RectLeft, _Key, GUIContent.none);
            EditorGUI.PropertyField(RectRight, _Value, GUIContent.none);

            GUI.contentColor = Color.white;
            GUI.backgroundColor = Color.white;
        }
    }
}