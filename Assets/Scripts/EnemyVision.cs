using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] Transform EnemyTransform;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float rotationSpeed;
    [SerializeField] float speed;
    public bool canMove;
    private void Start()
    {
        PlayerTransform = FindAnyObjectByType<PlayerAbilities>().GetComponent<Transform>();
        Invoke(nameof(CanMove), 1);
    }
    private void Update()
    {
        Vector3 Dir = new Vector3(PlayerTransform.position.x - EnemyTransform.position.x, 0, PlayerTransform.position.z - EnemyTransform.position.z);
        EnemyTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(EnemyTransform.forward, Dir, rotationSpeed * Time.deltaTime, 0));
        if (canMove)
        {
            EnemyTransform.position = Vector3.MoveTowards(EnemyTransform.position, PlayerTransform.position, speed * Time.deltaTime);
        }
    }
    private void OnTriggerStay(Collider other)
    { 
        //if (other.CompareTag("Player") && canMove)
        //{
        //    EnemyTransform.position = Vector3.MoveTowards(EnemyTransform.position, other.transform.position, speed * Time.deltaTime);
        //}
    }
    private void CanMove()
    {
        canMove = true;
    }
}
