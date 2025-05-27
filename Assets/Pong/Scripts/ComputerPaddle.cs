using UnityEngine;

namespace Pong.Scripts
{
    public class ComputerPaddle : Paddle
    {
        [SerializeField] private Rigidbody2D ball; // Reference to the ball's Rigidbody2D component
    
        void Start()
        {
            Debug.Log("ComputerPaddle Start");
            Rb = GetComponent<Rigidbody2D>();
            Border = AdjustScreenPosition.Border.Right;
            ResetPosition();
        }
    
        void FixedUpdate()
        {
            Move();   
        }

        private void Move()
        {
            Movement = Vector2.zero;
            // if the ball movement is in direction of the Computer paddle follow the ball, ohterwise move to the center
            if (ball.linearVelocityX > 0)
            {
                if (ball.position.y > Rb.position.y)
                {
                    Movement = Vector2.up;
                }
                else if (ball.position.y < Rb.position.y)
                {
                    Movement = Vector2.down;
                }
            }
            else
            {
                if (Rb.position.y > 0)
                {
                    Movement = Vector2.down;
                }
                else if (Rb.position.y < 0)
                {
                    Movement = Vector2.up;
                }
            }
            // Move the paddle usinf force
            Rb.AddForce(Movement * speed); // or add force to the rigid body
        }
    }
}
