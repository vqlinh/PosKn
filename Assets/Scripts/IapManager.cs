using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

[System.Serializable]
public class ConsumableItem {
    public string Name;
    public string Id;
    public string desc;
    public float price;
}

[System.Serializable]
public class IapManager : MonoBehaviour, IStoreListener
{
    public TextMeshProUGUI coinTxt;
    public ConsumableItem[] listItems;
    // Define All Product Id
    const string COIN_2000 = "coin_2000";
    const string COIN_6000 = "coin_6000";
    const string COIN_9500 = "coin_9500";

    IStoreController m_StoreController;
    int totalCoins = 0;
    public CoinData coinData;
    // Start is called before the first frame update
    void Start()
    {
        coinTxt.text = coinData.coin.ToString();
        SetupBuilder();
    }

    //SETUP BUILDER
    void SetupBuilder() 
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        //Add All Product Items to Builder
        for (int i = 0; i <listItems.Length; i++)
        {
            builder.AddProduct(listItems[i].Id, ProductType.Consumable);
        }
        // builder.AddProduct(cItem.Id, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    /** UI BUTTON EVENTs for PURCHASE **/
    public void Consumable_BtnCoin2000_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_2000);
    }
    public void Consumable_BtnCoin6000_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_6000);
    }
    public void Consumable_BtnCoin9500_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_9500);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        //Restrive the purchased product
        var product = purchaseEvent.purchasedProduct;
        Debug.Log("Purchase completed: " + product.definition.id);
        if (product.definition.id == COIN_2000)
        {
            //Add Coins
            Debug.Log("Plus 2000 coins");
            coinData.coin += 2000;
            coinTxt.text = coinData.coin.ToString();
        } else if (product.definition.id == COIN_6000)
        {
            Debug.Log("Plus 6000 coins");
            //TODO:Add Coins
            coinData.coin += 6000;
            coinTxt.text = coinData.coin.ToString();
        } else if (product.definition.id == COIN_9500) 
        {
            Debug.Log("Plus 9500 coins");
            //TODO:Add Coins
            coinData.coin += 9500;
            coinTxt.text = coinData.coin.ToString();
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Init purchase success!");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }
}
