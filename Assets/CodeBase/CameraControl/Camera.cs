using UnityEngine;

namespace CodeBase.CameraControl
{
    public class Camera : MonoBehaviour
    {
        [SerializeField]
        private float cameraSpeed;

        private void LateUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            transform.position += new Vector3(h, 0, v) * Time.deltaTime * cameraSpeed;
        }
    }
}