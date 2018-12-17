using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;
    
	//movement
	private CharacterController controller;
 	[SerializeField] private float jumpForce = 4.0f;
	[SerializeField] private float gravity = 12.0f;
	[SerializeField] private float verticalVelocity;
	[SerializeField] private float speed = 7.0f;
	private int desiredLane = 1; // 0 = izquierda, 1 = en medio, 2 = derecha

	private void Start(){
		this.controller = GetComponent<CharacterController>();
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
        moveVector.y = -0.1f;
        moveVector.z = speed;

        //mover el personaje
        controller.Move(moveVector * Time.deltaTime);
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
}
