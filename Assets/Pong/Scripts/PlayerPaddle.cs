using System;
using UnityEngine;

namespace Pong.Scripts
{
    public class PlayerPaddle : Paddle
    {
        void Start()
        {
            Debug.Log("PlayerPaddle Start");
            Border = AdjustScreenPosition.Border.Left;
            ResetPosition();
        }
    
        void FixedUpdate()
        {
            Move();   
        }

        private void Move()
        {
            float y = Input.GetAxis("Vertical");
            // Don't move if the player is not pressing the up or down arrow key
            if (!(Math.Abs(y) > Mathf.Epsilon)) return;
            Movement = new Vector2(0, y);
            // Move the paddle
            Rb.AddForce(Movement * speed); // or add force to the rigid body
        }
    }
}
