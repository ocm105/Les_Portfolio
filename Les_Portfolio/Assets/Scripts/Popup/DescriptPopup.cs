using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using TMPro;
using UnityEngine.UI;
using System;

public class DescriptPopup : UIPopup
{
    [SerializeField] TextMeshProUGUI descriptsText;
    [SerializeField] Button okButton;

    private int descriptIndex;

    public PopupState Open(DescriptType type)
    {
        ShowLayer();
        Init((int)type);

        return state;
    }
    protected override void OnFirstShow()
    {
        okButton.onClick.AddListener(OnClick_OkBtn);
    }
    protected override void OnShow() { }

    private void Init(int index)
    {
        descriptIndex = index;
        StartCoroutine(SpeechText(GameDataManager.Instance.discription_Data[descriptIndex].descript_key));
    }
    #region Event
    private void OnClick_OkBtn()
    {
        if (GameDataManager.Instance.discription_Data[descriptIndex].next_index == (int)DescriptType.NULL)
            OnResult(PopupResults.OK);
        else
        {
            descriptIndex++;
            StartCoroutine(SpeechText(GameDataManager.Instance.discription_Data[descriptIndex].descript_key));
        }
    }
    private IEnumerator SpeechText(string key)
    {
        string text = string.Empty;
        okButton.image.raycastTarget = false;

        string msg = LocalizationManager.Instance.GetLocalizeText(key);
        for (int i = 0; i < msg.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            text += msg[i];
            descriptsText.text = text;
        }
        okButton.image.raycastTarget = true;

    }

    #endregion
}
