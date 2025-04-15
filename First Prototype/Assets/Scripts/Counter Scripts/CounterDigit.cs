using UnityEngine;

public class CounterDigit : MonoBehaviour
{
    [SerializeField] CounterCube counter;
    [SerializeField] Transform wheel;
    [SerializeField] int digitIndex;
    public float rotationSpeed;
    public float slurpSpeed;
    public float goalNumber = 0;
    public float rotations = 0;
    float currentNumber = 0;
    
    float currentSlurp = 0;
    float currentSpeed = 0;
    int accelFlag = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }
    
    void Awake()
    {
        currentNumber = counter.displayedNum();
        if(Mathf.Pow(10, digitIndex) > goalNumber){
            counter.disappear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rotations != 0){
            if(currentNumber - counter.displayedNum() > 9){
                currentNumber -= 10;
            } else if(counter.displayedNum() - currentNumber > 9){
                currentNumber += 10;
            } else {
                rotations -= (currentNumber - counter.displayedNum());
            }
        }
        currentNumber = counter.displayedNum();
        if(currentNumber < goalNumber) {
            increment();
        } else if (currentNumber > goalNumber) {
            decrement();
        } else {
            stick();
        }
    }

    public float at() {
        if(digitIndex != 0) {
            return (int) (currentNumber + 0.1f) % 10;
        }
        return currentNumber;
    }

    void increment() {

        if(accelFlag != 1) {
            currentSlurp = 0;
            accelFlag = 1;
            currentSpeed = 0;
        } else {
            currentSlurp += (slurpSpeed - currentSlurp) * slurpSpeed;
            currentSpeed += (rotationSpeed - currentSpeed) * currentSlurp;
        }
        float angle = currentSpeed * Time.deltaTime / Mathf.Pow(10, digitIndex);
        wheel.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
    }
    
    void decrement() {
        if(accelFlag != -1) {
            currentSlurp = 0;
            currentSpeed = 0;
            accelFlag = -1;
        } else {
            currentSlurp += (slurpSpeed - currentSlurp) * slurpSpeed;
            currentSpeed += (rotationSpeed - currentSpeed) * currentSlurp;
        }
        float angle = -currentSpeed * Time.deltaTime / Mathf.Pow(10, digitIndex);
        wheel.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
    }

    void stick() {

    }
}
