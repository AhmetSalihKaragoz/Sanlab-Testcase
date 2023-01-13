using System;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public Vector3 childhoodPosition;
    [SerializeField] private GameObject parentInAttachedPos;
    [SerializeField] private GameObject childInAttachedPos;
    

    private bool isSet;
    private bool hasShown;

    private GameObject fluParent;
    private GameObject fluChild;
    
    private Vector3 screenPoint;
    private Vector3 offset;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Part"))
        {
            if (!hasShown)
            {
                if (other.gameObject == parentInAttachedPos)
                {
                    hasShown = true;
                    isSet = true;
                    // transform.SetParent(other.transform);
                    // transform.localPosition = childhoodPosition;
                    ShowFluParts(other.gameObject);
                }
                if(other.gameObject == childInAttachedPos)
                {
                    hasShown = true;
                    isSet = true;
                    // transform.position = other.transform.position;
                    // other.transform.parent = transform;
                    // other.transform.localPosition = other.GetComponent<Drag>().childhoodPosition;
                    ShowFluParts(other.gameObject);
                }
            }
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        hasShown = false;
        fluChild.SetActive(false);
        fluParent.SetActive(false);
    }

    void OnMouseDown()
    {
        isSet = false;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!isSet)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    void ShowFluParts(GameObject other)
    {
        var transMat = transform.GetComponent<Renderer>().material;
        var transMatColor = transMat.color;
        transMatColor.a = 50;
        if (other.gameObject == parentInAttachedPos)
        {
            fluParent = Instantiate(parentInAttachedPos, other.transform.position, transform.rotation);
            fluChild = Instantiate(gameObject, childhoodPosition, other.transform.rotation);
            fluParent.GetComponent<Renderer>().material = transMat;
            fluChild.GetComponent<Renderer>().material = transMat;
            fluChild.SetActive(true);
            fluParent.SetActive(true);
        }
        if (other.gameObject != childInAttachedPos) return;
        fluParent = Instantiate(gameObject, other.transform.position, transform.rotation);
        fluChild = Instantiate(childInAttachedPos, other.GetComponent<Drag>().childhoodPosition, other.transform.rotation); fluParent.GetComponent<Renderer>().material = transMat;
        fluChild.GetComponent<Renderer>().material = transMat;
        fluChild.SetActive(true);
        fluParent.SetActive(true);
    }
}
