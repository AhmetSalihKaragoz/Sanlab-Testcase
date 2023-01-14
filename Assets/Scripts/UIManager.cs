using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
        image.rectTransform.DOAnchorPosX(0, 1.5f);
    }
    
    public void RestartLevel()
    {
        image.rectTransform.position = new Vector3(0, 0, 0);
        AttachmentManager.Instance.ResetAttachmentTurn();
        SceneManager.LoadScene(0);
    }
    

}
