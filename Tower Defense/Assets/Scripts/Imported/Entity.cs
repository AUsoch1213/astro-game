using UnityEngine;
/// <summary>
/// ������� ����� ���� ������������� ������� �������� �� �����.
/// </summary>

namespace Common
{
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// �������� ������� ��� ������������.
        /// </summary>
        [SerializeField]
        private string m_Nicname;
        public string Nicname => m_Nicname;
    }
}

