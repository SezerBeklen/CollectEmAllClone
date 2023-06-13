using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class linerend : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Vector2 mousePos;
    private Vector2 startMousePos;

    public float valueX;
    public float valueY;
    RaycastHit2D hit;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        MyClick();
    }
    
    public void MyClick()
    {

        if (LineControl._instance.obj.Count == 0)
        {
            lineRenderer.GetComponent<LineRenderer>().enabled = false;
           
        }

        if (Input.GetMouseButton(0))
         {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float difx = mousePos.x - lineRenderer.transform.position.x;
            float dify = mousePos.y - lineRenderer.transform.position.y;

            float mousePosy = Mathf.Clamp(dify, -3f, 3f);
            float mousePosx = Mathf.Clamp(difx, -3f, 3f);

            lineRenderer.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0));
            lineRenderer.SetPosition(1, new Vector3(mousePosx * valueX, mousePosy * valueY, 0));


            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(mousePos.x, mousePos.y));

            if (hit.collider != null && LineControl._instance.obj.Contains(hit.collider.gameObject)) 
            {
                if(hit.transform.gameObject == LineControl._instance.obj[LineControl._instance.obj.Count - 1])
                {
                    lineRenderer.GetComponent<LineRenderer>().enabled = true;
                    lineRenderer.gameObject.transform.position = hit.transform.gameObject.transform.position;

                    lineRenderer.startColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                    lineRenderer.endColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;

                }

              
            }

        }


        


    }
}
