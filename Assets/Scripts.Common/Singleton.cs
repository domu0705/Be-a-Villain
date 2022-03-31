using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Properties
    public static T Instance
    {
        get
        {
            if (isApplicationQuitting)
                return null;

            lock (lockObj)
            {
                if (instance == null)
                {
                    Debug.Log("싱글톤만듦"+ typeof(T).ToString());
                    instance = GameObject.FindObjectOfType<T>();
                    if (instance == null)
                    {
                        var name = typeof(T).ToString();
                        var go = new GameObject(name);
                        DontDestroyOnLoad(go.gameObject);//씬이 달라져도 안없어짐

                        instance = go.AddComponent<T>();

                        Debug.Log(string.Format("[{0}] Singleton created", name));
                    }

                }
            }
            return instance;
        }
    }



    // Fields
    protected static T instance = null;
    private static bool isApplicationQuitting = false;
    private static object lockObj = new Object();



    // Unity Messages
    protected virtual void OnApplicationQuit()
    {
        isApplicationQuitting = true;
        instance = null;
    }

}
