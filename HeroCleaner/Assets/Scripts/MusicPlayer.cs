using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct TrackInfo
    {
        public string songTitle;
        public string artistName;
        public AudioClip clip;
    }

    [Header("Playlist Settings")]
    [SerializeField] private TrackInfo[] playlist;

    [Header("UI References")]
    [SerializeField] private CanvasGroup uiCanvasGroup;
    [SerializeField] private TMP_Text songTitleUI;
    [SerializeField] private TMP_Text artistNameUI;

    [Header("Fade Timing (Seconds)")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float displayDuration = 3.0f;

    private AudioSource audioSource;
    private List<int> remainingIndices = new List<int>();
    private int lastPlayedIndex = -1;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (uiCanvasGroup != null)
            uiCanvasGroup.alpha = 0f;

        if (playlist.Length > 0)
        {
            PlayNextRandomTrack();
        }
    }

    private void Update()
    {
        // Note: AudioSource.isPlaying becomes false if the game is paused or unfocused unless handled,
        // or when the track finishes naturally.
        if (!audioSource.isPlaying && playlist.Length > 0)
        {
            PlayNextRandomTrack();
        }
    }

    private void PlayNextRandomTrack()
    {
        // Refill and shuffle the bag when all tracks have been played
        if (remainingIndices.Count == 0)
        {
            RefillShuffleBag();
        }

        // Grab the next track index from the shuffled list
        int nextIndex = remainingIndices[0];
        remainingIndices.RemoveAt(0);
        lastPlayedIndex = nextIndex;

        PlayTrack(nextIndex);
    }

    private void RefillShuffleBag()
    {
        remainingIndices.Clear();
        for (int i = 0; i < playlist.Length; i++)
        {
            remainingIndices.Add(i);
        }

        // Fisher-Yates shuffle
        for (int i = remainingIndices.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = remainingIndices[i];
            remainingIndices[i] = remainingIndices[randomIndex];
            remainingIndices[randomIndex] = temp;
        }

        // Prevent the first song of the new shuffle from repeating the very last song played
        if (playlist.Length > 1 && remainingIndices[0] == lastPlayedIndex)
        {
            int swapIndex = Random.Range(1, remainingIndices.Count);
            int temp = remainingIndices[0];
            remainingIndices[0] = remainingIndices[swapIndex];
            remainingIndices[swapIndex] = temp;
        }
    }

    private void PlayTrack(int index)
    {
        TrackInfo currentTrack = playlist[index];

        audioSource.clip = currentTrack.clip;
        audioSource.Play();

        if (songTitleUI != null)
            songTitleUI.text = currentTrack.songTitle;

        if (artistNameUI != null)
            artistNameUI.text = currentTrack.artistName;

        if (uiCanvasGroup != null)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeNowPlayingUI());
        }
    }

    private IEnumerator FadeNowPlayingUI()
    {
        // Fade In
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            uiCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        uiCanvasGroup.alpha = 1f;

        // Stay Visible
        yield return new WaitForSeconds(displayDuration);

        // Fade Out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            uiCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            yield return null;
        }
        uiCanvasGroup.alpha = 0f;
    }
}
