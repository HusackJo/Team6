using MiniGameCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameCollection.Games2024.Team06
{
    public class GameManager : MiniGameBehaviour
    {
        [Header("Managers")]
        public static GameManager instance;
        public MiniGameManager mGM;

        [Header("UI Properties")]
        public GameObject countDownPanel;

        [Header("Game Properties")]


        [Header("Players")]
        public PlayerMovement Player1Ref;
        public PlayerMovement Player2Ref;
        [Header("Players")]
        public Slider Player1SliderRef;
        public Slider Player2SliderRef;
        
        [HideInInspector]
        public PlayerMovement[] PlayerRefs => new PlayerMovement[] { Player1Ref, Player2Ref };
        public Slider[] PlayerSliderRefs => new Slider[] { Player1SliderRef, Player2SliderRef };

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        private void Start()
        {
            mGM.StartGame();
        }
        protected override void OnGameStart()
        {
            foreach (var player in PlayerRefs)
            {
                player.hasGameStarted = true;
            }
            countDownPanel.SetActive(false);
        }
        private void Update()
        {
            foreach (var player in PlayerRefs)
            {
                PlayerSliderRefs[player.playerID-1].value = player.timeSinceLastFire / player.timeBetweenFires;
            }
        }
    }
}

