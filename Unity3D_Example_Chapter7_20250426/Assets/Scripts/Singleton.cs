using UnityEngine;
/// <summary>
/// ������ ���������� �� �ϳ��� SingletonInstance 
/// ���� ã�ƺ��� ����� ���� ���� ��õ�Ѵ�.
/// �ش� Ŭ������ ���׸� Ŭ������ ���� �Ͽ� �� Manager���� ��ӹ޾� ��밡���ϴ�.
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
