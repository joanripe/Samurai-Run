using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;
    [SerializeField] private float TURN_SPEED = 0.05f;

    // animaciones
    private Animator anim;

    //movement
    private CharacterController controller;
 	[SerializeField] private float jumpForce = 4.0f;
	[SerializeField] private float gravity = 12.0f;
	[SerializeField] private float verticalVelocity;
	[SerializeField] private float speed = 7.0f;
    private int desiredLane = 1; // 0 = izquierda, 1 = en medio, 2 = derecha

	private void Start(){
		this.controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}

	private void Update(){

        // recoger la entrada para ver en que linea debemos estar
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);
        }

        // calcular donde debemos de estar en el futuro
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        } else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        // vamos a calcular el vector de movimiento
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - this.transform.position).normalized.x * speed;

        // calcular Y
        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        if (isGrounded) // si esta en el suelo
        {
            verticalVelocity = -0.1f;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // saltar
                anim.SetTrigger("Salto");
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            // mecanica de caida rapida
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //mover el personaje
        controller.Move(moveVector * Time.deltaTime);

        // rotar el personaje hacia donde esta moviendose
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
        
    }

    private void MoveLane(bool _toRight)
    {
        if (!_toRight)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        else
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(
            new Vector3(
                controller.bounds.center.x,
                (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
                controller.bounds.center.z),
            Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);
        return Physics.Raycast(groundRay, 0.2f + 0.1f); // en el suelo = true, saltando = false
    }
}
