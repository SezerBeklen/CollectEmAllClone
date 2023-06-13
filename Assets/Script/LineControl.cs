using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour
{
    [SerializeField] private GameObject line;
   
    [HideInInspector]
    public GameObject lineClone = null;

    private GameObject gameObj;
    private GameObject hitObjectChild_1;


    private string selectedObjTag = null;
  
    RaycastHit2D hit;

    public List<GameObject> obj = new List<GameObject>();
    public List<GameObject> nearestObj = new List<GameObject>();

    public static LineControl _instance;
    public bool isDestroy;
    public bool isObjCountIncrease;
    private AudioSource _source;

    [SerializeField] private AudioClip booblePop,multiplePop;
    [SerializeField] private string circle_Tag;
    private void Awake()
    {
        
        if (_instance == null)
        {
            _instance = this;
        }
        isDestroy = false;
        isObjCountIncrease = false;
    }

    private void Start()
    {
        
        _source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (obj.Count >= 3)
        {
            isObjCountIncrease = true;

        }
        else
        {
            isObjCountIncrease = false;
            
        }
     
        if (obj.Count > 0)
        {
            selectedObjTag = obj[0].tag;
           
        }

        if (Input.GetMouseButton(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject gameObject = hit.transform.gameObject;
                AutoChangeLineColor(gameObject);
                CreateLineAndAddObjectsToList(gameObject);
                InstantiateBackGround(gameObject);
                int current›ndex = obj.IndexOf(hit.transform.gameObject);
                RemoveObjectByIndex(current›ndex);
            }
        }
        else if(Input.GetMouseButtonUp(0) || hit.collider == null)
        {
            ClearObjectsAndLists();
            
        }

        if (Input.GetMouseButtonUp(0) && isObjCountIncrease) 
        {
            isDestroy = true;
        }
    }

    private void CreateLineAndAddObjectsToList(GameObject _obj2)
    {
        if (!obj.Contains(_obj2))
        {
            if (obj.Count == 0)
            {
                _source.PlayOneShot(booblePop, 0.6f);
                obj.Add(_obj2); 
                lineClone = Instantiate(line);
                lineClone.GetComponent<LineRenderer>().positionCount = obj.Count;
                lineClone.gameObject.GetComponent<LineRenderer>().SetPosition(0, _obj2.transform.position);
                gameObj = _obj2;
            }
            else if (_obj2.tag == selectedObjTag && lineClone != null && _obj2 != gameObj)
            {
                nearestObj = GetColliders(obj[obj.Count - 1].transform.position);
                if (_obj2.tag == selectedObjTag && nearestObj.Contains(_obj2))
                {
                    obj.Add(_obj2);
                    _source.PlayOneShot(booblePop, 1f);
                    lineClone.GetComponent<LineRenderer>().positionCount = obj.Count;
                    lineClone.gameObject.GetComponent<LineRenderer>().SetPosition(obj.Count - 1, _obj2.transform.position);
                }
            }
        }
        List<GameObject> GetColliders(Vector3 center)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, 3.5f);
            List<GameObject> goList = new List<GameObject>();
            foreach (var item in hitColliders)
            {
                goList.Add(item.gameObject);
            }

            return goList;
        }
    }
    private void AutoChangeLineColor(GameObject _obj)
    {
        if (obj.Contains(_obj))
        {
            line.gameObject.transform.GetComponent<LineRenderer>().sharedMaterial.color = hit.collider.gameObject.transform.GetComponent<SpriteRenderer>().color;
        }
    }
    private void InstantiateBackGround(GameObject _obj3)
    {
        if (obj.Contains(_obj3))
        {
            hitObjectChild_1 = _obj3.transform.GetChild(1).gameObject;
            hitObjectChild_1.SetActive(true);
            hitObjectChild_1.GetComponent<SpriteRenderer>().material.color = hit.collider.gameObject.transform.GetComponent<SpriteRenderer>().color;
        }
    }
    private void RemoveObjectByIndex(int index)
    {
        if (obj.Count >= 1 && index >= 0 && index < obj.Count - 1 && gameObj.tag == selectedObjTag)
        {
            lineClone.GetComponent<LineRenderer>().positionCount = obj.Count;
            for (int i = obj.Count - 1; i >= index; i--)
            {
                ChildsSetActiveState2(i);
                obj.RemoveAt(i);
                lineClone.GetComponent<LineRenderer>().SetPosition(i, hit.transform.gameObject.transform.position);
            }
        }
    }
    private void ClearObjectsAndLists()
    {
        if (obj.Count > 0)
        {
            if (obj.Count > 2)
            {
                foreach (GameObject go in obj)
                {
                    Destroy(go);
                }
                Destroy(lineClone);
                ChildsSetActiveState();
                _source.PlayOneShot(multiplePop, 1f);
                
            }
            else
            {
                Destroy(lineClone);
                ChildsSetActiveState();
                
            }
            obj.Clear();
            nearestObj.Clear();
            selectedObjTag = "";
            lineClone = null;
            
        }
    }
    private void ChildsSetActiveState()
    {
        GameObject[] circle = GameObject.FindGameObjectsWithTag(circle_Tag);
        foreach (GameObject c in circle)
        {
            c.SetActive(false);
        }
    } 
    private void ChildsSetActiveState2(int i)
    {
        GameObject currentObj = obj[i].gameObject.transform.GetChild(1).gameObject;
        currentObj.SetActive(false);
        
    }
    

}
