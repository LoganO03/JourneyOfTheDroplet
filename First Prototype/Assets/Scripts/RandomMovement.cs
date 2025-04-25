using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 2f;
    public float directionChangeInterval = 2f;

    private Vector2 direction;
    private float timer;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        // Move the particle
        transform.Translate(direction * speed * Time.deltaTime);

        // Update timer and change direction if needed
        timer += Time.deltaTime;
        if (timer >= directionChangeInterval)
        {
            ChooseNewDirection();
            timer = 0f;
        }
    }

    void ChooseNewDirection()
    {
        direction = Random.insideUnitCircle.normalized;
    }
}