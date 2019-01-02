using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 offset = new Vector3(0, 5.0f, -10.0f);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = lookAt.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0;
        desiredPosition.y = offset.y;
        transform.position = Vector3.Lerp (transform.position, desiredPosition, (Time.deltaTime*4)); 
    }
}
