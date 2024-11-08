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
        public bool hasGameStarted = false;

        //Private properites
        private GameManager gameManager;
        private Vector3 playerInput;
        private Rigidbody rB;
        private int arcadeInputID => playerID - 1;
        private bool isFiring, secondInput;
        private float timeSinceLastFire;

        private void Awake()
        {
            rB = GetComponentInChildren<Rigidbody>();
            gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
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
            if (secondInput)
            {
                /* Testing if mGM works. Until then this button does nothing :3
                if (playerID == 1)
                {
                    gameManager.mGM.Winner = MiniGameWinner.Player1;
                }
                if (playerID == 2)
                {
                    gameManager.mGM.Winner = MiniGameWinner.Player2;
                }
                */
            }
        }

        //Gathers inputs to class properties.
        private void CheckPlayerInput()
        {
            // 2 axis inputs
            playerInput.x = ArcadeInput.Players[arcadeInputID].AxisX;
            playerInput.z = ArcadeInput.Players[arcadeInputID].AxisY;
            //Rotation
            if (playerInput != Vector3.zero)
            {
                lookAngle = Mathf.Atan2(-playerInput.z, playerInput.x) ;
            }
            playerInput.Normalize();

            //Firing Input
            isFiring = ArcadeInput.Players[arcadeInputID].Action1.Down;
            secondInput = ArcadeInput.Players[arcadeInputID].Action2.Down;
        }

        //Runs every FixedUpdate. Input is gathered in CheckPlayerInput
        private bool FiringLogic()
        {
            if (timeSinceLastFire <= 0)
            {
                if (isFiring && hasGameStarted)
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

        //Shoots fireball, Assigns currect player value to fireball
        public void Shoot()
        {
            GameObject newFireBall = Instantiate(fireBallPrefab, firePos.transform.position, firePos.transform.rotation);
            newFireBall.GetComponent<FireBallBehaviors>().assignedPlayer = playerID;
        }
    }
}



