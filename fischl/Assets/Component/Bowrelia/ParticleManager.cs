using System.Collections;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticles;

    private void OnEnable()
    {
        Slime.OnDeath += HandleDeathParticle;
    }

    private void HandleDeathParticle(Transform slimeTransform)
    {
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

