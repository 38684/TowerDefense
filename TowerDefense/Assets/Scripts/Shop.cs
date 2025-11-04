
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject ShopUI;

    public void OnClick()
    {
        if (ShopUI.activeSelf)
            ShopUI.SetActive(false);
        else
            ShopUI.SetActive(true);
    }
}
