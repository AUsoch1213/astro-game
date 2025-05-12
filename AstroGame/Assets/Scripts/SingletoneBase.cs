using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class SingletoneBase<T> : MonoBehaviour  where T : MonoBehaviour
{

    [Header("Singletone")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;

    public static T Inctance {  get; private set; }

    protected virtual void Awake()
    {
        if (Inctance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Inctance = this as T;

        if (m_DoNotDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
