using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class BonusManager : MonoBehaviour
{
    [SerializeField] RoomManager roomManager;
    private GameObject bomb;
    private GameObject portal;
    private Transform text;
    [SerializeField] private GameObject particleObject;
    private AudioSource source;
    [SerializeField] AudioClip clip;
    private bool hasBeenPlayed = false;

    public void Start()
    {
        bomb = transform.parent.Find("Bomb").gameObject;
        portal = transform.parent.Find("Portal blue").gameObject;
        text = transform.parent.Find("Canvas").Find("Text");
        source = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (bomb != null && bomb.activeSelf)
        {
            if (bomb.GetComponent<Animator>().GetBool("isLidOpen"))
            {
                bomb.GetComponent<Grabbable>().enabled = true;
                bomb.GetComponent<HandGrabInteractable>().enabled = true;
                portal.SetActive(true);
                portal.GetComponent<Animator>().SetBool("canBeShown", true);
                if (source && ! hasBeenPlayed)
                {
                    source.clip = clip;
                    source.Play();
                    hasBeenPlayed = true;
                }
                    
                text.gameObject.SetActive(true);
            }

            if (bomb.transform.localScale.x < 0.1f)
            {
                Destroy(bomb.gameObject);
            }
        }
        if (portal != null && portal.activeSelf)
        {
            if (bomb == null)
            {
                Destroy(portal.gameObject);
                Destroy(text.gameObject);
                roomManager.OpenDoors();
                NetworkingBonus.SendBonusEvent(BonusType.BOMB);
            }
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Lid")
        {
            if (particleObject)
                particleObject.SetActive(true);
            bomb.GetComponent<Animator>().SetBool("isLidOpen", true);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Lid")
        {
            bomb.GetComponent<Animator>().SetBool("isLidOpen", false);
        }
    }
}