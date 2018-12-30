using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.GetCoin();
            anim.SetTrigger("Collected");
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetTrigger("Spawn");
    }
}
