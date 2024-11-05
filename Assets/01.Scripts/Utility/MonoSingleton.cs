using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    Debug.LogWarning($"No instance of {typeof(T)}.");
                    return null;
                }
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    _instance.Init();
                }
            }
            return _instance;
        }
    }

    public static bool InstanceIsNull => _instance is null;
    private static bool _isInitialized;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Debug.LogError($"Multiple instances of {GetType()} exist. Delete this object.");
            DestroyImmediate(this);
            return;
        }
        if (!_isInitialized)
        {
            _isInitialized = true;
            _instance.Init();
        }
    }

    public virtual void Init() { }

	private void OnDestroy()
	{
		_instance = null;
	}

	private void OnApplicationQuit()
    {
        _instance = null;
    }
}