using DG.Tweening;

public class InnerCapBottom : Parts
{
    protected override void MontageChain()
    {
        if (!isAttached)
        {
            TurnOffDrag();
            Move();
        }
    }
    protected override void Move()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(myAttachedSelf.transform.position, 0.5f)).OnComplete(() =>
        {
            isAttached = true;
            TurnOnDrag();
        });
    }
}
