using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] public Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] private Button volumeButton;
    [SerializeField] private Button graphicButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] public Button backButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] public GameObject grayOutPanel;
    [SerializeField] private GameObject audioPanelGameObject;
    [SerializeField] private GameObject graphicPanelGameObject;
    [SerializeField] private GameObject controlPanelGameObject;
    [SerializeField] private GameObject mainMenuImage;
    [SerializeField] private GameObject settingsImage;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private ResulationPanel resulationPanel;
    [SerializeField] private AudioPanel audioPanel;
    [SerializeField] private GraphicPanel graphicPanel;
    [SerializeField] private ControlsPanel controlsPanel;
    [Header("==== Anothers ====")]
    [Space(10)]  
    public bool inSettingsPanel = false;
    private int currentIndex = 0;
    private bool isMouseOverButton = false;
    private void Start()
    {      
        audioPanel = audioPanelGameObject.GetComponent<AudioPanel>();
    }
    private void Update()
    {
        HandleSettingsPanelInput();
        SetButtonToFirstS();
    }
    public void SetButtonToFirstS()
    {
        if (inSettingsPanel)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }

    }
    private void HandleSettingsPanelInput()
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
            if (selectedButton == volumeButton)
            {             
                volumeButton.onClick.Invoke();
            }
            else if (selectedButton == graphicButton)
            {
                graphicButton.onClick.Invoke();
            }
            else if (selectedButton == controlsButton)
            {
                controlsButton.onClick.Invoke();
            }
            else if (selectedButton == backButton)
            {
                backButton.onClick.Invoke();
            }
        }
    }
    public void OnAudioButtonClick()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            settingsImage.SetActive(false);
            grayOutPanel.SetActive(true);
            audioPanelGameObject.SetActive(true);
            audioPanel.inAudioPanel = true;
            inSettingsPanel = false;
            if (!inSettingsPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            Debug.Log("AudioPanel");
        }
        if (audioPanelGameObject.activeSelf == true)
        {
            currentIndex = 0;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
        
    }
    public void OnGraphicButtonClick()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            settingsImage.SetActive(false);
            grayOutPanel.SetActive(true);
            graphicPanelGameObject.SetActive(true);
            graphicPanel.inGraphicPanel = true;
            inSettingsPanel = false;
            if (!inSettingsPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            Debug.Log("GrahpicPanel");
        }
        if (graphicPanelGameObject.activeSelf == true)
        {
            currentIndex = 0;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnControlsButtonClick()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            settingsImage.SetActive(false);
            grayOutPanel.SetActive(true);
            controlPanelGameObject.SetActive(true);
            controlsPanel.inControlPanel = true;
            inSettingsPanel = false;
            if (!inSettingsPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            Debug.Log("ControlPanel");
        }
        if (controlPanelGameObject.activeSelf == true)
        {
            currentIndex = 0;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnBackButtonClick()
    {
        if (!audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive)
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            mainMenuImage.SetActive(true);
            settingsPanel.SetActive(false);
            inSettingsPanel = false;
            mainMenuController.inMainMenu = true;
            mainMenuController.grayOutPanel.SetActive(false);
            if (!inSettingsPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (settingsPanel.activeSelf == false)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            Debug.Log("ExitSettings");
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }
}
