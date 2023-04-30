using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuActivator : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SumaryScreen sumaryScreen;

    private void Update()
    {
        OnPauseMenu();
    }
    private void OnPauseMenu()
    {
        if (!sumaryScreen.isSumaryScreenOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.inPauseMenu)
                {
                    pauseMenu.ResumeGame();
                }
                else
                {
                    pauseMenu.PauseGame();
                }
            }
        }

    }
}
