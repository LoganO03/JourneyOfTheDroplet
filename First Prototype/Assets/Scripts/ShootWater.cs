using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ShootWater : MonoBehaviour
{

    //have existing objects in a queue instead of creating

    private List<GameObject> waterList = new List<GameObject>();
    public GameObject waterShot;

    private Rigidbody2D waterBody;
    private Rigidbody2D player;

    public int moveForce;

    private int pos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
        for(int i = 0; i <= 200; i++){
            GameObject water = Instantiate(waterShot, new Vector2(0,0), Quaternion.identity);
            water.SetActive(false);
            waterList.Add(water);

        }
        player = GetComponent<Rigidbody2D>();
        //waterBody = waterShot.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = pos%201;
         if (Input.GetMouseButton(0))
        {
            //Debug.Log("Shooting");
            Shoot();
        }
        
    }

    void Shoot(){
        Vector2 spawn = player.transform.position;
        Vector2 goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // GameObject water = Instantiate(waterShot, spawn, Quaternion.identity);
        //Debug.Log("Adding Force: " + ((goal - spawn) * moveForce));
        GameObject w = waterList[pos];
        w.SetActive(false);
        w.transform.position = spawn;
        w.SetActive(true);
        waterBody = w.GetComponent<Rigidbody2D>();
        waterBody.totalForce = new Vector2(0,0);
        waterBody.AddForce((goal - spawn) * moveForce);
        pos ++;
        

    }

    
}
