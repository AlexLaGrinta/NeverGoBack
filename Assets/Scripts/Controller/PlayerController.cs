using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;  // Référence unique à InputManager
    private Transform cameraTransform;

    private Animator anim;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float sprintSpeed = 4.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float friction = 5.0f; // Friction (décélération) pour ralentir le joueur
    [SerializeField]
    private float groundCheckDistance = 0.2f; // Distance de vérification du sol

    private Vector3 velocity; // Utiliser velocity pour gérer la vitesse du joueur

    private bool isJumping;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance; // Récupère l'instance du InputManager déjà existante
        cameraTransform = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Amélioration de la détection du sol avec un raycast
        groundedPlayer = controller.isGrounded;
        if (!groundedPlayer)
        {
            // Double vérification avec un raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
            {
                groundedPlayer = true;
            }
        }

        // Réinitialise la vitesse verticale si on est au sol
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Récupère les entrées de mouvement (horizontal et vertical)
        Vector2 movement = inputManager.GetPlayerMovement(); // Récupère la référence sans la réinstancier
        Vector3 move = new Vector3(movement.x, 0f, movement.y); // Création d'un vecteur 3D

        // Applique l'orientation de la caméra au mouvement
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;

        // Normalisation pour éviter une vitesse plus rapide en diagonale
        move.y = 0f;
        move = move.normalized;

        //bool isMoving = move.magnitude > 0.1f;
        //if (isMoving)
        //{
        //    anim.SetFloat("Speed", 1f);
        //} else
        //{
        //    anim.SetFloat("Speed", 0f);
        //}
        float movementMagnitude = Mathf.Clamp01(move.magnitude);
        anim.SetFloat("Speed", movementMagnitude);
        anim.SetBool("isRunning", movementMagnitude > 0);

        //animator.SetBool("isRunning", move.magnitude > 0);

        // Ajout d'une friction pour ralentir le joueur quand il ne bouge pas
        if (groundedPlayer)
        {
            if (move.magnitude > 0)
            {
                velocity = Vector3.Lerp(velocity, move * playerSpeed, Time.deltaTime * friction); // Décélération
            }
            else
            {
                velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * friction); // Décélération à zéro
            }
        }
        else
        {
            // En l'air, on conserve la vitesse horizontale
            velocity.x = move.x * playerSpeed;
            velocity.z = move.z * playerSpeed;
        }

        // Applique le mouvement horizontal
        controller.Move(velocity * Time.deltaTime);

        // Gére le saut
        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer)
        {
            anim.SetBool("isJumping", true);
            isJumping = true;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue); // Applique la force de saut
            bool value = anim.GetBool("isJumping");
            Debug.Log(value);
        }
        else if (groundedPlayer)
        {
            anim.SetBool("onFloor", true);
            anim.SetBool("isJumping", false);
            isJumping = false;
            anim.SetBool("isFalling",false);
        } else
        {
            anim.SetBool("onFloor", false);
            if (isJumping)
            {
                anim.SetBool("isFalling",true);
            }
        }

        // Applique la gravité pour le mouvement vertical
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Applique la gravité
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
