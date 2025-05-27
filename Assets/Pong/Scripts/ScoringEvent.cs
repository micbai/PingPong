using UnityEngine;
using UnityEngine.EventSystems;

namespace Pong.Scripts
{
    public class ScoringEvent : MonoBehaviour
    {
        public EventTrigger.TriggerEvent scoreEventTrigger; // The event to trigger when the score is updated

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball)
            {
                // Trigger the event for the computer score
                scoreEventTrigger.Invoke(new BaseEventData(EventSystem.current));
            }
        }
    }
}
