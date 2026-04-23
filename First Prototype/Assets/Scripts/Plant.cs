using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Rendering;

public class Plant : MonoBehaviour
{
    public float growthRate = 0.1f;
    public float speed = 1;
    public bool ladder;
    public bool bounce;
    public bool platform;
    public bool tree;
    public float maxWidth;
    public float maxHeight;
    private AudioSource growingSound;
    private AudioSource finishGrowing;
    private int countdown = 10;
    private int countdown_base;
    private bool finishedplayed;
    private Animator animator;

    bool isClimbable = false;
    bool growing = false;

    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tree)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            sr = GetComponent<SpriteRenderer>();
        }
        finishedplayed = false;
        countdown_base = countdown;
        growing = true;
        finishGrowing = GameObject.FindWithTag("FinishedGrowing").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.SetBool("Wiggling", true);
        if (bounce)
            Debug.Log("I'm a mushroom!");
        {
            growingSound = GameObject.FindWithTag("GrowingMushroom").GetComponent<AudioSource>();
            }
        if (tree)
        {
            Debug.Log("I'm a tree!");
            growingSound = GameObject.FindWithTag("GrowingTree").GetComponent<AudioSource>();
        }
        if (ladder)
        {
            Debug.Log("I'm a vine!");
            growingSound = GameObject.FindWithTag("GrowingVine").GetComponent<AudioSource>();
        }
        if (!ladder && !tree && !bounce)
        {
            Debug.Log("Error: Plant type undefined");
        }
    }


    //col.gameObject.GetComponent<PlayerMovement>().StartClimb();

    public void Grow()
    {
        if (!growing)
        {
            growing = true;
            animator.SetBool("Wiggling", false);
        }
        
        if (sr.size.y < maxHeight)
        {
            if (growingSound != null && !growingSound.isPlaying)
            {
                Debug.Log("Growing");
                growingSound.Play();
            }
        }
        
        if (ladder)
        {
            sr.size += new Vector2(0, growthRate);
            if (sr.size.y >= maxHeight)
            {
                growing = false;
                sr.size = new Vector2(sr.size.x, maxHeight);
                GetComponent<Collider2D>().isTrigger = true;
                GetComponent<Ladder>().enabled = true;
                transform.Find("TopLadder").position = new Vector3(transform.position.x, (transform.position.y + maxHeight) - 0.1f, 1);
            }

        }
        else if (bounce)
        {
            //when mushroom is right width, jump pad is enabled
            if (transform.localScale.x >= maxWidth)
            {
                growing = false;
                transform.localScale = new Vector2(maxWidth, transform.localScale.y);
                GetComponent<JumpPad>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;


            }
            else if (transform.localScale.y >= maxHeight)
            {
                transform.localScale = new Vector3(transform.localScale.x + growthRate, maxHeight, 1);
            }
            else
            {
                transform.localScale += new Vector3(growthRate, growthRate, 0);
                transform.position += new Vector3(0, -(growthRate / 3), 0);


            }
        }
        else if (tree)
        {
            // it is a tree
            if (transform.localScale.x >= 1 || transform.localScale.y >= 1)
            {
                if (GetComponent<BoxCollider2D>().excludeLayers == LayerMask.GetMask("Nothing"))
                {
                    GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Player");
                }
                transform.localScale = new Vector2(1, 1);
                GetComponent<TreeGrow>().GrowTree(growthRate, maxWidth, maxHeight);
                growing = false;
            }
            else
            {
                transform.localScale += new Vector3(growthRate, growthRate, 0);
                transform.position += new Vector3(0, growthRate, 0);
            }
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        
        if ((growingSound != null) && (growingSound.isPlaying && !growing))
        {
            
            
            if (countdown > 0)
            {
                Debug.Log(countdown);
                countdown--;
            }
            else
            {
                Debug.Log("Growing stop");
                growingSound.Stop();
                countdown = countdown_base;
            }
        }
        if (!growing && !finishedplayed)
        {
            finishGrowing.Play();
            finishedplayed = true;
            
        }
    }
}
