using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGameCollection.Games2024.Team06
{
    public class StartMenuManager : MonoBehaviour
    {
        public void GoToPlayScene()
        {
            SceneManager.LoadScene("Team 6 Main Scene");
        }
    }
}