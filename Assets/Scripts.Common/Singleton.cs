using UnityEngine;

namespace BAV.Common
{
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
						instance = GameObject.FindObjectOfType<T>();
						if (instance == null)
						{
							var name = typeof(T).ToString();
							var go = new GameObject(name);
							DontDestroyOnLoad(go);

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
}