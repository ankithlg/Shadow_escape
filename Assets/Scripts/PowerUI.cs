using UnityEngine;
using TMPro;

public class PowerUI : MonoBehaviour
{
    public TMP_Text freezeText;
    public TMP_Text invisibleText;

    private PlayerPowers playerPowers;

    void Update()
    {
        if (playerPowers == null)
        {
            playerPowers = FindFirstObjectByType<PlayerPowers>();

            if (playerPowers == null)
                return;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (playerPowers.IsInvisible())
        {
            invisibleText.text =
                " 'X' --> Invisible: Active\n";
        }
        else if (playerPowers.GetInvisCooldown() > 0f)
        {
            invisibleText.text =
                " 'X' --> Invisible: " +
                playerPowers.GetInvisCooldown().ToString("0.0") + "s";
        }
        else
        {
            invisibleText.text = " 'X' -->  Invisible: Ready";
        }


        if (playerPowers.IsFreezeActive())
        {
            freezeText.text =
                " 'C' --> Freeze: Active\n";
        }
        else if (playerPowers.GetFreezeCooldown() > 0f)
        {
            freezeText.text =
                " 'C' --> Freeze: " +
                playerPowers.GetFreezeCooldown().ToString("0.0") + "s";
        }
        else
        {
            freezeText.text = " 'C' --> Freeze: Ready";
        }
    }
}