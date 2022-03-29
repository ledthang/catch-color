using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType(typeof(T)) as T;
        }
        return _instance;
    }

    protected abstract void Initialize();

    private void Awake()
    {
        T instance = GetInstance();
        if (instance == null)
        {
            instance = this as T;
        }
        if (instance != (this as T))
        {
            Destroy(gameObject);
            return;
        }

        _instance = instance;
        Initialize();
        //GameObject.DontDestroyOnLoad(gameObject);
    }

    private static T _instance = null;
}