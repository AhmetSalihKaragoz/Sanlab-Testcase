using DG.Tweening;
using UnityEngine;
public abstract class Parts : MonoBehaviour
{
    [SerializeField] protected GameObject triggerPoint;
    [SerializeField] protected Transform myAttachmentPoint;

    protected bool isAttached;
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (IsFit(other.gameObject)) 
        {
            if (!isAttached)
            {
                Attach();
                TurnOffDrag();
            }
        }
    }
    
    protected virtual void Attach()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(myAttachmentPoint.transform.position, 0.5f)).OnComplete(() =>
        {
            isAttached = true;
            TurnOnDrag();
        });
    }
    protected void TurnOnDrag()
    {
        gameObject.GetComponent<Drag>().isOnMontage = false;
    }

    private void TurnOffDrag()
    {
        gameObject.GetComponent<Drag>().isOnMontage = true;
    }
    protected virtual bool IsFit(GameObject other)
    {
        return other.CompareTag("TriggerPoint") && other.GetComponent<Collider>() == triggerPoint.GetComponent<Collider>();
    }
}
