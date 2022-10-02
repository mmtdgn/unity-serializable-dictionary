using UnityEngine;
using UnityEditor;

namespace MD.SerializableDictionary
{
    [CustomPropertyDrawer(typeof(SDictionaryBase), true)]
    public class SDictionaryDrawer : PropertyDrawer
    {
        private int m_VerticalIndex;
        private float m_Height;
        private float m_DelayedHeight = 0f;

        private GUIStyle m_LabelStyle
        {
            get
            {
                return new GUIStyle()
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter,
                    normal =
                    {
                        textColor = SDictionaryConstants.S_CONTENT_COLOR,
                    }
                };
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            m_Height = (m_VerticalIndex + 2) * SDictionaryConstants.FIELD_HEIGHT + SDictionaryConstants.TOP_OFFSET * 2 + SDictionaryConstants.SPACE * 8f;
            return m_Height + m_DelayedHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();
            position.y += SDictionaryConstants.TOP_OFFSET;
            DrawBackGround(position);
            DrawElementBox(position);
            DrawHeader(position, property);
            DrawLabels(position, property);
            DrawElements(position, property);
            DrawControllerButtons(position, property);
            property.serializedObject.ApplyModifiedProperties();
        }

        private void DrawBackGround(Rect position)
        {
            GUI.backgroundColor = SDictionaryConstants.S_BG_COLOR;
            var rect = new Rect(position.x, position.y, position.width, m_Height - SDictionaryConstants.SPACE);
            GUI.Box(rect, GUIContent.none, EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
        }

        private Rect IndexRect(Rect position, int index)
        {
            return new Rect(position.x + SDictionaryConstants.SPACE, position.y + SDictionaryConstants.FIELD_HEIGHT * 2 + SDictionaryConstants.SPACE * 3
            + (index * SDictionaryConstants.FIELD_HEIGHT), 50 + SDictionaryConstants.SPACE, SDictionaryConstants.FIELD_HEIGHT);
        }

        private void DrawHeader(Rect position, SerializedProperty property)
        {
            GUI.backgroundColor = Color.red;
            var _labelRect = new Rect(position.x + SDictionaryConstants.SPACE, position.y + SDictionaryConstants.TOP_OFFSET, position.width - SDictionaryConstants.SPACE * 2, SDictionaryConstants.FIELD_HEIGHT);
            GUI.Box(_labelRect, GUIContent.none, EditorStyles.helpBox);
            GUI.Label(_labelRect, property.displayName, m_LabelStyle);
            GUI.backgroundColor = Color.white;
        }

        private void DrawLabels(Rect position, SerializedProperty property)
        {
            #region Index
            var _indexRect = new Rect(position.x + SDictionaryConstants.SPACE, position.y + SDictionaryConstants.SPACE + SDictionaryConstants.FIELD_HEIGHT, 50 + SDictionaryConstants.SPACE, SDictionaryConstants.FIELD_HEIGHT);
            GUI.Box(_indexRect, GUIContent.none, EditorStyles.objectField);
            GUI.Label(_indexRect, "Index", m_LabelStyle);
            #endregion

            #region Key
            var _keyRect = new Rect(_indexRect.xMax + SDictionaryConstants.SPACE, position.y + SDictionaryConstants.SPACE + SDictionaryConstants.FIELD_HEIGHT
            , position.width / 2.75f, SDictionaryConstants.FIELD_HEIGHT);
            GUI.Box(_keyRect, GUIContent.none, EditorStyles.objectField);
            GUI.Label(_keyRect, "Key", m_LabelStyle);
            #endregion

            #region Value
            var _valueRect = new Rect(_keyRect.xMax + SDictionaryConstants.SPACE, position.y + SDictionaryConstants.SPACE + SDictionaryConstants.FIELD_HEIGHT
            , position.max.x - _keyRect.xMax - SDictionaryConstants.SPACE * 2, SDictionaryConstants.FIELD_HEIGHT);
            GUI.Box(_valueRect, GUIContent.none, EditorStyles.objectField);
            GUI.Label(_valueRect, "Value", m_LabelStyle);
            #endregion
        }

        private void DrawElementBox(Rect position)
        {
            var rect = new Rect(position.x + SDictionaryConstants.SPACE, position.y + SDictionaryConstants.SPACE * 2 + SDictionaryConstants.FIELD_HEIGHT * 2
            , position.width - SDictionaryConstants.SPACE * 2, m_Height - SDictionaryConstants.SPACE * 5 - SDictionaryConstants.FIELD_HEIGHT * SDictionaryConstants.m_FieldCount);
            GUI.Box(rect, GUIContent.none, EditorStyles.objectField);
        }

        private void DrawElements(Rect position, SerializedProperty property)
        {
            var _dictionary = property.FindPropertyRelative("Dictionary");
            m_VerticalIndex = _dictionary.arraySize;

            for (int i = 0; i < _dictionary.arraySize; i++)
            {
                GUI.backgroundColor = SDictionaryConstants.S_BG_COLOR;
                var _indexBox = new Rect(IndexRect(position, i).x + SDictionaryConstants.SPACE, IndexRect(position, i).y, IndexRect(position, i).width - SDictionaryConstants.SPACE, 20);
                GUI.Box(_indexBox, GUIContent.none, EditorStyles.objectField);
                GUI.Label(IndexRect(position, i), System.String.Format("{0:00}", i), m_LabelStyle);
                GUI.backgroundColor = Color.white;

                var _rect = new Rect(position.x, position.y + SDictionaryConstants.FIELD_HEIGHT * 2 + SDictionaryConstants.SPACE * 3 + (i * SDictionaryConstants.FIELD_HEIGHT), position.width, 20);
                var value = _dictionary.GetArrayElementAtIndex(i);
                EditorGUI.PropertyField(_rect, value, GUIContent.none);
            }
        }


        private void DrawControllerButtons(Rect position, SerializedProperty property)
        {
            GUI.backgroundColor = SDictionaryConstants.S_BG_COLOR;
            GUI.contentColor = SDictionaryConstants.S_CONTENT_COLOR;

            float _buttonWidth = 65f;
            var _dictionary = property.FindPropertyRelative("Dictionary");
            var _addButtonRect = new Rect(position.x + SDictionaryConstants.SPACE, position.y + m_Height - SDictionaryConstants.FIELD_HEIGHT - SDictionaryConstants.SPACE * 2, _buttonWidth, SDictionaryConstants.FIELD_HEIGHT);
            var _removeButtonRect = new Rect(_addButtonRect.xMax + SDictionaryConstants.SPACE / 2f, position.y + m_Height - SDictionaryConstants.FIELD_HEIGHT - SDictionaryConstants.SPACE * 2, _buttonWidth, SDictionaryConstants.FIELD_HEIGHT);
            var _clearButtonRect = new Rect(_removeButtonRect.xMax + SDictionaryConstants.SPACE / 2f, position.y + m_Height - SDictionaryConstants.FIELD_HEIGHT - SDictionaryConstants.SPACE * 2, _buttonWidth, SDictionaryConstants.FIELD_HEIGHT);

            var ResizeCount = property.FindPropertyRelative("ResizeCount");
            void SetIndexCount()
            {
                _dictionary.serializedObject.Update();
                ResizeCount.intValue = _dictionary.arraySize;
                _dictionary.serializedObject.ApplyModifiedProperties();
            }

            GUI.backgroundColor = SDictionaryConstants.S_COLOR_GREEN;
            if (GUI.Button(_addButtonRect, "Add"))
            {
                _dictionary.InsertArrayElementAtIndex(_dictionary.arraySize);

                m_DelayedHeight = SDictionaryConstants.FIELD_HEIGHT;
                EditorApplication.delayCall += () =>
                {
                    m_DelayedHeight = 0;
                    SetIndexCount();
                };
            }

            GUI.backgroundColor = SDictionaryConstants.S_COLOR_RED;
            if (GUI.Button(_removeButtonRect, "Remove"))
            {
                if (_dictionary.arraySize > 0)
                {
                    _dictionary.DeleteArrayElementAtIndex(_dictionary.arraySize - 1);

                    m_DelayedHeight = -SDictionaryConstants.FIELD_HEIGHT;
                    EditorApplication.delayCall += () =>
                    {
                        m_DelayedHeight = 0;
                        SetIndexCount();
                    };
                }
            }

            GUI.backgroundColor = Color.yellow;
            if (GUI.Button(_clearButtonRect, "Clear"))
            {
                _dictionary.ClearArray();
                EditorApplication.delayCall += () =>
                {
                    SetIndexCount();
                };
            }

            var _resizeRect = new Rect(position.xMax - _buttonWidth - 40f, position.y + m_Height - SDictionaryConstants.FIELD_HEIGHT - SDictionaryConstants.SPACE * 2 + 2.5f, 25, 18f);
            var _resizeButtonRect = new Rect(position.xMax - _buttonWidth - SDictionaryConstants.SPACE, position.y + m_Height - SDictionaryConstants.FIELD_HEIGHT - SDictionaryConstants.SPACE * 2
            , _buttonWidth, SDictionaryConstants.FIELD_HEIGHT);

            GUI.backgroundColor = Color.yellow;
            EditorGUI.PropertyField(_resizeRect, ResizeCount, GUIContent.none);

            GUI.backgroundColor = Color.gray;
            if (GUI.Button(_resizeButtonRect, "Set"))
            {
                _dictionary.arraySize = ResizeCount.intValue;
            }

            GUI.backgroundColor = Color.white;
            GUI.contentColor = Color.white;
        }
    }
}