using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

/// <summary>
/// 싱글턴
/// </summary>
public class SoundManager
{
    
    public enum SoundType
    {
        BGM,
        Effect,
        MAX
    }

    static private SoundManager instance;
    static public SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
                instance.init();
            }
                
            return instance;
        }
    }

    /// <summary>
    /// BGM와 효과음은 오디오 소스 분리
    /// </summary>
    private AudioSource _bgmSource;
    private LinkedList<AudioSource> _effectSources;
    private LinkedList<string> _playingEffects;
    /// <summary>
    /// 소리가 미리 저장되어 있는 딕셔너리
    /// </summary>
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private AudioMixer mixer;
    private AudioMixerGroup bgmGroup;
    private AudioMixerGroup effectGroup;

    public SoundManager()
    {
        _effectSources = new LinkedList<AudioSource>();
        _playingEffects = new LinkedList<string>();
    }

    public void init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null) 
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(SoundType)); // "Bgm", "Effect"

            GameObject bgm_go = new GameObject { name = soundNames[(int)SoundType.BGM] }; 
            _bgmSource = bgm_go.AddComponent<AudioSource>();
            bgm_go.transform.parent = root.transform;
            
            GameObject effect_go = new GameObject { name = soundNames[(int)SoundType.Effect] };
            effect_go.transform.parent = root.transform;
            this.effect_go = effect_go;
            AddEffectSource();
            mixer = Resources.Load<AudioMixer>("MainMixer");
            bgmGroup = mixer.FindMatchingGroups("BGM")[0];
            effectGroup = mixer.FindMatchingGroups("Effect")[0];
            
            _bgmSource.loop = true; // bgm 재생기는 무한 반복 재생
        }
    }

    private GameObject effect_go;
    
    public void Play(string path, SoundType type = SoundType.Effect, float pitch = 1.0f, float volume = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch,volume);
    }
    
    public void Play(AudioClip audioClip, SoundType type = SoundType.Effect, float pitch = 1.0f,float volume = 1.0f)
    {
        if (audioClip == null)
            return;
        Debug.Log(audioClip);
        if (type == SoundType.BGM) // BGM 배경음악 재생
        {
            AudioSource audioSource = _bgmSource;
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = bgmGroup;
            audioSource.Play();
        }
        else // Effect 효과음 재생
        {
            AudioSource audioSource = GetEffectSource();
            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = effectGroup;
            audioSource.PlayOneShot(audioClip);
            _playingEffects.AddLast(audioClip.name);
        }
    }

    public AudioSource GetEffectSource()
    {
        foreach (var source in _effectSources)
        {
            if (source.isPlaying == false)
                return source;
        }
        return AddEffectSource();
    }

    private AudioSource AddEffectSource()
    {
        var source = effect_go.AddComponent<AudioSource>();
        _effectSources.AddLast(source);
        return source;
    }
    
    AudioClip GetOrAddAudioClip(string path, SoundType type = SoundType.Effect)
    {
        if (path.Contains("Sound/") == false)
            path = $"Sound/{path}"; // 📂Sound 폴더 안에 저장될 수 있도록

        AudioClip audioClip;

        if (type == SoundType.BGM) // BGM 배경음악 클립 붙이기
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else // Effect 효과음 클립 붙이기
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }

    public void ChangeVolumeBGM(float val)
    {
        mixer.SetFloat("BGM", Mathf.Log10(val)*20);
    }

    public void ChangeVolumeEffect(float val)
    {
        mixer.SetFloat("Effect", Mathf.Log10(val)*20);
    }
    
    public void ChangeSrcVolumeBGM(float val)
    {
        _bgmSource.volume = val;
    }

    public bool optVibration;
    public void DoVibration()
    {
    #if UNITY_ANDROID
            if(optVibration)
                Handheld.Vibrate();
    #endif
    }
    
    /// <summary>
    /// effect 리스트를 돌며 볼륨 변경
    /// 만약 재생중이 아닌 소스가 있을경우 삭제
    /// </summary>
    /// <param name="val"></param>
    public void ChangeSrcVolumeEffect(float val)
    {
        var nodeSrc = _effectSources.First;
        if (nodeSrc == null)
            return;
        do
        {
            var src = nodeSrc.Value;
            nodeSrc = nodeSrc.Next;
            if (src.isPlaying)
            {
                src.volume = val;
            }
            // else
            // {
            //     _effectSources.Remove(nodeSrc);
            // }
        } while (nodeSrc != null);
    }

    // public void Stop(string name)
    // {
    //     var node_src = _effectSources.First;
    //     var node = _playingEffects.First;
    //     
    //     do
    //     {
    //         var src = node.Value;
    //         node_src = node_src.Next;
    //         node = node.Next;
    //         if (node.Value == name)
    //         {
    //             node_src.Value.Stop();
    //             _playingEffects.Remove(node);
    //         }
    //     } while (node != null);
    // }
    
    public void Clear()
    {
        _bgmSource.clip = null;
        _bgmSource.Stop();
        
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _effectSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
        // 재생중 효과음 이름 지우기
        _playingEffects.Clear();
    }

}
   
    

    


