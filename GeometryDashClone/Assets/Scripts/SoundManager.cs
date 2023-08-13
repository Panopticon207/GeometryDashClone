using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource source;


    private void Awake()
    {

        source = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }
    public void PlaySound(AudioClip _sound, float volume)
    {
        try
        {
            source.PlayOneShot(_sound,volume);
        }
        catch (System.Exception)
        {


        }
    }

}