using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioPanel : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] private Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button masterButton;
    [SerializeField] public Button musicButton;
    [SerializeField] public Button sfxButton;
    [SerializeField] public Button applyButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject settingsImage;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private SettingsPanelController settingsPanelController;
    [SerializeField] private PauseMenu pasueMenu;
    [Header("==== Volume Settings ====")]
    [Space(10)]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    public float currentMasterVolume = 1f;
    public float currentMusicVolume = 1f;
    public float currentSfxVolume = 1f;
    public float volumeIncrement = 0.1f;
    [Header("==== Anothers ====")]
    [Space(10)]
    public bool inAudioPanel = false;
    public bool isButtonSelected = false;
    private int currentIndex = 0;
    private bool isMouseOverButton = false;
    [SerializeField] public Animator buffering;
    [SerializeField] private GameObject bufferingGameObject;


    private void Start()
    {
        bufferingGameObject.SetActive(false);
    }
    private void Update()
    {
        HandleAudioPanelInput();
        SetButtonToFirstS();
        SoundsVolumeChangeUp();
    }
    public void MasterVolume()
    {
        AudioManager.Instance.MasterVolume(masterVolumeSlider.value);
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicVolumeSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SfxVolume(sfxVolumeSlider.value);
    }
    public void SetButtonToFirstS()
    {
        if (inAudioPanel)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }

    }
    private void HandleAudioPanelInput()
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
    public void SoundsVolumeChangeUp()
    {
        Button selectedButton = buttons[currentIndex];
        if (selectedButton == buttons[0])
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentMasterVolume = Mathf.Clamp(currentMasterVolume + volumeIncrement, 0f, 1f);
                masterVolumeSlider.value = currentMasterVolume;

                bufferingGameObject.SetActive(true);
                buffering.SetTrigger("Play");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentMasterVolume = Mathf.Clamp(currentMasterVolume - volumeIncrement, 0f, 1f);
                masterVolumeSlider.value = currentMasterVolume;

                bufferingGameObject.SetActive(true);
                buffering.SetTrigger("Play");
            }
        }
        else if (selectedButton == buttons[1])
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentMusicVolume = Mathf.Clamp(currentMusicVolume + volumeIncrement, 0f, 1f);
                musicVolumeSlider.value = currentMusicVolume;

                bufferingGameObject.SetActive(true);
                buffering.SetTrigger("Play");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentMusicVolume = Mathf.Clamp(currentMusicVolume - volumeIncrement, 0f, 1f);
                musicVolumeSlider.value = currentMusicVolume;

                bufferingGameObject.SetActive(true);
                buffering.SetTrigger("Play");
            }

        }
        else if (selectedButton == buttons[2])
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentSfxVolume = Mathf.Clamp(currentSfxVolume + volumeIncrement, 0f, 1f);
                sfxVolumeSlider.value = currentSfxVolume;

                bufferingGameObject.SetActive(true);
                buffering.SetTrigger("Play");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentSfxVolume = Mathf.Clamp(currentSfxVolume - volumeIncrement, 0f, 1f);
                sfxVolumeSlider.value = currentSfxVolume;

                bufferingGameObject.SetActive(true);
                buffering.SetTrigger("Play");
            }
        }
    }
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {            
            if (selectedButton == masterButton)
            {
                masterButton.onClick.Invoke();
            }
            else if (selectedButton == musicButton)
            {
                musicButton.onClick.Invoke();
            }
            else if (selectedButton == sfxButton)
            {
                sfxButton.onClick.Invoke();
            }
            else if (selectedButton == applyButton)
            {               
                applyButton.onClick.Invoke();
            }
        }
    }
    private void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
        isButtonSelected = true;

    }
    public void ButtonToggleMaster()
    {
        AudioManager.Instance.ToggleMaster();
        Debug.Log("cichosza");
    }
    public void ButtonToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ButtonToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
    public void ApplyButton()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.Instance.PlaySFX("ApplyButton");
            settingsImage.SetActive(true);
            currentMasterVolume = masterVolumeSlider.value;
            currentMusicVolume = musicVolumeSlider.value;
            currentSfxVolume = sfxVolumeSlider.value;
            bufferingGameObject.SetActive(false);
            buffering.SetTrigger("Stop");
            PlayerPrefs.SetFloat("master_volume", currentMasterVolume);
            PlayerPrefs.SetFloat("music_volume", currentMusicVolume);
            PlayerPrefs.SetFloat("sfx_volume", currentSfxVolume);
            PlayerPrefs.Save();
            Debug.Log("Zapisano");

            settingsPanelController.grayOutPanel.SetActive(false);
            audioPanel.SetActive(false);
            inAudioPanel = false;
            settingsPanelController.inSettingsPanel = true;
            if (!inAudioPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (audioPanel.activeSelf == false)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }

        if (SceneManager.GetActiveScene().name == "LEVEL")
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            currentMasterVolume = masterVolumeSlider.value;
            currentMusicVolume = musicVolumeSlider.value;
            currentSfxVolume = sfxVolumeSlider.value;
            bufferingGameObject.SetActive(false);
            buffering.SetTrigger("Stop");
            PlayerPrefs.SetFloat("master_volume", currentMasterVolume);
            PlayerPrefs.SetFloat("music_volume", currentMusicVolume);
            PlayerPrefs.SetFloat("sfx_volume", currentSfxVolume);
            PlayerPrefs.Save();
            Debug.Log("Zapisano");

            pasueMenu.grayOutPanel.SetActive(false);
            inAudioPanel = false;
            audioPanel.SetActive(false);
            pasueMenu.inPauseMenu = true;
            if (!inAudioPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (audioPanel.activeSelf == false)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }
    }

}
