using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 offset = new Vector3(0, 5.0f, -10.0f);
    public Vector3 rotation = new Vector3(35, 0, 0);

    public bool IsMoving { set; get; }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!IsMoving)
        {
            return;
        }

        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0;
        desiredPosition.y = offset.y;
        transform.position = Vector3.Lerp (transform.position, desiredPosition, 0.1f);
        if (!transform.rotation.Equals(rotation))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
        }
    }
}
