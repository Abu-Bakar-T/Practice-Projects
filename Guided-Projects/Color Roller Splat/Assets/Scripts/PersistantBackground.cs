using UnityEngine;

public class PersistantBackground : MonoBehaviour
{
    public static PersistantBackground instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
}
