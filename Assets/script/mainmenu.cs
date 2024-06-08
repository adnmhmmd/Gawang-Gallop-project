using UnityEngine;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public Text coinText; // Referensi ke UI Text untuk menampilkan jumlah koin

    void Start()
    {
        UpdateCoinUI(); // Perbarui UI saat menu utama dimulai
    }

    void UpdateCoinUI()
    {
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0); // Muat jumlah koin yang tersimpan
        if (coinText != null)
        {
            coinText.text = coinCount.ToString(); // Perbarui teks UI dengan jumlah koin
        }
        else
        {
            Debug.LogError("Coin Text UI belum ditetapkan!");
        }
    }
}
