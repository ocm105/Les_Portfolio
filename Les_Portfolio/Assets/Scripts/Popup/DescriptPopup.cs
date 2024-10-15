using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using TMPro;
using UnityEngine.UI;

public class DescriptPopup : UIPopup
{
    [SerializeField] TextMeshProUGUI descriptsText;
    [SerializeField] Button okButton;

    int test = 1001;
    private bool isLast = false;

    public PopupState Open()
    {
        ShowLayer();
        Init();

        return state;
    }
    protected override void OnFirstShow()
    {
        okButton.onClick.AddListener(OnClick_OkBtn);
    }
    protected override void OnShow()
    {
    }

    private void Init()
    {
        isLast = false;
        StartCoroutine(SpeechText(GameDataManager.Instance.discription_Data[test].descript_key));
    }
    #region Event
    private void OnClick_OkBtn()
    {
        isLast = true;
        StartCoroutine(SpeechText(GameDataManager.Instance.discription_Data[test + 1].descript_key));
    }
    private IEnumerator SpeechText(string key)
    {
        okButton.image.raycastTarget = false;

        string msg = LocalizationManager.Instance.GetLocalizeText(key);
        string text = string.Empty;
        for (int i = 0; i < msg.Length; i++)
        {
            yield return new WaitForSeconds(0.15f);
            text += msg[i];
            descriptsText.text = text;
        }
        okButton.image.raycastTarget = true;
        if (isLast) OnResult(PopupResults.OK);
    }

    #endregion
}
