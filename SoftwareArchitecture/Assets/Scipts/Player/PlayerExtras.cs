using UnityEngine;
using UnityEngine.InputSystem;
using System;
using SA_Inventory;
using StarterAssets;

public class PlayerExtras : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    private bool inventoryIsShown = false;

    [SerializeField] GameObject skillTree;
    private bool skillTreeIsShown = false;

    [SerializeField] GameObject pauseMenu;
    private bool pauseMenuIsShown = false;

    [SerializeField] GameObject stats;

    private StarterAssetsInputs assetsInputs;
    private FirstPersonController firstPersonController;

    public Quest quest;

    private PlayerAttacks playerAttacks;

    private void Awake()
    {
        assetsInputs = GetComponent<StarterAssetsInputs>();
        firstPersonController = GetComponent<FirstPersonController>();
        playerAttacks = GetComponent<PlayerAttacks>();
    }

    private void ToggleUI(ref bool isShown, GameObject uiElement, Action onActivate = null, Action onDeactivate = null)
    {
        isShown = !isShown;
        uiElement.SetActive(isShown);

        if (isShown)
        {
            onActivate?.Invoke();
            assetsInputs.cursorLocked = false;
            assetsInputs.cursorInputForLook = false;
            playerAttacks.canAttack = false;

            if (firstPersonController.Grounded)
            {
                firstPersonController.enabled = false;
            }
        }

        else
        {
            onDeactivate?.Invoke();
            assetsInputs.cursorLocked = true;
            assetsInputs.cursorInputForLook = true;
            firstPersonController.enabled = true;
            playerAttacks.canAttack = true;
        }

        assetsInputs.OnApplicationFocus(assetsInputs.cursorLocked);
    }

    void OnInventory()
    {
        ToggleUI(ref inventoryIsShown, inventory);
    }

    void OnSkillTree()
    {
        ToggleUI(ref skillTreeIsShown, skillTree,
        onActivate: () =>
        {
            Time.timeScale = 0;
            stats.SetActive(false);
        },
        onDeactivate: () =>
        {
            Time.timeScale = 1;
            stats.SetActive(true);
        }
        );
    }

    void OnPauseMenu()
    {
        ToggleUI(ref pauseMenuIsShown, pauseMenu, onActivate: () =>
        {
            Time.timeScale = 0;
            stats.SetActive(false);
        },
        onDeactivate: () =>
        {
            Time.timeScale = 1;
            stats.SetActive(true);
        }
        );
    }
}
