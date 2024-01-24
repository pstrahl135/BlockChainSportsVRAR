using UnityEngine;
using TMPro;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField]  Transform pointB;
    private float speed = 5f;

    private Transform target;


    void Start()
    {
        target = pointA;
    }

    void FixedUpdate()
    {

        MoveToTarget();
    }

    void MoveToTarget()
    {

        Vector3 direction = (target.position - transform.position).normalized;


        transform.Translate(direction * speed * Time.fixedDeltaTime);


        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {

            target = (target == pointA) ? pointB : pointA;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<BoxCollider>() != null )
        {
            target = (target == pointA) ? pointB : pointA;
       
        }    
    }


}