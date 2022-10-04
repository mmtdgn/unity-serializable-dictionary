using UnityEngine;
using MD.SerializableDictionary;
using System.Collections;

public enum EnemyType
{
    Rouge,
    Warrior,
    Sorcerer,
}

public class Demo2 : MonoBehaviour
{
    [SerializeField] private SDictionary<EnemyType, Enemy> m_Enemies;

    private IEnumerator Start()
    {
        DebugLogger("Demo2 Start()!");
        yield return new WaitForSeconds(1f);

        foreach (var I in m_Enemies.Keys)
        {
            Instantiate(m_Enemies.GetValue(I));
            DebugLogger($"{m_Enemies.GetValue(I).name} object spawned with `{I}` key!");
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    private void DebugLogger(string message)
    {
        Debug.Log(message, this);
    }
}
