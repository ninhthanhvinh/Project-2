using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public bool isSFX;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volumn = .7f;
    [Range(.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, .5f)]
    public float randomVolumn = .1f;
    [Range(0f, .5f)]
    public float randomPitch = .1f;

    [HideInInspector]
    public AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volumn * (1 + Random.Range(-randomVolumn / 2f, randomVolumn / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    Sound[] sounds;

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            //Instance = this;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            if (sounds[i].isSFX)
            {
                sounds[i].source.outputAudioMixerGroup = sfxMixer;
            }
            else
            {
                sounds[i].source.outputAudioMixerGroup = musicMixer;
            }
        }
    }

    void Start()
    {

    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("Audio Manager: Sound not found");
    }
}
