using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioClip HarmAudio;
    [SerializeField] private AudioClip NonShotAudio;
    [SerializeField] private AudioClip JumpAudio;
    private AudioSource _source;
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;
    }
    public void PlayHarmAudio()
    {
        if (HarmAudio != null)
        {
            _source.clip = HarmAudio;
            _source.Play();
        }

    }
    public void PlayNonShotAudio()
    {
        if (NonShotAudio != null)
        {
            _source.clip = NonShotAudio;
            _source.Play();
        }
    }
    public void PlayJumpAudio()
    {
        if (JumpAudio != null)
        {
            _source.clip = JumpAudio;
            _source.Play();
        }
    }
}
