using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Static instance of the class
    private static T instance;

    // Property to access the instance
    public static T Instance
    {
        get
        {
            // If instance doesn't exist, find it in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    // Optionally create a new GameObject if no instance is found
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    // Awake is called when the script instance is being loaded
    protected virtual void Awake()
    {
        // If the instance is already set and it's not this instance, destroy this object
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Otherwise, set the instance to this
        instance = this as T;

        // Optionally, persist this object across scene changes
        DontDestroyOnLoad(gameObject);
    }
}
