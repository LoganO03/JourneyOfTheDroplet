using System;
using System.Linq;
using UnityEngine;

public class ParallaxParentStage2 : MonoBehaviour
{

    public Sprite[] backgrounds;

    public float backgroundSize;

    void Awake()
    {
        for (int i = 0; i < backgrounds.Length; i++) {
            GameObject go = new GameObject("Layer" + i);
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(0, 0, 0);
            ParallaxScrollStage2 ps = go.AddComponent<ParallaxScrollStage2>();
            ps.sprite = backgrounds[i];
            ps.backgroundSize = backgroundSize;
            ps.sortOrder = backgrounds.Length - i;
            ps.parallaxSpeed = (1.0f + i) / backgrounds.Length;
            ps.Setup();
        }   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}