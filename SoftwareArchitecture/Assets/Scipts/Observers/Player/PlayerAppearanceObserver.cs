using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAppearanceObserver : PlayerObserver
{
    [SerializeField] Image hitOverlay;

    protected override void OnPlayerAwake() {}

    protected override void OnPlayerHealthChange() 
    {
        StartCoroutine(FlashRed());
    }

    protected override void OnXPGained() {}

    protected override void OnPlayerDied()
    {
        
    }

    private IEnumerator FlashRed()
    {
        Color original = hitOverlay.color;
        hitOverlay.color = new Color (1, 0, 0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        hitOverlay.color = original;
    }
}
