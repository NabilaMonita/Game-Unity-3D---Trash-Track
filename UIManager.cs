using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image[] lives;
    public Text coinText;
    public Text timerText; // Timer mundur
   

    public void UpdateLives(int currentLives) {
        for (int i = 0; i < lives.Length; i++) {
            lives[i].color = currentLives > i ? Color.white : Color.black;
        }
    }
   


    public void UpdateCoins(int coin) {
        coinText.text = coin.ToString();
    }

    public void UpdateTimer(int timeRemaining) {
        int minutes = timeRemaining / 60; // Hitung menit
        int seconds = timeRemaining % 60; // Hitung detik

        // Format waktu ke menit:detik
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString; // Update teks timer dengan waktu yang tersisa
    }
}
