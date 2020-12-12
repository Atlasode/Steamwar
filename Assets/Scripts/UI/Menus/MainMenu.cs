﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Steamwar.UI;

namespace Steamwar.UI.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public static MainMenu instance;
        public GameObject mainButtons;
        public LoadMenu loadMenu;
        public Button loadButton;
        public SectorMenu sectorMenu;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
                return;
            }
        }

        public void Start()
        {
            if (SaveGame.GetGames().Length == 0)
            {
                loadButton.enabled = false;
            }
        }

        public void Update()
        {

        }

        public void DoAction(int id)
        {
            ButtonType type = (ButtonType)id;
            switch (type)
            {
                case ButtonType.CONTINUE:
                    LoadingScreen.instance.Show(SceneManager.LoadSceneAsync(1));
                    break;
                case ButtonType.NEW_GAME:
                    break;
                case ButtonType.LOAD:
                    mainButtons.gameObject.SetActive(false);
                    loadMenu.gameObject.SetActive(true);
                    break;
                case ButtonType.EDITOR:
                    break;
                case ButtonType.SETTINGS:
                    break;
                case ButtonType.EXIT_DESKTOP:
                    Application.Quit();
                    break;
            }
        }
    }
}