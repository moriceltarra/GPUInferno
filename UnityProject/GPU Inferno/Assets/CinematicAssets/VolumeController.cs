using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeController : MonoBehaviour
{
   public AudioMixer audioMixer; // Arrastra el Audio Mixer aquí
    public Slider volumeSlider;   // Arrastra el Slider aquí


    void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>(); // Encuentra todos los AudioSource en la escena

        Debug.Log($"📢 Se encontraron {audioSources.Length} objetos con AudioSource:");

        foreach (AudioSource audio in audioSources)
        {
            Debug.Log($"🔊 Objeto: {audio.gameObject.name} | Volume: {audio.volume} | IsPlaying: {audio.isPlaying}");
        }

    }
    public void SetVolume(string type)
    {
        audioMixer.SetFloat(type, volumeSlider.value);
        PlayerPrefs.SetFloat(type, volumeSlider.value);
        Debug.Log("dsgdfgdsf");
    }

}
