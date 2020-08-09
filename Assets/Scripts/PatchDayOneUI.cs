using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatchDayOneUI : MonoBehaviour
{
    [SerializeField] private EditionHand hand;
    [SerializeField] private Button button;

    private void Awake()
    {

    }

    private void Start()
    {
        PatchDayOneDLC dlc = DLCManager.PatchDayOne;
        if (dlc.IsLoaded)
        {
            this.button.onClick.AddListener(() =>
            {
                this.hand.Pick(dlc.Block);
            });
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
