# Unity Serializable Dictionary
A serializable dictionary, visualized in inspector.

# Usage
 * Just declare SDictionary field and ready to use!

## Example Decleration
### Demo 1
```C
public class Demo1 : MonoBehaviour
{
    [SerializeField] private SDictionary<GameObject, Transform> m_ExampleDictionary;
    [SerializeField] private SDictionary<string, int> m_StringIntDictionary;
}
```

<img src="/.github/screenshots/ss00.png">

### Demo 2

```C
public enum EnemyType
{
    Rouge,
    Warrior,
    Sorcerer,
}

public class Demo2 : MonoBehaviour
{
    [SerializeField] private SDictionary<EnemyType, Enemy> m_Enemies;
}
```
<img src="/.github/screenshots/ss01.png">
