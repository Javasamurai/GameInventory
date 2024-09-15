using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
class AudioService : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    private Sound[] sounds;
    private void Awake()
    {
        EventService.Instance.OnItemPurchased.AddListener(PlayPurchaseSound);        
    }

    private void PlayPurchaseSound(Item item)
    {
        Sound sound = Array.Find(sounds, s => s.soundType == SoundType.Purchase);
        audioSource.PlayOneShot(sound.clip);
    }
}

[Serializable]
struct Sound
{
    public SoundType soundType;
    public AudioClip clip;
}

enum SoundType
{
    Purchase,
    Gather
}