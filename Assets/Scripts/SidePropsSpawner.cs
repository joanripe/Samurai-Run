using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePropsSpawner : MonoBehaviour
{
    private const float DISTANCE_TO_RESPAWN = 10;

    public float scrollSpeed = -2f;
    public float totalLenght;
    public bool IsScrolling { set; get; }

    private float scrollLocation;
    private Transform playerTransform;
    private PlayerMotor playerMotor;

    // Start is called before the first frame update
    void Start()
    {
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsScrolling)
        {
            return;
        }
        scrollSpeed = -playerMotor.speed;
        scrollLocation += scrollSpeed * Time.deltaTime;
        Vector3 newLocation = (playerTransform.position.z + scrollLocation) * Vector3.forward;
        transform.position = newLocation;
        if (transform.GetChild(0).transform.position.z < ( playerTransform.position.z - DISTANCE_TO_RESPAWN))
        {
            RespawnChild();
            RespawnChild();
        }
    }

    private void RespawnChild()
    {
        transform.GetChild(0).localPosition += Vector3.forward * totalLenght;
        transform.GetChild(0).eulerAngles = Random.Range(0f, 360f) * Vector3.up;
        transform.GetChild(0).localScale = new Vector3(Random.Range(1f, 1.5f), Random.Range(1f, 2.5f), Random.Range(1f, 1.5f));
        transform.GetChild(0).SetSiblingIndex(transform.childCount);
    }
}
