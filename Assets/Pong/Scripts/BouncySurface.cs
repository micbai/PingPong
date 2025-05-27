using UnityEngine;

namespace Pong.Scripts
{
    public class BouncySurface : MonoBehaviour
    {
        [SerializeField] private float bounceForce = 10f; // The force with which the ball will bounce
        [SerializeField] private AudioClip bounceSound;
        private AudioSource _audioSource;
    
        private void Start()
        {
            _audioSource = FindFirstObjectByType<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogWarning("No AudioSource found in the scene.");
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Check if the object colliding with the surface is a ball
            if (other.gameObject.CompareTag("Ball"))
            {
                // Get the Rigidbody2D component of the ball
                Ball ball = other.gameObject.GetComponent<Ball>();
                Vector2 normal = other.contacts[0].normal;
            
                // Check if the ball is colliding with the paddle
                if (this.CompareTag("Paddle"))
                {
                    // Get the point where the ball hits the paddle
                    Vector3 hitPoint = other.contacts[0].point;
                    Vector2 bounceDirection = (hitPoint - transform.position).normalized;
            
                    // Calculate angle to provide more dynamic bouncing
                    float angleOffset = Vector2.Dot(bounceDirection, Vector2.up) / 5f;
                    Vector2 modifiedBounceDirection = new Vector2(
                        bounceDirection.x * Mathf.Abs(angleOffset), 
                        Mathf.Sign(bounceDirection.y) * Mathf.Abs(angleOffset)
                    ).normalized;

                    // Apply the calculated bounce direction with the bounce force
                    ball.AddForce(modifiedBounceDirection * bounceForce);
                    float bounceDirectionAngle = Mathf.Atan2(bounceDirection.y, bounceDirection.x) * Mathf.Rad2Deg;
                    float modifiedBounceDirectionAngle = Mathf.Atan2(modifiedBounceDirection.y, modifiedBounceDirection.x) * Mathf.Rad2Deg;
                    Utility.Log($"Paddle hit at {hitPoint} with angle {bounceDirectionAngle} modified angle now is {modifiedBounceDirectionAngle}");
                }

                else
                {
                    ball.AddForce(-normal * bounceForce);
                }
                if (bounceSound && _audioSource) _audioSource.PlayOneShot(bounceSound);
            }
        }
    }
}