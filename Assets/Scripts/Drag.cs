using System;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private List<GameObject> fitParts;
    private readonly List<GameObject> _parts = new List<GameObject>();
    private Vector3 screenPoint;
    private Vector3 offset;


    private List<Transform> children;
    private Transform myParent;
    private Transform myChild;

    private void Start()
    {
        myParent = transform.parent;

        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }
        
        for (int i = 0; i < myParent.childCount; i++)
        {
            _parts.Add(myParent.GetChild(i).gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == gameObject.transform.parent || children.Contains(gameObject.transform))
        {
            Debug.Log("Hello World");
        }
    }

    void OnMouseDown()
    {
        TurnUnfitCollidersOff();
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
 
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        TurnCollidersOn();
    }

    private void TurnCollidersOn()
    {
        for (int i = 0; i < _parts.Count; i++)
        {
            _parts[i].GetComponent<Collider>().isTrigger = true;
        }
    }

    void TurnUnfitCollidersOff()
    {
        for (int i = 0; i < _parts.Count; i++)
        {
            if (!fitParts.Contains(_parts[i]) && _parts[i] != gameObject)
            {
                _parts[i].GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}
