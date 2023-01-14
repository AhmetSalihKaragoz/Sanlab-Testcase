using UnityEngine;

public class AttachmentManager : MonoBehaviour
{
    public static AttachmentManager Instance;
    private int _attachmentTurn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentAttachmentOrder()
    {
        if (_attachmentTurn >= 6) return 6;
        return _attachmentTurn;
    }

    public void IncreaseAttachmentOrder()
    {
        _attachmentTurn++;
        if (_attachmentTurn > 7)
        {
            UIManager.Instance.MovePanelImageOnComplete();
        }
    }

    public void DecereaseAttachmentOrder()
    {
        
        _attachmentTurn--;
    }

    public void ResetAttachmentTurn()
    {
        _attachmentTurn = 0;
    }
}
