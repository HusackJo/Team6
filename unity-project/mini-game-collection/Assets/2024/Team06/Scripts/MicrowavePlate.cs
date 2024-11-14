using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team06
{
    public class MicrowavePlate : MonoBehaviour
    {
        [SerializeField]
        public float speed;

        private void FixedUpdate()
        {
            transform.Rotate(0,speed, 0);
        }
    }
}

