using UnityEngine;
using System.Collections;
using static InputBase;

namespace RVP
{
    [DisallowMultipleComponent]
    [AddComponentMenu("RVP/Camera/Basic Camera Input", 1)]

    // Class for setting the camera input with the input manager
    public class BasicCameraInput : MonoBehaviour
    {
        CameraControl cam;
        float xInput;
        float yInput;

        void Start()
        {
            // Get camera controller
            cam = GetComponent<CameraControl>();
            input.Camera.Yaw.performed += context => xInput = context.ReadValue<float>();
            input.Camera.Pitch.performed += context => yInput = context.ReadValue<float>();

            input.Camera.Yaw.canceled += context => xInput = context.ReadValue<float>();
            input.Camera.Pitch.canceled += context => yInput = context.ReadValue<float>();
        }

        private void OnDisable()
        {
            input.Camera.Yaw.performed -= context => xInput = context.ReadValue<float>();
            input.Camera.Pitch.performed -= context => yInput = context.ReadValue<float>();

            input.Camera.Yaw.canceled -= context => xInput = context.ReadValue<float>();
            input.Camera.Pitch.canceled -= context => yInput = context.ReadValue<float>();
        }

        void FixedUpdate()
        {
            // Set camera rotation input if the input axes are valid
            if (cam) cam.SetInput(xInput, yInput);
        }
    }
}