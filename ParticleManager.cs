using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject particlePrefab; // Ensure this is public
    public int numberOfParticles = 100;

    void Start()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Instantiate(particlePrefab, RandomPosition(), Quaternion.identity);
        }
    }

    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
    }
}
