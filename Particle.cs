using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector2 velocity;
    private float radius;
    private float mass;

    void Start()
    {
        // Initialize with a random velocity
        velocity = Random.insideUnitCircle.normalized * Random.Range(2.0f, 5.0f);
        // Assume the particle has a circular collider
        radius = GetComponent<CircleCollider2D>().radius;
        // Set a default mass
        mass = 1.0f;
    }

    void Update()
    {
        // Move the particle
        transform.Translate(velocity * Time.deltaTime);

        // Handle screen boundary collisions
        Vector2 position = transform.position;
        if (position.x > 8.0f || position.x < -8.0f)
        {
            velocity.x = -velocity.x;
            position.x = Mathf.Clamp(position.x, -8.0f, 8.0f);
        }
        if (position.y > 4.0f || position.y < -4.0f)
        {
            velocity.y = -velocity.y;
            position.y = Mathf.Clamp(position.y, -4.0f, 4.0f);
        }
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Particle"))
        {
            Particle other = collision.gameObject.GetComponent<Particle>();
            if (other != null)
            {
                // Calculate new velocities using elastic collision equations
                Vector2 normal = (other.transform.position - transform.position).normalized;
                Vector2 relativeVelocity = other.velocity - velocity;
                float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

                if (velocityAlongNormal > 0)
                    return;

                float e = 1.0f; // Coefficient of restitution (1 for elastic collision)
                float j = -(1 + e) * velocityAlongNormal;
                j /= 1 / mass + 1 / other.mass;

                Vector2 impulse = j * normal;
                velocity -= impulse / mass;
                other.velocity += impulse / other.mass;
            }
        }
    }
}
