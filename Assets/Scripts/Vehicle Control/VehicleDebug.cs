using System.Collections;
using static InputBase;
using UnityEngine;

namespace RVP
{
    [DisallowMultipleComponent]
    [AddComponentMenu("RVP/Vehicle Controllers/Vehicle Debug", 3)]

    // Class for easily resetting vehicles
    public class VehicleDebug : MonoBehaviour
    {
        public Vector3 spawnPos;
        public Vector3 spawnRot;

        [Tooltip("Y position below which the vehicle will be reset")]
        public float fallLimit = -10;

        private void Awake()
        {
            input.Car.ResetPos.performed += context => StartCoroutine(ResetRotation());
            input.Car.ResetPos.performed += context => StartCoroutine(ResetPosition());
        }

        private void OnDisable()
        {
            input.Car.ResetPos.performed -= context => StartCoroutine(ResetRotation());
            input.Car.ResetPos.performed -= context => StartCoroutine(ResetPosition());
        }

        // This waits for the next fixed update before resetting the rotation of the vehicle
        IEnumerator ResetRotation()
        {
            if (GetComponent<VehicleDamage>())
            {
                GetComponent<VehicleDamage>().Repair();
            }

            yield return new WaitForFixedUpdate();
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            transform.Translate(Vector3.up, Space.World);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        // This waits for the next fixed update before resetting the position of the vehicle
        IEnumerator ResetPosition()
        {
            if (GetComponent<VehicleDamage>())
            {
                GetComponent<VehicleDamage>().Repair();
            }

            transform.position = spawnPos;
            yield return new WaitForFixedUpdate();
            transform.rotation = Quaternion.LookRotation(spawnRot, GlobalControl.worldUpDir);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}