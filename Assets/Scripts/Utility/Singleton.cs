using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T _instance;

    private static object _lock = new object(); // 다중 스레드 환경에서의 데이터 경합 방지

    public static bool destroyOnLoad = false;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        if (!Singleton<T>.destroyOnLoad)
                        {
                            DontDestroyOnLoad(singleton);
                        }
                    }
                }
            }
            return _instance;
        }
    }


    protected virtual void OnDestroy()
    {
        if (!Application.isPlaying && Instance != null && Equals(Instance, this))
        {
            _instance = null;
        }
    }
}