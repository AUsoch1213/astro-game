
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
namespace SpaceShooter
{
    [RequireComponent (typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [SerializeField] private Sprite m_PreviewImage;

        #region Properties
        /// <summary>
        /// Масса корабля
        /// </summary>
        [Header("Ship")]
        [SerializeField] private float m_mass;
        /// <summary>
        /// Сила толкающая вперёд
        /// </summary>
        [SerializeField] private float m_thrust;
        /// <summary>
        /// Вращающая сила
        /// </summary>
        [SerializeField] private float m_mobility;
        /// <summary>
        /// Максимальная линейная скорость
        /// </summary>
        [SerializeField] private float m_maxLinearVelocity;
        /// <summary>
        /// Максимальная вращательная скорость
        /// </summary>
        [SerializeField] private float m_maxAngularVelocity;

        private Rigidbody2D m_Rigidbody;

        public float MaxLinearVelocity => m_maxLinearVelocity;
        public float MaxAngularVelocity => m_maxAngularVelocity;
        public Sprite PreviewImage => m_PreviewImage;

        #endregion
        #region UnityEvent
        protected override void Start()
        {
            base.Start();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Rigidbody.mass = m_mass;

            m_Rigidbody.inertia = 1;

            InitOffensive();

        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }
        #endregion
        /// <summary>
        /// Метод добавления сил кораблю для движения
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigidbody.AddForce(m_thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigidbody.AddForce(-m_Rigidbody.velocity * (m_thrust / m_maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody.AddTorque(m_mobility * TorqueControl * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * (m_mobility / m_maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        }

        #region Public API
        /// <summary>
        /// Контролирование движения от -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// Контролирование вращения от -1.0 до +1.0
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion

        [SerializeField] private Turret[] m_Turrets;
        public void Fire(TurretMode mode)
        {
           
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }

            }
        }

        [SerializeField] int m_MaxEnergy;
        [SerializeField] int m_MaxAmmo;
        [SerializeField] int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);

        }
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            if (m_PrimaryEnergy > m_MaxEnergy)
            {
                m_PrimaryEnergy = m_MaxEnergy;
            }
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
            {
                return true;
            }

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }


            return false;
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
            {
                return true;
            }

            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }


            return false;
        }

        public void AssigneWeapon(TurretProperties properties)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignetLoadout(properties);
            }
        }

 

    }
}

