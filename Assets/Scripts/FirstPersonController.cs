using UnityEngine;
using Cinemachine;

public class FirstPersonController : MonoBehaviour
{
    public Transform Joueur;
    public CinemachineVirtualCamera virtualCamera;
    public float mouseSensitivity = 2f;

    private float cameraVerticalRotation = 0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        var cinemachinePOV = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        if (cinemachinePOV != null)
        {
            cinemachinePOV.m_HorizontalAxis.Value += inputX;
            cinemachinePOV.m_VerticalAxis.Value -= inputY;
        }
        else
        {
            Debug.LogError("missing");
        }
    }
}
