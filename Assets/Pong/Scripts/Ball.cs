using UnityEngine;

namespace Pong.Scripts
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 2f; // Speed of the ball
        [SerializeField] private float maxSpeed = 25f;
        private Rigidbody2D _rb;
        private float _limitAngle = 30f; // 
    
        void Awake()
        {
            // Initialize the ball's Rigidbody2D component
            _rb = GetComponent<Rigidbody2D>();
        }
    
        public void Restart()
        {
            SetStartPosition();
            SetStartForce();
        }

        public void SetStartPosition()
        {
            // Reset the ball's position to the center of the screen
            _rb.position = Vector2.zero;
            // Reset the ball's velocity to zero
            _rb.linearVelocity = Vector2.zero;
            // Reset the ball's rotation to zero
            _rb.angularVelocity = 0;
        }

        public void AddForce(Vector2 force, float weight = 0)
        {
            // Add force to the ball
            _rb.AddForce(force, ForceMode2D.Impulse);
        
            // Clamp the velocity of the ball to the min/max speed
            Vector2 velocity = _rb.linearVelocity;
            var clampedDirection = ClampDirection(velocity, _limitAngle);
        
            // Apply the clamped speed and direction
            _rb.linearVelocity = clampedDirection * Mathf.Clamp(_rb.linearVelocity.magnitude, initialSpeed, maxSpeed);
            float directionAngleInDegrees = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            float clampedDirectionAngleInDegrees = Mathf.Atan2(clampedDirection.y, clampedDirection.x) * Mathf.Rad2Deg;
            Utility.Log($"Direction {directionAngleInDegrees} clamped {clampedDirectionAngleInDegrees} speed {_rb.linearVelocity.magnitude} and rotation {_rb.rotation}");
        }

        private void SetStartForce()
        {
            // direction should be random between 0 degree +/- 30 degrees and 180 degrees +/- 30 degrees
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-0.3f, 0.3f);
            var direction = new Vector2(x, y);
            direction = ClampDirection(direction, _limitAngle);
        
            // Set the ball's initial velocity
            _rb.AddForce(direction * initialSpeed, ForceMode2D.Impulse);
            if (_rb.linearVelocity.magnitude < initialSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * initialSpeed;
            }
        }
    
        private Vector2 ClampDirection(Vector2 velocity, float degree)
        {
            degree = Mathf.Abs(degree);
        
            // Get the current angle in degrees
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
    
            // If moving right (positive x)
            if (velocity.x > 0)
            {
                angle = Mathf.Clamp(angle, -degree, degree);
            }
            // If moving left (negative x)
            else if (velocity.x < 0)
            {
                if (angle > 0)
                    angle = Mathf.Max(angle, 180 - degree);
                else
                    angle = Mathf.Min(angle, -180 + degree);
            }
    
            // Convert angle back to a direction vector
            float angleRad = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            return direction;
        }
    }
}
