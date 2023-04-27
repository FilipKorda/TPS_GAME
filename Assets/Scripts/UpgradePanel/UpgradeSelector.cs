using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelector : MonoBehaviour
{
    public int selectedUpgrade = 0; // default selected upgrade is the first one

    //Button references
    public Button upgrade1Button;
    public Button upgrade2Button;
    public Button upgrade3Button;

    [SerializeField] public GameObject selectionTab;

    public void Update()
    {
        // Attach the OnClick functions to the buttons
        upgrade1Button.onClick.AddListener(SelectUpgrade1);
        upgrade2Button.onClick.AddListener(SelectUpgrade2);
        upgrade3Button.onClick.AddListener(SelectUpgrade3);
    }

    private void SelectUpgrade1()
    {
        selectedUpgrade = 1;
        selectionTab.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SelectUpgrade2()
    {
        selectedUpgrade = 2;
        selectionTab.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SelectUpgrade3()
    {
        selectedUpgrade = 3;
        selectionTab.SetActive(false);
        Time.timeScale = 1f;
    }
}
