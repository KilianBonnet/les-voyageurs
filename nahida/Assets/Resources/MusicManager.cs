using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clips;
    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        EntryRoomTrigger.PlayerRoomEnterEvent += HandleEntryEvent;
    }

    private void HandleEntryEvent(int room)
    {
        switch (room)
        {
            case 1:
                source.clip = clips[1];
                source.Play();
                break;
            case 2:
                source.clip = clips[1];
                source.Play();
                break;
            case 3:
                source.clip = clips[1];
                source.Play();
                break;
            case 5:
                source.clip = clips[1];
                source.Play();
                break;
            case 6:
                source.clip = clips[1];
                source.Play();
                break;
            case 8:
                source.clip = clips[1];
                source.Play();
                break;
            case 9:
                source.clip = clips[2];
                source.Play();
                break;
            case 11:
                source.clip = clips[1];
                source.Play();
                break;
            case 14:
                source.clip = clips[1];
                source.Play();
                break;
            default:
                source.clip = clips[0];
                source.Play();
                break;

        }
    }
}
