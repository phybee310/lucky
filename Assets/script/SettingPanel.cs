using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;

    // Reference to global score (assuming globalScore is static in TrashCan or another class)
    

    public void Setting()
    {
        settingPanel.SetActive(true);
        // Freeze time during pause
    }

    public void Back()
    {
        settingPanel.SetActive(false);
         // Resume time when game is unpaused
    }
}
