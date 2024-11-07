using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team06
{
    public class FireBallBehaviors : MonoBehaviour
    {
        public float shotSpeed;

        //should be attached to prefab
        private Rigidbody rB;
        public int assignedPlayer;

        private Vector3 BulletDirection;
        private Vector3 BulletVelocity;

        private void Awake()
        {
            rB = GetComponent<Rigidbody>();
            BulletDirection = transform.right;
            BulletVelocity = BulletDirection * shotSpeed;
            
        }
        private void Update()
        {
            Move();
        }

        /// <summary>
        /// Move the bullet based on its current direction and max speed.
        /// </summary>
        private void Move()
        {
            //Only change velocity if it has changed (hit a wall)
            if(rB.velocity != BulletDirection * shotSpeed)
            {
                rB.velocity = BulletDirection * shotSpeed;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            //Reflect the object using the normal of the wall collision
            if(collision.gameObject.CompareTag("Wall"))
            {
                Vector3 newDirection = Vector3.Reflect(BulletDirection, collision.GetContact(0).normal);
                BulletDirection = newDirection;
                
            }
            //this logic destroys the fireball if it collides with anything other than the player who fired it. This is to avoid collision Jank when firing.
            else if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<PlayerMovement>().playerID != assignedPlayer)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
}

