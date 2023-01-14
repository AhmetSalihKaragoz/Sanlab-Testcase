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


    public void MovePanelImageOnComplete()
    {
        var lerpDuration = 3f;
        var timeElapsed = 0f;
        var imageRectTransform = image.GetComponent<RectTransform>();
        var goalPos = new Vector3(imageRectTransform.anchoredPosition.x+750, imageRectTransform.anchoredPosition.y, 0);
        while (timeElapsed < lerpDuration)
        {
            imageRectTransform.anchoredPosition = Vector3.Lerp(imageRectTransform.anchoredPosition, goalPos, timeElapsed/lerpDuration);
            timeElapsed += Time.deltaTime;
        }
    }
    
    public void RestartLevel()
    {
        image.rectTransform.position = new Vector3(0, 0, 0);
        AttachmentManager.Instance.ResetAttachmentTurn();
        SceneManager.LoadScene(0);
    }

    // ReSharper disable Unity.Performan

}
