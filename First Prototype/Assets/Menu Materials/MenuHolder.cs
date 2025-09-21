using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHolder : MonoBehaviour
{
    private List<GameObject> tabs;
    public GameObject controlWidgets;
    public GameObject settingWidgets;
    public int ActiveTab = 0;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tabs.Add(controlWidgets);
        tabs.Add(settingWidgets);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeTab(int tabNum)
    {
        if (ActiveTab != tabNum && tabs.Count <= tabNum)
        {
            tabs[ActiveTab].SetActive(false);
            ActiveTab = tabNum;
            tabs[ActiveTab].SetActive(true);

        }
    }
}
