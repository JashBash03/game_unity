using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Declara una variable est�tica llamada "instance" que har� referencia a la �nica instancia de la clase AudioManager (patr�n singleton).
    public static AudioManager instance;

    // Declara variables p�blicas para el componente AudioSource y un arreglo de AudioClip para almacenar las pistas de m�sica.
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    // Declara una variable privada para realizar un seguimiento del �ndice de la pista de m�sica actual en el arreglo.
    private int currentClipIndex = 0;

    // Define el m�todo Awake, que se ejecuta cuando el objeto se instancia. Implementa el patr�n singleton.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Define el m�todo Start, que se ejecuta al inicio. Verifica y configura el AudioSource y reproduce la primera pista de m�sica.
    private void Start()
    {
        if (musicSource == null)
        {
            musicSource = FindObjectOfType<AudioSource>();
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
            }
        }

        PlayMusic();
    }

    // Define el m�todo Update, que se ejecuta en cada frame. Verifica si la pista de m�sica actual ha terminado de reproducirse.
    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            OnMusicFinished();
            PlayNextMusic();
        }
    }

    // Define el m�todo PlayMusic, que reproduce la pista de m�sica actual. Muestra una advertencia si no se han asignado clips de m�sica.
    public void PlayMusic()
    {
        if (musicClips.Length > 0)
        {
            musicSource.clip = musicClips[currentClipIndex];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("No music clips assigned.");
        }
    }

    // Define m�todos para pausar y reanudar la reproducci�n de la m�sica.
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    // Define un m�todo para detener la reproducci�n de la m�sica.
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Define un m�todo para reproducir la siguiente pista de m�sica en el arreglo.
    public void PlayNextMusic()
    {
        currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
        PlayMusic();
    }

    // Define un m�todo para ajustar el volumen del AudioSource.
    public void SetVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    // Define un m�todo llamado OnMusicFinished que se llama cuando la m�sica ha terminado de reproducirse.
    private void OnMusicFinished()
    {
        // Puedes implementar l�gica adicional o activar eventos cuando la m�sica termina.
        Debug.Log("Music finished playing.");
    }
}
