using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    [SerializeField] 
    private AudioMixer _mixer;
    [SerializeField] 
    private Sound[] sounds;

    private Sound[] punches;

    void Awake() {
        // singletone pattern
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        foreach(Sound _sound in sounds) {
            _sound._source = gameObject.AddComponent<AudioSource>();
            
            _sound._source.clip = _sound._clip;
            
            _sound._source.volume = _sound.volume;
            _sound._source.pitch = _sound.pitch;
            _sound._source.loop = _sound.loop;
            _sound._source.outputAudioMixerGroup = _sound.audioMixerGroup;
        }

        punches = new Sound[3];
        for(var i=1; i<4; i++) {
            punches[i-1] = Array.Find(sounds, sound => sound.name == "Hit"+i.ToString());
        }
    }

    public void Play(string name, float pitch=-1f) {
        Sound _sound = Array.Find(sounds, sound => sound.name == name);
        
        if(_sound == null) {
            print("Cannot find \'" + name + "\' sound in AudioManager");
            return;
        }

        if(pitch > 0f)
            _sound._source.pitch = pitch;        

        _sound._source.Play();        
    }

    public void PlayPunch() {
        punches[UnityEngine.Random.Range(0, 3)]._source.Play();
    }

    public void StopAllFX() {
        foreach (Sound _sound in sounds) {
            _sound._source.Stop();
        }
    }
}
