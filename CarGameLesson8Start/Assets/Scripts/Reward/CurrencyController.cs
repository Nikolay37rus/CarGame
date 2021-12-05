using UnityEngine;

public class CurrencyController : BaseController
{
    public CurrencyController(Transform placeForUi, CurrencyView currencyView)
    {
        var instanceCurrencyView = Object.Instantiate(currencyView, placeForUi);
        AddGameObjects(instanceCurrencyView.gameObject);
    }
}
