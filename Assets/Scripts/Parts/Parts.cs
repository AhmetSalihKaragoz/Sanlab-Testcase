using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Parts : MonoBehaviour
{

    [SerializeField] private List<Transform> myAttachmentPoints;

    [SerializeField] private int myAttachmentOrder;

    [SerializeField] private GameObject silhouette;

    [HideInInspector] public AttachmentPoint ActiveAttachmentPoint;
    
    private bool IsItOnCorrectVolumeOnMouseDrag;

    private Vector3 _startingPos;
    private Quaternion _startingRot;

    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
        _startingPos = transform.position;
        _startingRot = transform.rotation;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("TriggerPoint")) return;
        if (silhouette.activeSelf == true) return;
        bool isThisPointUsed = other.GetComponent<AttachmentPoint>().IsUsed;
        if (IsCorrectAttachmentPosition(other.gameObject)&&
            myAttachmentOrder == AttachmentManager.Instance.GetCurrentAttachmentOrder()
            && isThisPointUsed == false)
        {
            SetSilhouetteState(true);
            ActiveAttachmentPoint = other.GetComponent<AttachmentPoint>();
        }
        else
        {
            SetSilhouetteState(false);
            ActiveAttachmentPoint = null;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("TriggerPoint")) return;
        if (IsCorrectAttachmentPosition(other.gameObject))
        {
            SetSilhouetteState(false);
            ActiveAttachmentPoint = null;
        }
    }

    public void ReturnStartingPosition()
    {
        myCollider.enabled = false;
        transform.DOMove(_startingPos, 0.25f).OnComplete(() =>
        {
            myCollider.enabled = true;
        });
        transform.DORotate(_startingRot.eulerAngles, 0.25f);
        
    }

    public void Attach()
    {
        SetSilhouetteState(false);
        myCollider.enabled = false;
        ActiveAttachmentPoint.GetComponent<AttachmentPoint>().IsUsed = true;
        var sequence = SetAnimationSequence();
        sequence.OnComplete(() =>
        {
            myCollider.enabled = true;
        });
        sequence.Play();
    }

    private bool IsCorrectAttachmentPosition(GameObject other)
    {
        return other.CompareTag("TriggerPoint") && myAttachmentPoints.Contains(other.transform);
    }

    private void SetSilhouetteState(bool isActive)
    {
        IsItOnCorrectVolumeOnMouseDrag = isActive;
        silhouette.SetActive(isActive);
    }

    public virtual Sequence SetAnimationSequence()
    {
        var wayPointList = ActiveAttachmentPoint.GetComponentsInChildren<Transform>();
        Debug.Log(wayPointList.Length);
        Debug.Log(wayPointList[0].name);
        var sequence = DOTween.Sequence();
        for (int i = 1; i < wayPointList.Length; i++)
        {
            sequence.Append(transform.DOMove(wayPointList[i].position, 0.5f));
        }
        return sequence;
    }
}
