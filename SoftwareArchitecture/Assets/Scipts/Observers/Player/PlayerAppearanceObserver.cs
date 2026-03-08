using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAppearanceObserver : PlayerObserver
{
    [SerializeField] Image hitOverlay;

    protected override void OnPlayerAwake() 
    {
        hitOverlay.color = new Color32(255, 0, 0, 0);
    }

    protected override void OnPlayerHealthChange() 
    {
        StartCoroutine(FlashRed());
    }

    protected override void OnXPGained() {}

    protected override void OnPlayerDied() 
    {
        hitOverlay.color = new Color32(255, 0, 0, 128);
    }

    private IEnumerator FlashRed()
    {
        hitOverlay.color = new Color32(255, 0, 0, 128);

        yield return new WaitForSeconds(0.5f);

        hitOverlay.color = new Color32(255, 0, 0, 0);
    }
}
