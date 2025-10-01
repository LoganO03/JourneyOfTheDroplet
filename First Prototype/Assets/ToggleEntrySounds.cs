using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ToggleDripping : MonoBehaviour
{
    public GameObject player;
    public List<StalactiteDrip> stalactiteDrips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            foreach (StalactiteDrip dripper in stalactiteDrips)
            {
                dripper.dripping.mute = false;
            }
        player.GetComponent<PlayerMovement>().enteredCave = true;
    }
}
