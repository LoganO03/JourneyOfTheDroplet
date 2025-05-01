using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ShootWater : MonoBehaviour
{

    public GameObject pausepanel;
    //have existing objects in a queue instead of creating

    private List<GameObject> waterList = new List<GameObject>();
    public GameObject waterShot;

    private Rigidbody2D waterBody;
    private Rigidbody2D player;

    [SerializeField] float waterVolume;
    [SerializeField] float minCharacterScale;
    [SerializeField] float drainRate;

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
            if (!pausepanel.activeInHierarchy && GameManager.Instance.playerWater > 0.001f){
                Shoot();
                GameManager.Instance.playerWater -= Mathf.Min(waterVolume, GameManager.Instance.playerWater) * drainRate * player.transform.localScale.x;
            }
            //Debug.Log("Shooting");
        }
        float volume = (GameManager.Instance.playerWater * 3) / (Mathf.PI * 4);
        transform.localScale = new Vector3(Mathf.Max(Mathf.Pow(volume, 0.33333f), minCharacterScale), Mathf.Max(Mathf.Pow(volume, 0.33333f), minCharacterScale), 1);
    }

    void Shoot(){
        Vector2 spawn = player.transform.position;
        Vector2 goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // GameObject water = Instantiate(waterShot, spawn, Quaternion.identity);
        //Debug.Log("Adding Force: " + ((goal - spawn) * moveForce));
        GameObject w = waterList[pos];
        w.SetActive(false);
        w.transform.position = spawn;
        float volume = Mathf.Min(GameManager.Instance.playerWater, waterVolume) / drainRate;
        float radius = Mathf.Pow(volume / Mathf.PI, 0.5f);

        w.transform.localScale = new Vector3(radius * player.transform.localScale.x, radius * player.transform.localScale.y, volume / (radius * player.transform.localScale.y * radius * player.transform.localScale.y * Mathf.PI));
        w.SetActive(true);
        waterBody = w.GetComponent<Rigidbody2D>();
        waterBody.totalForce = new Vector2(0,0);
        waterBody.AddForce((goal - spawn) * moveForce);
        pos ++;
    }
}
