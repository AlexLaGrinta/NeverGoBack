using UnityEngine;
using Cinemachine;

public class FirstPersonController : MonoBehaviour
{
    public Transform Joueur;
    public CinemachineVirtualCamera virtualCamera;
    public float mouseSensitivity = 2f;
    private Transform cameraTransform;


    public Transform character;
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
            if (character != null)
            {
                float horizontalAngle = cinemachinePOV.m_HorizontalAxis.Value;
                float verticalAngle = cinemachinePOV.m_VerticalAxis.Value;

                // Construire une rotation à partir de ces angles
                Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

                // Obtenir le vecteur de direction (forward) de la caméra
                Vector3 cameraDirection = rotation * Vector3.forward;
                cameraDirection.y = 0;
                if (cameraDirection != Vector3.zero)
                {
                    character.rotation = Quaternion.LookRotation(cameraDirection);
                }
            }
        }
        else
        {
            Debug.LogError("missing");
        }


    }
}
