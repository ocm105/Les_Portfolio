using System.Collections;
using UISystem;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;


public class LocalizationManager : SingletonMonoBehaviour<LocalizationManager>
{
    private StringTable localizationTable = null;
    private bool isChanging = false;
    const string tableName = "Localization Table";

    private bool isCompleted = false;

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    public IEnumerator Init()
    {
        if (isCompleted == false)
        {
            yield return LocalizationSettings.InitializationOperation;

            var tableOp = LocalizationSettings.StringDatabase.GetTableAsync(tableName);
            yield return new WaitUntil(() => tableOp.IsDone);

            localizationTable = tableOp.Result;
        }
        isCompleted = true;
    }

    public string GetLocalizeText(string key)
    {
        var entry = localizationTable?.GetEntry(key);
        if (entry != null)
            return entry.GetLocalizedString();

        return key;
    }

    IEnumerator _ChangeLanguage(int index)
    {
        isChanging = true;

        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        isChanging = false;
    }

    public void ChangeLanguage(int index)
    {
        if (!isChanging)
            return;

        StartCoroutine(_ChangeLanguage(index));
    }
}