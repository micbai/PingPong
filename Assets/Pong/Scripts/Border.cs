using UnityEngine;

namespace Pong.Scripts
{
    public class Border : MonoBehaviour
    {
        [SerializeField] private AdjustScreenPosition.Border border = AdjustScreenPosition.Border.Top;
        [SerializeField] float offset = 0.5f;
    
        private AdjustScreenPosition _adjust;
    
        void Awake()
        {
            if (Camera.main) _adjust = Camera.main.GetComponent<AdjustScreenPosition>();
            if (!_adjust)
            {
                Debug.LogWarning("No AdjustScreenPosition found in the scene.");
            }
        }
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartPosition();
        }

        private void StartPosition()
        {
            if (_adjust) transform.position = _adjust.Middle(border, offset);
        }
    }
}
