using UnityEngine;

namespace Pong.Scripts
{
    public class Paddle : MonoBehaviour
    {
        public float speed = 10f; // Speed of the paddle movement
        public float offset = 0.5f;
        protected float Offset;
        protected Rigidbody2D Rb;
        protected Vector2 Movement;
        protected AdjustScreenPosition.Border Border;
        private AdjustScreenPosition _adjust;
    
        void Awake()
        {
            Utility.Log("Paddle Awake");
            Rb = GetComponent<Rigidbody2D>();
            if (Camera.main) _adjust = Camera.main.GetComponent<AdjustScreenPosition>();
            if (!_adjust)
            {
                Debug.LogWarning("No AdjustScreenPosition found in the scene.");
            }
        }

        void Start()
        {
            Utility.Log("Paddle Start");
        }

        protected virtual void ResetPosition()
        {
            // Reset the paddle's position to the center of the screen
            if (_adjust) transform.position = _adjust.Middle(Border, offset);
            Rb.position = new Vector2(Rb.position.x, 0);
        }
    }
}
