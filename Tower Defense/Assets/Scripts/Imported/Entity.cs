using UnityEngine;
/// <summary>
/// Базовый класс всех интерактивных игровых обьектов на сцене.
/// </summary>

namespace Common
{
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Название обьекта для пользователя.
        /// </summary>
        [SerializeField]
        private string m_Nicname;
        public string Nicname => m_Nicname;
    }
}

