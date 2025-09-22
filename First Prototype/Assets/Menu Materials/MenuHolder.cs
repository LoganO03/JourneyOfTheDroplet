using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHolder : MonoBehaviour
{
    private List<GameObject> tabs;
    GameObject controlWidgets;
    GameObject settingWidgets;
    public int ActiveTab = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tabs = new List<GameObject>();
        controlWidgets = gameObject.GetComponentInChildren<ControlIdentifier>().gameObject;
        settingWidgets = gameObject.GetComponentInChildren<SettingsIdentifier>().gameObject;
        tabs.Add(controlWidgets);
        tabs.Add(settingWidgets);
        settingWidgets.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeTab(int tabNum)
    {
        Debug.Log("Calling tab change to tab" + tabNum);
        if (ActiveTab != tabNum && tabs.Count >= tabNum)
        {
            Debug.Log("Changing tabs");
            tabs[ActiveTab].SetActive(false);
            ActiveTab = tabNum;
            tabs[ActiveTab].SetActive(true);

        }
        else
        {
            Debug.Log("Note: ActiveTab is " + ActiveTab + " and tabNum is " + tabNum + ". The number of tabs is " + tabs.Count + ".");
        }
    }
}
