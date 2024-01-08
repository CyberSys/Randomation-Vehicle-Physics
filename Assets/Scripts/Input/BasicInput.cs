using UnityEngine.InputSystem;
using static InputBase;
using UnityEngine;

namespace RVP
{
    [RequireComponent(typeof(VehicleParent))]
    [DisallowMultipleComponent]
    [AddComponentMenu("RVP/Input/Basic Input", 0)]

    // Class for setting the input with the input manager
    public class BasicInput : MonoBehaviour
    {
        private VehicleParent vp;

        public string[] inputs;
        public string[] methods;
        public string[] inputsBoolean;
        public string[] methodsBoolean;
        public InputAction[] booleanActions;

        private void Awake()
        {
            vp = GetComponent<VehicleParent>();
        }

        private void OnEnable() => EnableAllInputs();

        private void OnDisable() => DisableAllInputs();

        #region INPUT_HANDLING

        private void EnableAllInputs()
        {
            booleanActions = new InputAction[inputsBoolean.Length];
            for (int i = 0; i < inputsBoolean.Length; i++)
            {
                booleanActions[i] = input.FindAction(inputsBoolean[i]);
                EnableBoolInput(i, methodsBoolean[i]);
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                EnableInput(inputs[i], methods[i]);
            }
        }

        private void DisableAllInputs()
        {
            for (int i = 0; i < inputsBoolean.Length; i++)
            {
                DisableBoolInput(i, methodsBoolean[i]);
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                DisableInput(inputs[i], methods[i]);
            }
        }

        private void EnableInput(string inputName, string method)
        {
            input.FindAction(inputName).performed += context => vp.SendMessage(method, context.ReadValue<float>());
            input.FindAction(inputName).canceled += context => vp.SendMessage(method, context.ReadValue<float>());
        }

        private void EnableBoolInput(int index, string method)
        {
            booleanActions[index].performed += context => vp.SendMessage(method, true);
            booleanActions[index].canceled += context => vp.SendMessage(method, false);
        }

        private void DisableInput(string inputName, string method)
        {
            input.FindAction(inputName).performed -= context => vp.SendMessage(method, context.ReadValue<float>());
            input.FindAction(inputName).canceled -= context => vp.SendMessage(method, context.ReadValue<float>());
        }

        private void DisableBoolInput(int index, string method)
        {
            booleanActions[index].performed -= context => vp.SendMessage(method, true);
            booleanActions[index].canceled -= context => vp.SendMessage(method, false);
        }

        #endregion
    }
}