using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{

    public float backgroundSize;
    public float parallaxSpeed;

    public int sortOrder;
    public Sprite sprite;

    private Transform cameraTransform;
    private Vector3 oldTransform;

    private GameObject emptyGameObject;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    private float lastCameraX;
    private float lastCameraY;


    void Awake()
    {
        //Setup();
    }

    public void Setup()
    {
        cameraTransform = Camera.main.transform;
        oldTransform = cameraTransform.localScale;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
        layers = new Transform[3];

        for (int i = 0; i < 3; i++)
        {
            GameObject go = new GameObject(); 
            go.transform.position = transform.position + new Vector3((i - 1) * backgroundSize, 0, 0);
            go.transform.parent = transform;
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = sortOrder;
            spriteRenderer.sortingLayerName = "Background";
            go.name = i.ToString();
            layers[i] = go.transform;
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
        
    }

    private void ScrollLeft()
    {
        layers[rightIndex].localPosition = Vector3.right * (layers[leftIndex].localPosition.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight()
    {
        layers[leftIndex].localPosition = Vector3.right * (layers[rightIndex].localPosition.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale += (cameraTransform.localScale - oldTransform) * parallaxSpeed;
        //oldTransform = cameraTransform.localScale;
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * parallaxSpeed);
        lastCameraX = cameraTransform.position.x;
        float deltaY = cameraTransform.position.y - lastCameraY;
        transform.position += Vector3.up * (deltaY * parallaxSpeed) * 0; // get rid of 0 for vertical parrallax
        lastCameraY = cameraTransform.position.y;

        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
    }
}