using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    

    [SerializeField] private int columnCount = 6;
    [SerializeField] private int rowCount = 6;
    [SerializeField] public float gridNewPos = 1f;


    [SerializeField] private GameObject emptyObj;
    [SerializeField] private GameObject[] colorful_Circle_objs;
    [SerializeField] private List<Transform> columnPositions = new List<Transform>();


    public List<GameObject> gridsColumn_1 = new List<GameObject>();
    public List<GameObject> gridsColumn_2 = new List<GameObject>();
    public List<GameObject> gridsColumn_3 = new List<GameObject>();
    public List<GameObject> gridsColumn_4 = new List<GameObject>();
    public List<GameObject> gridsColumn_5 = new List<GameObject>();
    public List<GameObject> gridsColumn_6 = new List<GameObject>();

    private List<List<GameObject>> columnsLists = new List<List<GameObject>>();
    private void Awake()
    { 
        columnsLists.Add(gridsColumn_1);
        columnsLists.Add(gridsColumn_2);
        columnsLists.Add(gridsColumn_3);
        columnsLists.Add(gridsColumn_4);
        columnsLists.Add(gridsColumn_5);
        columnsLists.Add(gridsColumn_6);
    }

    private void Start()
    {
        GridsGenerator();
        
    }

    private void Update()
    {

        
        if ( LineControl._instance.isDestroy)
        {

            StartCoroutine(WaithCreating());
            Invoke("WaithToFalse",0.25f);

        }
    }
    private void GridsGenerator()
    {
        for (int column = 0; column < columnCount; column++)
        {
            for (int row = 0; row < rowCount; row++)
            {
                Vector2 gridPos = new Vector2(
                    columnPositions[column].transform.position.x,
                    columnPositions[column].transform.position.y - (row * gridNewPos));
                
               GameObject grid = Instantiate(emptyObj, gridPos, Quaternion.identity);
                grid.name = "Grid -" + "yatayda:"+column + "," + "dikeyde:"+row;
                grid.transform.tag = "grid";
                columnsLists[column].Add(grid);
            }
        }

       for (int column = 0; column < columnsLists.Count; column++)
        {
            for (int row = 0; row < columnsLists[column].Count; row++)
            {
                int randomColorfulObjs = Random.Range(0, colorful_Circle_objs.Length);
                GameObject colorfulObj = Instantiate(colorful_Circle_objs[randomColorfulObjs], Vector3.zero, Quaternion.identity);

                colorfulObj.transform.parent = columnsLists[column][row].gameObject.transform;
                colorfulObj.name += column + "." + row;
                colorfulObj.transform.localPosition = Vector3.zero;
            }

        }

    }

  /*  private void RandomCreateDestroyedColorfulObj()
    {
        
        for (int i = 0; i < columnsLists.Count; i++)
        {
            for (int j = 0; j < columnsLists[i].Count; j++)
            {
                if (columnsLists[i][j].transform.childCount == 0)
                {
                    if (j != 0)
                    {
                        columnsLists[i][j - 1].transform.GetChild(0).transform.parent = columnsLists[i][j].transform;
                    }
                }
               
                if (columnsLists[i][0].transform.childCount == 0)
                {
                    int randomObj = Random.Range(0, colorful_Circle_objs.Length);

                    float objXPos = columnsLists[i][0].transform.position.x;
                    float objYpos = columnsLists[i][0].transform.position.y;

                    Vector2 posObj = new Vector2(objXPos, objYpos);

                    GameObject ColorfulObj = Instantiate(colorful_Circle_objs[randomObj], posObj, Quaternion.identity);

                    ColorfulObj.transform.parent = columnsLists[i][0].transform;

                }
              

            }
        }
    }*/

    IEnumerator WaithCreating()
    {
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < columnsLists.Count; i++)
        {
            for (int j = 0; j < columnsLists[i].Count; j++)
            {
                if (columnsLists[i][j].transform.childCount == 0)
                {
                    if (j != 0)
                    {
                        columnsLists[i][j - 1].transform.GetChild(0).transform.parent = columnsLists[i][j].transform;
                    }
                }
              
                if (columnsLists[i][0].transform.childCount == 0)
                {
                    
                    int randomObj = Random.Range(0, colorful_Circle_objs.Length);

                    float objXPos = columnsLists[i][0].transform.position.x;
                    float objYpos = columnsLists[i][0].transform.position.y;

                    Vector2 posObj = new Vector2(objXPos, objYpos);
                   
                    GameObject ColorfulObj = Instantiate(colorful_Circle_objs[randomObj], posObj, Quaternion.identity);

                    ColorfulObj.transform.parent = columnsLists[i][0].transform;
                    
                }


            }
        }
         
       


    }

    void WaithToFalse()
    {
        LineControl._instance.isDestroy = false;
    }
 
}
