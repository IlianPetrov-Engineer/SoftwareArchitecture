using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void OnPress()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(activeScene);
    }
}
