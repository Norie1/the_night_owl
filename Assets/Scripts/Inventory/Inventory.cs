using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int wallet;
    public Text walletText;

    private int initialWallet;

    public static Inventory instance;

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

    public void AddCoins(int amount)
    {
        wallet += amount;
        walletText.text = wallet.ToString();
    }

    public void ResetWallet()
    {
        wallet = initialWallet;
        walletText.text = wallet.ToString();
    }
}
