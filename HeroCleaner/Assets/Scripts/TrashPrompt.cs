using UnityEngine;
using UnityEngine.UI;

public class TrashPrompt : MonoBehaviour
{
    [SerializeField] private Image progressBar;

    private Canvas promptCanvas;

    private void Awake()
    {
        promptCanvas = GetComponent<Canvas>();
        promptCanvas.enabled = false;
    }

    public void ShowPrompt(bool show)
    {
        promptCanvas.enabled = show;

        if (!show)
            progressBar.fillAmount = 0f;
    }

    public void UpdateProgress(float progress)
    {
        progressBar.fillAmount = Mathf.Clamp01(progress);
    }
}
