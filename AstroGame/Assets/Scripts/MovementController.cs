using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControleMode
        {
            Keyboard,
            Joystick
        }

        [SerializeField] private SpaceShip m_TargetShip;
        [SerializeField] private VirtualJoystick m_Joystick;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private ControleMode m_ControlMode;

        private void Start()
        {
            if (m_ControlMode == ControleMode.Joystick)
            {
                m_Joystick.gameObject.SetActive(true);

                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
            else
            {
                m_Joystick.gameObject.SetActive(false);

                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
        }

        private void Update()
        {

            if (m_TargetShip == null) return; 

            if (m_ControlMode == ControleMode.Keyboard) ControlKeyboard();

            if (m_ControlMode == ControleMode.Joystick) ControlJoystick();
        }

        private void ControlJoystick()
        {
            var dir = m_Joystick.Value;
            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = -dir.x;
            if (m_MobileFirePrimary.IsHold) m_TargetShip.Fire(TurretMode.Primary);
            if (m_MobileFireSecondary.IsHold) m_TargetShip.Fire(TurretMode.Secondary);
        }

        private void ControlKeyboard()
        {
            float thrust = 0.0f;
            float torque = 0.0f;


            if (Input.GetKey(KeyCode.UpArrow)) thrust = 1.0f;
            if (Input.GetKey(KeyCode.DownArrow)) thrust = -1.0f;
            if (Input.GetKey(KeyCode.LeftArrow)) torque = 1.0f;
            if (Input.GetKey(KeyCode.RightArrow)) torque = -1.0f;
            if (Input.GetKey(KeyCode.E)) m_TargetShip.Fire(TurretMode.Primary);
            if (Input.GetKey(KeyCode.Q)) m_TargetShip.Fire(TurretMode.Secondary);
            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }

    }

}