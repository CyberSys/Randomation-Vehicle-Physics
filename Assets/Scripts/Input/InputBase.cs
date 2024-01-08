using UnityEngine;

public class InputBase : MonoBehaviour
{
    public static Controls input;

    // Start is called before the first frame update
    private void Awake()
    {
        input = new Controls();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public static void DisableInput() => input.Disable();

    public static void EnableInput() => input.Enable();

    public static void SetInputEnabled(bool enabled)
    {
        if (enabled)
        {
            input.Enable();
        }
        else
        {
            input.Disable();
        }
    }
}
