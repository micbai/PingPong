using UnityEngine;

namespace Pong.Scripts
{
    public class AdjustScreenPosition : MonoBehaviour
    {
        public enum Border
        {
            Left,
            Right,
            Top,
            Bottom
        }
        private Camera _mainCamera;
        private Vector2 _screenBounds;
        void Awake()
        {
            _mainCamera = Camera.main;
            CalculateScreenBounds();
        }

        public Vector3 Middle(Border border, float offset)
        {
            Vector3 middle = Vector3.zero;
            if (_mainCamera == null) return middle;
            if (border == Border.Left)
                middle = _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, _mainCamera.nearClipPlane)) + new Vector3(offset, 0, 0);
            else if (border == Border.Right)
                middle = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, _mainCamera.nearClipPlane)) + new Vector3(offset, 0, 0);
            else if (border == Border.Top)
                middle = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, _mainCamera.nearClipPlane)) + new Vector3(0, offset, 0);
            else if (border == Border.Bottom) 
                return _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, _mainCamera.nearClipPlane)) + new Vector3(0, offset, 0);
            middle.z = 0;
            Utility.Log($"Middle {middle}");
            return middle;
        }
    
        void CalculateScreenBounds()
        {
            if (_mainCamera == null) return;
    
            // Convert screen bounds to world coordinates
            _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }

        /// <summary>
        /// Visualizes screen boundaries within the Unity scene view by drawing gizmos.
        /// This method is called by Unity and is useful for debugging purposes.
        /// Specifically, it draws the top border in red, the left border in blue, and the
        /// right border in green based on screen bounds. The visualization helps understand
        /// the screen positioning of objects relative to the current camera view.
        /// </summary>
        void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            // Top border
            Vector3 topLeft = new Vector3(-_screenBounds.x, _screenBounds.y, 0);
            Vector3 topRight = new Vector3(_screenBounds.x, _screenBounds.y, 0);
    
            // Left border
            Vector3 leftTop = new Vector3(-_screenBounds.x, _screenBounds.y, 0);
            Vector3 leftBottom = new Vector3(-_screenBounds.x, -_screenBounds.y, 0);
    
            // Right border
            Vector3 rightTop = new Vector3(_screenBounds.x, _screenBounds.y, 0);
            Vector3 rightBottom = new Vector3(_screenBounds.x, -_screenBounds.y, 0);

            // Draw borders with different colors for clarity
            Gizmos.color = Color.red;    // Top border
            Gizmos.DrawLine(topLeft, topRight);
    
            Gizmos.color = Color.blue;   // Left border
            Gizmos.DrawLine(leftTop, leftBottom);
    
            Gizmos.color = Color.green;  // Right border
            Gizmos.DrawLine(rightTop, rightBottom);
        }
    }
}
