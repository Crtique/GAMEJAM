using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(DeleyLoad(2.0f));
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    IEnumerator DeleyLoad(float deley)
    {
        yield return new WaitForSeconds(deley);
    }
}
