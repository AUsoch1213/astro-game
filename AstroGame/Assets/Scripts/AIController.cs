using SpaceShooter;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIVariants
        {
            Null,
            Patrol

        }
        [SerializeField] private AIVariants m_AIVariants;

        [SerializeField] private AIPointPatrol m_PatrolPoint;
        [SerializeField] private Projectile m_Projectile;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_LinearSpeed;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_AngularSpeed;

        [SerializeField] private float m_TimerMoveToPoint;

        [SerializeField] private float m_TimerSwapTarget;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRayLength;

        private SpaceShip m_Ship;
        private Vector3 m_MoveTarget;
        private Destructible m_Target;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_TargetFocusTimer;
        private Timer m_FireDelayTimer;

        private void Start()
        {
            InitTimers();
            m_Ship = GetComponent<SpaceShip>();
        }
        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIVariants == AIVariants.Null)
            {

            }
            if (m_AIVariants == AIVariants.Patrol)
            {
                UpdateBehaviorPatrol();
            }
        }
        private void UpdateBehaviorPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }
        private void ActionFindNewMovePosition()
        {
            if (m_AIVariants == AIVariants.Patrol)
            {
                if (m_Target != null)
                {
                    m_MoveTarget = m_Target.transform.position + m_Target.transform.up - transform.position + m_Target.transform.forward;
                }
                else
                {
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
                                m_MoveTarget = newPoint;
                                m_RandomizeDirectionTimer.Start(m_TimerMoveToPoint);
                            }
                        }
                        else
                        {
                            m_MoveTarget = m_PatrolPoint.transform.position;
                        }
                    }

                }

            }
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MoveTarget = transform.position + transform.right * 100.0f;
            }
        }
        private void ActionControlShip()
        {
            m_Ship.ThrustControl = m_LinearSpeed;

            m_Ship.TorqueControl = ComputeAliginTorqueNormalized(m_MoveTarget, m_Ship.transform) * m_AngularSpeed;
        }

        private const float MAX_ANGLE = 24.0f;

        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {

            if (m_TargetFocusTimer.IsFinished == true)
            {
                m_Target = m_FindNearestDestTarget();

            }

        }
        private Destructible m_FindNearestDestTarget()
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;
            foreach (var v in Destructible.AllDestructible)
            {
                if (v.GetComponent<SpaceShip>() == m_Ship) continue;
                if (v.TeamId == m_Ship.TeamId) continue;
                if (v.TeamId == Destructible.TeamIdNeutral) continue;
                float dist = Vector2.Distance(m_Ship.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }
        private void ActionFire()
        {
            if (m_Target != null)
            {
                if (m_FireDelayTimer.IsFinished == true)
                {
                    m_Ship.Fire(TurretMode.Primary);
                    m_FireDelayTimer.Start(m_ShootDelay);

                }
            }
        }

        #region Timers
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_TimerMoveToPoint);
            m_TargetFocusTimer = new Timer(m_TimerSwapTarget);
            m_FireDelayTimer = new Timer(m_ShootDelay);
        }
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_TargetFocusTimer.RemoveTime(Time.deltaTime);
            m_FireDelayTimer.RemoveTime(Time.deltaTime);
        }
        private void SetPatrolBehavior(AIPointPatrol point)
        {
            m_AIVariants = AIVariants.Patrol;
            m_PatrolPoint = point;
        }


        #endregion
    }
}

