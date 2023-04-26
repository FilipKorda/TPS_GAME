using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] private Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button resumeButton;
    [SerializeField] public Button audioButton;
    [SerializeField] public Button graphicButton;
    [SerializeField] public Button controlsButton;
    [SerializeField] private Button quitButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject pauseMenuGameObject;
    [SerializeField] private GameObject hPSlider;
    [SerializeField] private GameObject eXPSlider;
    [SerializeField] private GameObject audioPanelGameObject;
    [SerializeField] private GameObject graphicPanelGameObject;
    [SerializeField] private GameObject controlPanelGameObject;
    [SerializeField] public GameObject grayOutPanel;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private AudioPanel audioPanel;
    [SerializeField] private GraphicPanel graphicPanel;
    [SerializeField] private ResulationPanel resulationPanel;
    [SerializeField] private ControlsPanel controlsPanel;
    [SerializeField] private PlayerController playerController;
    [Header("==== Anothers ====")]
    [Space(10)]
    [SerializeField] private string mainMenuScene;
    private int currentIndex = 0;
    public bool inPauseMenu = false;
    public static bool gamePaused = false;
    private bool isMouseOverButton = false;

    private void Start()
    {
        audioPanel = audioPanelGameObject.GetComponent<AudioPanel>();
    }
    public void Update()
    {
        OnPauseMenu();
        HandleMainMenuPanelInput();
        SetButtonToFirst();
    }
    public void SetButtonToFirst()
    {
        if(inPauseMenu)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }      
    }
    public void HandleMainMenuPanelInput()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel)
        {
            Button selectedButton = buttons[currentIndex];
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AudioManager.Instance.PlaySFX("ButtonSound");
                ToggleAttachedObject(selectedButton, false);
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = buttons.Length - 1;
                }

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                AudioManager.Instance.PlaySFX("ButtonSound");
                ToggleAttachedObject(selectedButton, false);
                currentIndex++;
                if (currentIndex >= buttons.Length)
                {
                    currentIndex = 0;
                }
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                bool anyButtonHovered = false;

                for (int i = 0; i < buttons.Length; i++)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(buttons[i].GetComponent<RectTransform>(), Input.mousePosition))
                    {
                        if (!isMouseOverButton)
                        {
                            AudioManager.Instance.PlaySFX("ButtonSound");
                            isMouseOverButton = true;
                        }
                        anyButtonHovered = true;

                        ToggleAttachedObject(selectedButton, false);
                        currentIndex = i;
                        selectedButton = buttons[currentIndex];
                        ToggleAttachedObject(selectedButton, true);
                        break;
                    }

                }
                if (!anyButtonHovered) // if no button is hovered, set the boolean to false
                {
                    isMouseOverButton = false;
                }

            }
            else
            {
                isMouseOverButton = false;
            }

            Submit();
        }
    }
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            if (selectedButton == resumeButton)
            {               
                resumeButton.onClick.Invoke();
            }
            else if (selectedButton == audioButton)
            {
                audioButton.onClick.Invoke();
            }
            else if (selectedButton == graphicButton)
            {
                graphicButton.onClick.Invoke();
            }
            else if (selectedButton == controlsButton)
            {
                controlsButton.onClick.Invoke();
            }
            else if (selectedButton == quitButton)
            {
                quitButton.onClick.Invoke();
            }
        }
    }
    private void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }
    private void OnPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inPauseMenu)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        hPSlider.SetActive(false);
        eXPSlider.SetActive(false);
        Time.timeScale = 0f;
        pauseMenuGameObject.SetActive(true);
        inPauseMenu = true;
        gamePaused = true;
        playerController.LookAtMouseEnabled = false;
    }
    public void ResumeGame()
    {
        hPSlider.SetActive(true);
        eXPSlider.SetActive(true);
        Time.timeScale = 1f;
        pauseMenuGameObject.SetActive(false);
        inPauseMenu = false;
        gamePaused = false;
        playerController.LookAtMouseEnabled = true;
    }
    public void OnButtonClickResumeGame()
    {
        AudioManager.Instance.PlaySFX("InterfaceBACK");
        hPSlider.SetActive(true);
        eXPSlider.SetActive(true);
        Time.timeScale = 1f;
        pauseMenuGameObject.SetActive(false);
        inPauseMenu = false;
        gamePaused = false;
        playerController.LookAtMouseEnabled = true;
        Debug.Log("Resume Dzia³a");
    }
    public void OnButtonClickAudioGame()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel)
        {
            grayOutPanel.SetActive(true);
            audioPanelGameObject.SetActive(true);
            audioPanel.inAudioPanel = true;
            inPauseMenu = false;
            if (!inPauseMenu)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (audioPanelGameObject.activeSelf == true)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }       
        else
        {
            Debug.Log("!");
        }
    }
    public void OnButtonClickGraphicGame()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            grayOutPanel.SetActive(true);
            graphicPanelGameObject.SetActive(true);
            graphicPanel.inGraphicPanel = true;
            inPauseMenu = false;
            if (!inPauseMenu)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (graphicPanelGameObject.activeSelf == true)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }       
        else
        {
            Debug.Log("!");
        }

    }
    public void OnButtonClickControlsGame()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            grayOutPanel.SetActive(true);
            controlPanelGameObject.SetActive(true);
            controlsPanel.inControlPanel = true;
            inPauseMenu = false;
            if (!inPauseMenu)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (controlPanelGameObject.activeSelf == true)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnButtonClickQuitGame()
    {
        AudioManager.Instance.PlaySFX("InterfaceBACK");
        Debug.Log("Quit");
    }
}
