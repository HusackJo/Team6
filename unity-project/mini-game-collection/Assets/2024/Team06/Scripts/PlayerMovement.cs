using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team06
{
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField, Range(1,2)]
        public int playerID;

        [field: SerializeField]
        public float moveSpeed;

        public float lookAngle { get; private set; }

        //Private properites
        private Vector3 playerInput;
        private Rigidbody rB;
        private int ID => playerID - 1;

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
            rB.MovePosition(rB.position + playerInput * moveSpeed);
            transform.localEulerAngles = new Vector3 (0, lookAngle, 0);
        }

        private void CheckPlayerInput()
        {
            // 2 axis inputs
            playerInput.x = ArcadeInput.Players[ID].AxisX;
            playerInput.z = ArcadeInput.Players[ID].AxisY;
            //Rotation
            if (playerInput != Vector3.zero)
            {
                lookAngle = Mathf.Atan2(-playerInput.z, playerInput.x) * Mathf.Rad2Deg;
            }

            playerInput.Normalize();

            
        }
    }
}


