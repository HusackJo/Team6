using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team06
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header ("Controller Properites")]
        [field: SerializeField, Range(1,2)]
        public int playerID;
        [field: SerializeField]
        public float moveSpeed;
        public float lookAngle { get; private set; }

        [Header("Firing Properties")]
        [SerializeField]
        public GameObject fireBallPrefab;
        [SerializeField]
        public GameObject firePos;
        [SerializeField]
        public float timeBetweenFires;


        //Private properites
        private Vector3 playerInput;
        private Rigidbody rB;
        private int ID => playerID - 1;
        private bool isFiring;
        private float timeSinceLastFire;

        private void Awake()
        {
            rB = GetComponentInChildren<Rigidbody>();
        }

        void Update()
        {
            CheckPlayerInput();
        }

        private void FixedUpdate()
        {
            //Moves & Rotates
            rB.MovePosition(rB.position + playerInput * moveSpeed);
            transform.localEulerAngles = new Vector3 (0, lookAngle * Mathf.Rad2Deg, 0);

            if (FiringLogic())
            {
                Shoot();
            }
        }

        //Gathers inputs to class properties.
        private void CheckPlayerInput()
        {
            // 2 axis inputs
            playerInput.x = ArcadeInput.Players[ID].AxisX;
            playerInput.z = ArcadeInput.Players[ID].AxisY;
            //Rotation
            if (playerInput != Vector3.zero)
            {
                lookAngle = Mathf.Atan2(-playerInput.z, playerInput.x) ;
            }
            playerInput.Normalize();

            //Firing Input
            isFiring = ArcadeInput.Players[ID].Action1.Down;
        }

        //Runs every FixedUpdate. Input is gathered in CheckPlayerInput
        private bool FiringLogic()
        {
            if (timeSinceLastFire <= 0)
            {
                if (isFiring)
                {
                    timeSinceLastFire = timeBetweenFires;
                    return true;
                }
            }
            else
            {
                timeSinceLastFire -= Time.deltaTime;
            }
            return false;
        }

        public void Shoot()
        {
            GameObject newFireBall = Instantiate(fireBallPrefab, firePos.transform.position, firePos.transform.rotation);
            newFireBall.GetComponent<FireBallBehaviors>().assignedPlayer = playerID;
        }
    }
}



