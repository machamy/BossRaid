using System;
using UnityEngine;

namespace Script.Game
{
    public class OptionManager : MonoBehaviour
    {
        [SerializeField] private GameObject OptionUI;
        [SerializeField] private GameObject BGM;
        [SerializeField] private GameObject Effect;
        private float volume_bgm;
        private float volume_effect;
        private void Start()
        {
            float volume_bgm = PlayerPrefs.GetFloat("volume_bgm",0.5f);
            float volume_effect = PlayerPrefs.GetFloat("volume_effect",0.5f);
            
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