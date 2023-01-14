using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Screw : Parts
{
    [SerializeField] private List<GameObject> triggerPoints;
    [SerializeField] private List<Transform> myAttachmentPoints;

    protected override void OnTriggerEnter(Collider other)
    {
        if (IsFit(other.gameObject))
        {
            CheckWhichPointUsed(other.gameObject);
            Attach();
        }
    }

    protected override void Attach()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(myAttachmentPoint.position, 0.5f)).OnComplete(() =>
        {
            isAttached = true;
            TurnOnDrag();
        });
    }
    protected override bool IsFit(GameObject other)
    {
        return other.CompareTag("TriggerPoint") && triggerPoints.Contains(other.gameObject) && !other.gameObject.GetComponent<CheckPointUsed>().IsUsed;
    }

    private void CheckWhichPointUsed(GameObject other)
    {
        if (other.gameObject == triggerPoints[0] )
        {
            myAttachmentPoint = myAttachmentPoints[0];
            other.gameObject.GetComponent<CheckPointUsed>().IsUsed = true;
        }
        else
        {
            myAttachmentPoint = myAttachmentPoints[1];
            other.gameObject.GetComponent<CheckPointUsed>().IsUsed = true;
        }
    }
}
