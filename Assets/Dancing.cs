using UnityEngine;

public class Dancing : MonoBehaviour
{
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetBool("IsDancing", true);
            Debug.Log("danse.");

        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            animator.SetBool("IsDancing", false);
        }

    }

}
