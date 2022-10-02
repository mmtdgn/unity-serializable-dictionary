using System.Collections.Generic;
using MD.SerializableDictionary;
using UnityEngine;

public class Demo1 : MonoBehaviour
{
    [SerializeField] private SDictionary<GameObject, Transform> m_ExampleDictionary;
    [SerializeField] private SDictionary<string, int> m_StringIntDictionary;
    private Dictionary<string, int> m_SystemDictionary = new Dictionary<string, int>();

    private void Start()
    {
        m_SystemDictionary.Add("A", 1);
        m_SystemDictionary.Add("B", 2);
        m_SystemDictionary.Add("C", 3);
        
        m_StringIntDictionary = new SDictionary<string, int>(m_SystemDictionary);

        if (m_StringIntDictionary.ContainsKey("A"))
            Debug.Log("Contains Key");
        else
            Debug.Log("Not Contains Key");
    }
}
