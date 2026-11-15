using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToScreen : MonoBehaviour
{
    // back to main screen
    public void QuitToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
