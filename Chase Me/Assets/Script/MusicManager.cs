using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (FindFirstObjectByType<MusicManager>() != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

}
