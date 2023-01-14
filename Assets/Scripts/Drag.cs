using System;
using System.Net;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isHeld;

    private Parts _part;

    private void Start()
    {
        _part = GetComponent<Parts>();
    }


    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        if (!_part.isAttached)
        {
            isHeld = true;
        }
        else if(_part.isAttached && AttachmentManager.Instance.GetCurrentAttachmentOrder() == _part.myAttachmentOrder+1)
        {
            isHeld = true;
            _part.Detach();
        }
    }

    private void OnMouseUp()
    {
        if (!isHeld) return;
        if (_part.activeAttachmentPoint != null)
        {
            _part.Attach();
        }
        else
        {
            _part.ReturnStartingPosition();
        }

        isHeld = false;
    }

    void OnMouseDrag()
    {
        if (isHeld)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = new Vector3(curPosition.x, curPosition.y, 0);
        }
    }
}