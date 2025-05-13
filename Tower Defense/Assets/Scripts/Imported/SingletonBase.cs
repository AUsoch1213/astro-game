using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T: MonoBehaviour
{
    [Header("Singleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;

    public static T Instanse { get; private set; }

    protected virtual void Awake()
    {       
        if (Instanse != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Instanse = this as T;

        if (m_DoNotDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
