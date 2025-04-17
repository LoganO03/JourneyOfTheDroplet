using UnityEngine;

public class CounterDigit : MonoBehaviour
{
    [SerializeField] CounterCube counter;
    [SerializeField] Transform wheel;
    [SerializeField] int digitIndex;
    float goalNumber = 0;
    float rotations = 0;
    float currentNumber = 0;
    [SerializeField] float maxDegreeDiff = 150;
    
    float currentSlurp = 0;
    float currentSpeed = 0;
    int accelFlag = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //maxDegreeDiff *= Mathf.PI / 180;
    }
    
    void Awake()
    {
        currentNumber = counter.displayedNum();
        if(Mathf.Pow(10, digitIndex) > goalNumber){
            //counter.disappear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(counter.displayedNum() > 7 && currentNumber < 2) {
            rotations -= (counter.displayedNum() - (currentNumber + 10));
        } else if (counter.displayedNum() < 2 && 7 < currentNumber) {
            rotations -= ((counter.displayedNum() + 10) - currentNumber);
        } else {
            rotations -= (counter.displayedNum() - currentNumber);
        }
        currentNumber = counter.displayedNum();
        if (Mathf.Abs(rotations) > 1.5) {
            rotate();
        } else {
            stick();
        }
    }

    public float at() {
        if(digitIndex != 0) {
            return (int) (counter.displayedNum() + 0.1f) % 10;
        }
        return counter.displayedNum();
    }

    public Vector2 floatToComponents(float a) {
        a = a % 1;
        return new Vector2(Mathf.Cos((Mathf.PI / 2) * a), Mathf.Sin((Mathf.PI / 2) * a));
    }

    public void newGoal(float rot, float goal) {
        goalNumber = goal;
        rotations = rot;
    }
    

    void stick() {

        float y;
        float z;

        // determine if a is y or z
        { Vector2 Temp = floatToComponents(goalNumber);
          if(((int) goalNumber) % 2 == 0) {
            y = Temp.x;
            z = Temp.y;
          } else {
            y = Temp.y;
            z = Temp.x;
          }
        }
        Debug.Log($"raw vector: {y}, {z}");

        // determine signs of y and z
            // determine the placement of the counter on the 5 spin chart
        float countery = counter.transformPweaseOwO().up.y;
        float counterz = counter.transformPweaseOwO().up.z;
        float c = counter.displayedNum();
        float tempGoal = goalNumber;
        if(goalNumber < 1 && c > 8) {
            goalNumber += 10;
        }
        if(goalNumber > 8 && c < 1) {
            goalNumber -= 10;
        }
        bool invert = false;
        if(c == c - (c % 1)) {
            if(c == 0 || c == 4 || c == 8) {
                invert = countery > 0;
            }
            if(c == 1 || c == 5 || c == 9) {
                invert = counterz > 0;
            } 
            if(c == 2 || c == 6) {
                invert = countery < 0;
            }
            if(c == 3 || c == 7) {
                invert = counterz < 0;
            }
        } else {
            if(c < 1) {
                invert = counterz > 0;
            }
            if((1 < c && c < 3)) {
                invert = countery < 0;
            }
            
            if(3 < c && c < 5) {
                invert = countery > 0;
            }

            if(5 < c && c < 7) {
                invert = countery < 0;
            }

            if(7 < c && c < 9) {
                invert = countery > 0;
            }

            if (9 < c) {
                invert = countery < 0;
            }
        }


        if((goalNumber > 1 && goalNumber < 3) || (goalNumber > 5 && goalNumber < 7) || goalNumber > 9) {
            y = -y;
        }
        if((goalNumber > 2 && goalNumber < 4) || (goalNumber > 6 && goalNumber < 8)) {
            z = -z;
        }
        if(!invert){
            y = -y;
            z = -z;
        }
        Debug.Log($"refined vector: {y}, {z}");
        wheel.transform.LookAt(wheel.transform.position + new Vector3(0, y, z));    
        Debug.Log(wheel.transform.rotation.eulerAngles);
        if(wheel.transform.rotation.eulerAngles.z != 0) {
            wheel.transform.rotation *= Quaternion.AngleAxis(-wheel.transform.rotation.eulerAngles.z, Vector3.forward);
            wheel.transform.rotation *= Quaternion.AngleAxis(180, Vector3.right);
        }
        if(wheel.transform.rotation.eulerAngles.y != 0) {
            wheel.transform.rotation *= Quaternion.AngleAxis(-wheel.transform.rotation.eulerAngles.y, Vector3.up);
            wheel.transform.rotation *= Quaternion.AngleAxis(180, Vector3.right);
        }
        
        wheel.transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);
        goalNumber = tempGoal;
    }

    void rotate() {
        // get the counter's rotation
        // set the rotation to maxDiff / 10^digitIndex 
        float angle = maxDegreeDiff / Mathf.Pow(10, digitIndex) * Mathf.Sign(rotations);
        wheel.transform.rotation = counter.transformPweaseOwO().rotation * Quaternion.AngleAxis(angle, Vector3.right);
    }
}
