using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;
    protected virtual void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(transform.root);
            instance = this as T;
        }
        else
        {
            if (instance != null)
                Destroy(this.gameObject);
        }
    }
}
