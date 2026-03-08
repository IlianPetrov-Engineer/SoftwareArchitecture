using UnityEngine;

public abstract class PlayerObserver : MonoBehaviour
{
    [SerializeField] protected PlayerStats playerStarts;

    protected void OnEnable()
    {
        playerStarts.onHealthChanged += OnPlayerHealthChange;
        playerStarts.onPlayerDeath += OnPlayerDied;
        playerStarts.onXPChanged += OnXPGained;
        playerStarts.onPlayerAwake += OnPlayerAwake;
    }

    protected void OnDisable()
    {
        playerStarts.onHealthChanged -= OnPlayerHealthChange;
        playerStarts.onPlayerDeath -= OnPlayerDied;
        playerStarts.onXPChanged -= OnXPGained;
        playerStarts.onPlayerAwake -= OnPlayerAwake;
    }

    protected abstract void OnPlayerAwake();

    protected abstract void OnPlayerHealthChange();

    protected abstract void OnPlayerDied();

    protected abstract void OnXPGained();
}
