using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CounterArray : MonoBehaviour
{
    [SerializeField] CounterDigit[] digits;
    [SerializeField] public float goalNumber;
    [SerializeField] bool useStaticUI;
    [SerializeField] TextMeshProUGUI NumberPanel;
    float prevGoal = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!NumberPanel.IsUnityNull()){
        if(useStaticUI) {
            foreach(CounterDigit digit in digits) {
                digit.disappear();
                digit.gameObject.SetActive(false);
            }
            NumberPanel.gameObject.SetActive(true);
        } else {
            NumberPanel.gameObject.SetActive(false);
            foreach(CounterDigit digit in digits) {
                digit.appear();
                digit.gameObject.SetActive(true);
            }}
        }
        /*
        int i = digits.Length - 1;
        while (i >= 0) {
            i -= 1;
        }
        i = (int) Mathf.Log(goalNumber, 10);
        while (i >= 0) {
            i -= 1;
        }*/
    }

    // Update is called once per frame
    void Update() {
        if (!NumberPanel.IsUnityNull()){
        if(!useStaticUI) {
            if(prevGoal != goalNumber) {
                int i = digits.Length - 1;
                float total = 0;
                while(i >= 0) {
                    total = total * 10 + digits[i].at();
                    i -= 1;
                }
                i = digits.Length - 1;
                while(i >= 0) {
                    float rotations = (goalNumber - total) / Mathf.Pow(10, i);
                    float goal = goalNumber / Mathf.Pow(10, i);
                    if(i > 0) { goal -= (goal % 1 - Mathf.Pow(goal % 1, 4)); rotations -= rotations % 1; }

                    digits[i].newGoal(rotations, goal % 10);
                    i -= 1;
                }
            }
            prevGoal = goalNumber;
            goalNumber += Time.deltaTime;
        } else {
            NumberPanel.text = $"{goalNumber:0.00}";
        }}
    }
}