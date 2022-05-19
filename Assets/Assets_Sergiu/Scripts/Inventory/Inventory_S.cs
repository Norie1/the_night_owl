using UnityEngine;
using UnityEngine.UI;

public class Inventory_S : MonoBehaviour
{
    public int wallet;
    public Text walletText;

    private int initialWallet;

    public static Inventory_S instance;

    //Awake is called before any other method (Start/Update/etc)
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Inventory already initialized in current scene");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        initialWallet = 0;
        wallet = initialWallet;
    }

    public void addCoins(int amount)
    {
        wallet += amount;
        walletText.text = wallet.ToString();
    }

    public void reinitializeWallet()
    {
        wallet = initialWallet;
        walletText.text = wallet.ToString();
    }
}
