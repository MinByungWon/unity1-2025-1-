using UnityEngine;
/// <summary>
/// 유명한 디자인패턴 중 하나인 SingletonInstance 
/// 직접 찾아보고 사용해 보는 것을 추천한다.
/// 해당 클래스는 제네릭 클래스로 정의 하여 각 Manager에서 상속받아 사용가능하다.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();    

    public static T Instance { 
        get
        {
            if (_instance == null)
            {
                lock (_lock) 
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        // GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                        //_instance = obj.GetComponent<T>();
                        _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }
            }
            return _instance; 
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
