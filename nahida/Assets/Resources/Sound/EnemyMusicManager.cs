using UnityEngine;

public class EnemyMusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip idle;
    [SerializeField]
    private AudioClip walking;
    [SerializeField]
    private AudioClip hurt1;
    [SerializeField]
    private AudioClip hurt2;
    [SerializeField]
    private AudioClip death;


    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void IdleReplay()
    {
        if (!source.isPlaying)
        {
            if (idle)
            {
                source.clip = idle;
                source.PlayScheduled(4);
            }
            
        }
    }

    public void Hurt1()
    {
        if (!source.isPlaying)
        {
            if (hurt1)
            {
                source.clip = hurt1;
                source.Play();
            }

        }
    }

    public void Hurt2()
    {
        if (!source.isPlaying)
        {
            if (hurt2)
            {
                source.clip = hurt2;
                source.Play();
            }

        }
    }

    public void Walking()
    {
        if (!source.isPlaying)
        {
            if (walking)
            {
                source.clip = walking;
                source.PlayScheduled(1);
            }

        }
    }

    public void Death()
    {
        if (!source.isPlaying)
        {
            if (death)
            {
                source.clip = death;
                source.Play();
            }

        }
    }
}
