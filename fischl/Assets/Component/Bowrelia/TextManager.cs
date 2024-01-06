using System.Collections;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject floatingText;
    [SerializeField] ParticleSystem deathParticles;

    private void OnEnable()
    {
        Slime.OnDeath += HandleDeath;
    }

    private void HandleDeath(Transform slimeTransform)
    {
        if (floatingText)
        {
            Instantiate(floatingText, slimeTransform.position, Quaternion.identity);
        }

        if (deathParticles)
        {
            ParticleSystem newParticles = Instantiate(deathParticles, slimeTransform.position, Quaternion.identity);
            newParticles.Play();
            StartCoroutine(DestroyAfterDuration(newParticles.gameObject));
        }
    }

    private IEnumerator DestroyAfterDuration(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}

