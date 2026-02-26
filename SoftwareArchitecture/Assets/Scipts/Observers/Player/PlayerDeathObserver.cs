using StarterAssets;
using UnityEngine;

public class PlayerDeathObserver : PlayerObserver
{
    [SerializeField] EnemySpawner[] spawners;

    protected override void OnPlayerAwake() {}

    protected override void OnPlayerHealthChange() {}

    protected override void OnXPGained() {}

    protected override void OnPlayerDied()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerStarts.gameObject != null)
            playerStarts.gameObject.SetActive(false);

        foreach (EnemySpawner spawner in spawners)
            spawner.StopAllCoroutines();
    }
}
