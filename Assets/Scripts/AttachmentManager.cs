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
        return _attachmentTurn;
    }

    public void IncreaseAttachmentOrder()
    {
        _attachmentTurn++;
    }

    public void DecereaseAttachmentOrder()
    {
        _attachmentTurn--;
    }
}
