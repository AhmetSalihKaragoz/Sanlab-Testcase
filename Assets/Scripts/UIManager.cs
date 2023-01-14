using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Image image;
    public static UIManager Instance;
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


    public void MovePanelImageOnComplete()
    {
        image.rectTransform.DOAnchorPosX(0, 2f);
    }
    
    public void RestartLevel()
    {
        image.rectTransform.DOAnchorPosX(-750, 2f);
        AttachmentManager.Instance.ResetAttachmentTurn();
        SceneManager.LoadScene(0);
    }
    

}
