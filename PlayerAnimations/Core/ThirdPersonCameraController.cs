using UnityEngine;

namespace PlayerAnimations.Core
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        private static readonly float DISTANCE_BEHIND_PLAYER_MIN = -0.5f;
        private static readonly float DISTANCE_BEHIND_PLAYER_MAX = -3f;
        private static readonly Vector2 THIRD_PERSON_CAMERA_OFFSET = new Vector2(0.2f, 0f);
        private static readonly float MOUSE_SCROLL_SPEED = 0.25f;

        private float currentDistanceBehindPlayer = 0f;

        public void OnEnable()
        {
            currentDistanceBehindPlayer = Mathf.Lerp(DISTANCE_BEHIND_PLAYER_MIN, DISTANCE_BEHIND_PLAYER_MAX, 0.5f);
            gameObject.transform.localPosition = new Vector3(THIRD_PERSON_CAMERA_OFFSET.x, THIRD_PERSON_CAMERA_OFFSET.y, currentDistanceBehindPlayer);
            Player.main.SetHeadVisible(true);
        }
        
        public void OnDisable()
        {
            gameObject.transform.localPosition = Vector3.zero;
            Player.main.SetHeadVisible(false);
        }

        public void Update()
        {
            currentDistanceBehindPlayer += Input.mouseScrollDelta.y * MOUSE_SCROLL_SPEED;
            currentDistanceBehindPlayer = Mathf.Min(currentDistanceBehindPlayer, DISTANCE_BEHIND_PLAYER_MIN);
            currentDistanceBehindPlayer = Mathf.Max(currentDistanceBehindPlayer, DISTANCE_BEHIND_PLAYER_MAX);
            gameObject.transform.localPosition = new Vector3(THIRD_PERSON_CAMERA_OFFSET.x, THIRD_PERSON_CAMERA_OFFSET.y, currentDistanceBehindPlayer);
        }
    }
}
