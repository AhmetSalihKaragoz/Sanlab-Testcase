using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Parts : MonoBehaviour
{

    [SerializeField] private List<Transform> myAttachmentPoints;

    public int myAttachmentOrder;

    [SerializeField] private GameObject silhouette;

    public AttachmentPoint activeAttachmentPoint;

    public bool isAttached;

    private bool _isTweening;
    
    private bool isItOnCorrectVolumeOnMouseDrag;

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
            activeAttachmentPoint = other.GetComponent<AttachmentPoint>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("TriggerPoint")) return;
        if (_isTweening) return;
        if (!IsCorrectAttachmentPosition(other.gameObject)) return;
        SetSilhouetteState(false);
            activeAttachmentPoint = null;
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
        _isTweening = true;
        SetSilhouetteState(false);
        myCollider.enabled = false;
        activeAttachmentPoint.GetComponent<AttachmentPoint>().IsUsed = true;
        var sequence = SetAnimationSequence();
        sequence.OnComplete(() =>
        {
            _isTweening = false;
            myCollider.enabled = true;
            isAttached = true;
        });
        sequence.Play();
        AttachmentManager.Instance.IncreaseAttachmentOrder();
    }

    private bool IsCorrectAttachmentPosition(GameObject other)
    {
        return other.CompareTag("TriggerPoint") && myAttachmentPoints.Contains(other.transform);
    }

    private void SetSilhouetteState(bool isActive)
    {
        isItOnCorrectVolumeOnMouseDrag = isActive;
        silhouette.SetActive(isActive);
    }

    public virtual Sequence SetAnimationSequence()
    {
        var wayPointList = activeAttachmentPoint.GetComponentsInChildren<Transform>();
        Debug.Log(wayPointList.Length);
        Debug.Log(wayPointList[0].name);
        var sequence = DOTween.Sequence();
        for (int i = 1; i < wayPointList.Length; i++)
        {
            sequence.Append(transform.DOMove(wayPointList[i].position, 0.5f));
        }
        return sequence;
    }

    public void Detach()
    {
        AttachmentManager.Instance.DecereaseAttachmentOrder();
        if (activeAttachmentPoint != null)
        {
            activeAttachmentPoint.IsUsed = false;
            isAttached = false;
        }
    }
}
