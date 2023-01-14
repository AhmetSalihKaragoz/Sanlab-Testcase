using DG.Tweening;
public class Head : Parts
{
    public override void MontageChain()
    {
        if (!isAttached)
        {
            TurnOffDrag();
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(myAttachedSelf.transform.position, 0.5f));
            isAttached = true;
        }
    }

    protected override void TurnOnDrag()
    {
        gameObject.GetComponent<Drag>().isOnMontage = false;
    }

    protected override void TurnOffDrag()
    {
        gameObject.GetComponent<Drag>().isOnMontage = true;
    }
}
