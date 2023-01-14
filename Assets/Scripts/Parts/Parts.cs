using System;
using UnityEngine;
public abstract class Parts : MonoBehaviour
{
    [SerializeField] protected GameObject triggerPoint;
    [SerializeField] protected GameObject myAttachedSelf;

    protected bool isAttached;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerPoint") && other.GetComponent<Collider>() == triggerPoint.GetComponent<Collider>())
        {
            MontageChain();
        }
        
    }

    protected virtual void MontageChain() {
        
    }

    protected abstract void Move();
    protected void TurnOnDrag()
    {
        gameObject.GetComponent<Drag>().isOnMontage = false;
    }
    protected void TurnOffDrag()
    {
        gameObject.GetComponent<Drag>().isOnMontage = true;
    }
}
