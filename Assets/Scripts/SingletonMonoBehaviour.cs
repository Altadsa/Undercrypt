using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour
    where T : SingletonMonoBehaviour<T>
{
    private static T _instance;
    private static readonly object padlock = new object();

    public static T Instance
    {
        get
        {
            lock (padlock)
            {
                if (!_instance)
                    _instance = FindObjectOfType<T>();
                return _instance;
            }
        }
    }
}
