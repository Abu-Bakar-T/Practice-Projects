using UnityEngine;

public class BallCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] int ballIndex;

    void Start()
    {
        explosionFX = transform.GetChild(0).GetComponent<ParticleSystem>();
        ballIndex = transform.position.x > 0 ? 0 : 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
            GameManager.instance.isGameOver = true;
            explosionFX.Play();
            Splatters.instance.AddSplatter(collision.transform, collision.contacts[0].point, ballIndex);
            PlayerMovement.instance.Restart();
        }
    }
}
