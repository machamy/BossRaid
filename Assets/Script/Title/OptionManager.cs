using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Game
{
    public class OptionManager : MonoBehaviour
    {
        [SerializeField] private GameObject OptionUI;
        [SerializeField] private GameObject BGM;
        [SerializeField] private GameObject Effect;
        [SerializeField] private Slider BGMSlider;
        [SerializeField] private Slider EffectSlider;

        private float volume_bgm;
        private float volume_effect;
        private void Start()
        {
            volume_bgm = PlayerPrefs.GetFloat("volume_bgm",1.0f);
            volume_effect = PlayerPrefs.GetFloat("volume_effect",1.0f);
            BGMSlider.value = volume_bgm;
            EffectSlider.value = volume_effect;
            SoundManager.Instance.ChangeVolumeBGM(volume_bgm);
            SoundManager.Instance.ChangeVolumeEffect(volume_effect);
        }

        public void OnBGMVolumeChange(float val)
        {
            volume_bgm = val;
            SoundManager.Instance.ChangeVolumeBGM(volume_bgm);
        }
        public void OnEffectVolumeChange(float val)
        {
            volume_effect = val;
            SoundManager.Instance.ChangeVolumeEffect(volume_effect);
        }

        public void ClickExit()
        {
            PlayerPrefs.SetFloat("volume_bgm",volume_bgm);
            PlayerPrefs.SetFloat("volume_effect",volume_effect);
            OptionUI.SetActive(false);
        }
    }
}