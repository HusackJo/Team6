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

        private GameManager Manager;
        private int bounces = 5;

        private void Awake()
        {
            rB = GetComponent<Rigidbody>();
            Manager = GameManager.instance;
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
            Debug.Log($"Collided with: {collision.gameObject.tag}");
            //Reflect the object using the normal of the wall collision
            if(collision.gameObject.CompareTag("Wall"))
            {
                Vector3 newDirection = Vector3.Reflect(BulletDirection, collision.GetContact(0).normal);
                BulletDirection = newDirection;
                bounces--;
                if (bounces == 0)
                {
                    Destroy(gameObject);
                }
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                Vector3 newDirection = Vector3.Reflect(BulletDirection, collision.GetContact(0).normal);
                BulletDirection = newDirection;
                
                Destroy(gameObject);
                
                Destroy(collision.gameObject);
            }
            //this logic destroys the fireball if it collides with anything other than the player who fired it. This is to avoid collision Jank when firing.
            else if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<PlayerMovement>().playerID != assignedPlayer)
                {
                    if(assignedPlayer == 1)
                    {
                        Manager.mGM.Winner = MiniGameWinner.Player1;
                    }
                    else if (assignedPlayer == 2)
                    {
                        Manager.mGM.Winner = MiniGameWinner.Player2;
                    }

                    Destroy(collision.gameObject);
                    Destroy(gameObject);

                    Manager.mGM.StopGame();
                }
            }
            else
            {
                //Occurs when bullets hit each other (or anything else not accounted for)
                Destroy(gameObject);
            }
            
        }
    }
}

