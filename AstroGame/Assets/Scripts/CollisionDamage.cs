using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamage : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";
        

        [SerializeField] private float damage;

        [SerializeField] private float Speed;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            var destructible = transform.root.GetComponent<Destructible>();
            
           
            if (destructible != null)
            {
                destructible.ApplyDamage((int)damage + (int)( Speed + collision.relativeVelocity.magnitude));
            }
        }
    }
}

