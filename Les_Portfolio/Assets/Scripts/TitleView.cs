using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UISystem
{
    public class TitleView : UIView
    {
        [SerializeField] GameObject title;
        [SerializeField] Button startButton;

        public void Show(object param)
        {
            ShowLayer();
            Init();
        }
        protected override void OnFirstShow()
        {
            startButton.onClick.AddListener(OnClick_StartBtn);
        }
        protected override void OnShow()
        {

        }
        private void Init()
        {
            title.SetActive(true);
            startButton.gameObject.SetActive(true);
        }

        #region Event

        private void OnClick_StartBtn()
        {
            //UIManager.Instance.Popup<DescriptPopup>()
        }

        #endregion
    }
}