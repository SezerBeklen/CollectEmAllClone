using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    void Update()
    {
        MoveColorfulObj();
    }

    private void MoveColorfulObj()
    {
        if (transform.parent != null) 
        {
            if (transform.localPosition == Vector3.zero) 
            {
                return;
            }
            else
            {
                
                float x = transform.position.x - transform.parent.transform.position.x;
                float y = transform.position.y - transform.parent.transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x - x, transform.position.y - y, 0), speed * Time.deltaTime);
                 
            }
        }
    }
 
}
