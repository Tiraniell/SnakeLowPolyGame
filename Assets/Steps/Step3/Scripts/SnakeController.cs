using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0,1)]
    public float BonesDistanse;
    public GameObject BonePrefabe;

    [Range(0, 1)]
    public float Speed;
    
    private Transform _transform;

    public UnityEvent OnEat;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);

        float angel = Input.GetAxis("Horizontal") * 4;
        _transform.Rotate(0, angel, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDistanse * BonesDistanse;
        Vector3 previosPosition = _transform.position;

        foreach(var bone in Tails)
        {
            if ((bone.position - previosPosition).sqrMagnitude > sqrDistance)
            {
                var temp = bone.position;
                bone.position = previosPosition;
                previosPosition = temp;
            }
            else
            {
                break;
            }
        }
        _transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);

            var bone = Instantiate(BonePrefabe);
            Tails.Add(bone.transform);

            if( OnEat!= null)
            {
                OnEat.Invoke();
            }
        } 
    }

}
