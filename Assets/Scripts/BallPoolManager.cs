using System.Collections;
using UnityEngine;

public class BallPoolManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;


    [SerializeField] Transform spawnPoint;
    [SerializeField] float maxShootPower = 30f;
    [SerializeField] float shootPowerIncreaseRate = 5f;
    [SerializeField] float ballLifeTime = 5f;
  


    private GameObject[] ballPool;
    private int poolSize = 5;  
    private int currentIndex = 0;
    private float currentShootPower = 0f;

    [SerializeField]  float sensitivityHor = 5f;
    [SerializeField]  float minimumHor = -360f;
    [SerializeField]  float maximumHor = 360f;

    private float rotationY = 0f;

    

 void Start()
    {
        InitializePool();
    }


    void Update()
    {
        HandleCannonRotation();

        if (Input.GetMouseButtonDown(0))
        {
            currentShootPower = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            currentShootPower = Mathf.Min(currentShootPower + shootPowerIncreaseRate * Time.deltaTime, maxShootPower);
        }

        if (Input.GetMouseButtonUp(0))
    {
        GameObject ball = GetPooledBall();
        ball.SetActive(true);
        ShootBall(ball, currentShootPower);
        StartCoroutine(DeactivateBallAfterTime(ball));
    }
    }

    void HandleCannonRotation()
    {
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;

        rotationY = RestrictAngle(transform.localEulerAngles.y + delta, minimumHor, maximumHor);
         transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, transform.localEulerAngles.z);

        
        
    }
    float RestrictAngle(float angle, float min, float max)
    {

        if (angle < 360f + min && angle > max)
        {
            if (angle > 180f)
            {
                return Mathf.Max(angle - 360f, max);
            }
            return Mathf.Min(angle + 360f, min);
        }
        return Mathf.Clamp(angle, min, max);
    }

    void ShootBall(GameObject ball, float shootPower)
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.velocity = Vector3.zero;  
        ballRigidbody.AddForce(spawnPoint.forward * shootPower, ForceMode.Impulse);
    }

    void InitializePool()
    {
        ballPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            ballPool[i] = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
            ballPool[i].SetActive(false);
        }
    }

    GameObject GetPooledBall()
    {
        GameObject ball = ballPool[currentIndex];
        ball.transform.position = spawnPoint.position;
        ball.transform.rotation = spawnPoint.rotation;
        currentIndex = (currentIndex + 1) % poolSize;
        return ball;
    }

IEnumerator DeactivateBallAfterTime(GameObject ball)
{
    yield return new WaitForSeconds(ballLifeTime);
    ball.SetActive(false);
}
}
