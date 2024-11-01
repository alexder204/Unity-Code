using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerDialogue
{
    public class AudioManager : MonoBehaviour
    {
        [Header("AudioSource")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource SFXSource;

        [Header("AudioClip")]
        public AudioClip audioClip;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            musicSource.clip = audioClip;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}
