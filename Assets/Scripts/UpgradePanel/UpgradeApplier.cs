using UnityEngine;
using UnityEngine.UI;

public class UpgradeApplier : MonoBehaviour
{
    public GameObject player;
    public UpgradeSelector upgradeSelector;

    public void ApplyUpgrade()
    {
        int selectedUpgrade = upgradeSelector.selectedUpgrade;

        switch (selectedUpgrade)
        {
            case 1:
                player.GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case 2:
                player.GetComponent<SpriteRenderer>().color = Color.green;
                break;

            case 3:
                player.GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            default:
                Debug.LogError("Invalid selected upgrade: " + selectedUpgrade);
                break;
        }
    }

}
