using UnityEngine;

namespace Pong.Scripts
{
    public static class Utility
    {
        public static bool EnableLog { get; set; }
        
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(string message)
        {
            if (EnableLog) Debug.Log(message);
        }
    }
}
