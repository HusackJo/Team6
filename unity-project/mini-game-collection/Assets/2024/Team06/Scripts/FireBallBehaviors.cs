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

        private void Awake()
        {
            rB = GetComponent<Rigidbody>();
            rB.AddForce(transform.right * shotSpeed * 2.5f);
        }
        private void Update()
        {
            //resets shot speed if below/equal to target velocity
            if (rB.velocity.magnitude <= shotSpeed)
            {
                rB.velocity = transform.right * shotSpeed;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            //we can do Bouncing here!

            //this logic destroys the fireball if it collides with anything other than the player who fired it. This is to avoid collision Jank when firing.
            if (collision.transform.CompareTag("Player"))
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

