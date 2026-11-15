using UnityEngine;

public class StartingNotePadCloser : MonoBehaviour
{
    [SerializeField] private Canvas startingNotePadScreen;

    public bool closed {  get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;
        startingNotePadScreen.enabled = true;
        closed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 1.0f;
            startingNotePadScreen.enabled = false;
            closed = true;
        }
    }
}
