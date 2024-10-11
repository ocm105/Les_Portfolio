using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEditor.Localization.Plugins.XLIFF.V20;

public class Test : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startText;

    Color alpha = new Color(0, 0, 0, 1);

    private void Start()
    {
        Tween_Fadein(startText);
    }

    #region Event
    private void Tween_Fadein(TextMeshProUGUI text)
    {
        if (!text.gameObject.activeInHierarchy) return;

        startText.DOFade(0, 0.5f).onComplete = () => Tween_Fadeout(startText);

    }
    private void Tween_Fadeout(TextMeshProUGUI text)
    {
        if (!text.gameObject.activeInHierarchy) return;

        startText.DOFade(1, 0.5f).onComplete = () => Tween_Fadein(startText);
    }
    #endregion
}
