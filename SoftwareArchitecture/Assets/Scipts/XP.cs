using UnityEngine;

public class XP : MonoBehaviour
{
    public int xp;

    [SerializeField] PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats.AddXP(xp);
            Destroy(gameObject);
        }
    }
}
