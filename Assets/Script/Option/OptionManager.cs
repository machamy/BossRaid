using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Game
{
    public class OptionManager : MonoBehaviour
    {
        private static bool initialized = false;
        
        [FormerlySerializedAs("OptionUI")] [SerializeField] private GameObject OptionElements;
        [SerializeField] private GameObject BGM;
        [SerializeField] private GameObject Effect;
        [SerializeField] private Slider BGMSlider;
        [SerializeField] private Slider EffectSlider;

        private float volume_bgm;
        private float volume_effect;
        private void Start()
        {
            if (!initialized)
                init();
            BGMSlider.value = volume_bgm;
            EffectSlider.value = volume_effect;
            
            
            // TODO: 왜 이걸 넣어야만 제대로 되는거지????? 수정필요
            transform.gameObject.SetActive(false);
        }

        private void init()
        {
            Debug.Log(PlayerPrefs.GetFloat("volume_bgm",1.0f));
            Debug.Log(PlayerPrefs.GetFloat("volume_effect",1.0f));
            volume_bgm = PlayerPrefs.GetFloat("volume_bgm",1.0f);
            volume_effect = PlayerPrefs.GetFloat("volume_effect",1.0f);
            initialized = true;
            // SoundManager.Instance.ChangeVolumeBGM(volume_bgm);
            // SoundManager.Instance.ChangeVolumeEffect(volume_effect);
        }

        public void OnBGMVolumeChange(float val)
        {
            volume_bgm = val;
            SoundManager.Instance.ChangeSrcVolumeBGM(volume_bgm);
        }
        public void OnEffectVolumeChange(float val)
        {
            volume_effect = val;
            SoundManager.Instance.ChangeSrcVolumeEffect(volume_effect);
        }

        public void ClickExit()
        {
            PlayerPrefs.SetFloat("volume_bgm",volume_bgm);
            PlayerPrefs.SetFloat("volume_effect",volume_effect);
            transform.parent.gameObject.SetActive(false);
        }
    }
}