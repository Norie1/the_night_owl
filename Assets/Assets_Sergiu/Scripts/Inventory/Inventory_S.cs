using UnityEngine;
using UnityEngine.UI;

public class Inventory_S : MonoBehaviour
{
    public int wallet;
    public Text walletText;

    private int initialWallet;

    public static Inventory_S inventory;

    private void Start()
    {
        initialWallet = 0;
    }

    private void Awake()
    {
        if (inventory != null)
        {
            Debug.LogWarning("Inventory already initialized in current scene");
            return;
        }

        inventory = this;
    }

    public void addCoins(int nbOfCoins)
    {
        wallet += nbOfCoins;
        walletText.text = wallet.ToString();
    }

    public void reinitializeWallet()
    {
        wallet = initialWallet;
        walletText.text = wallet.ToString();
    }
}
