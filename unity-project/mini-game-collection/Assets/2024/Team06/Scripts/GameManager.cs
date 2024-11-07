using MiniGameCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team06
{
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        public static GameManager instance;
        public MiniGameManager mGM;

        [Header("Game Properties")]


        [Header("Players")]
        public PlayerMovement Player1Ref;
        public PlayerMovement Player2Ref;
        [HideInInspector]
        public PlayerMovement[] PlayerRefs => new PlayerMovement[] {Player1Ref, Player2Ref};

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        private void Start()
        {
            mGM.StartGame();
        }
    }
}

