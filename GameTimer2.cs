using UnityEngine;
using UnityEngine.UI;

public class GameTimer2 : MonoBehaviour
{
    public float timeRemaining = 60f; // Waktu awal dalam detik
    public bool timerIsRunning = false;
    public Text timeText; // UI Text untuk menampilkan waktu
    public GameObject messageTextObject; // UI GameObject untuk menampilkan pesan
    public Player4 player; // Referensi ke script Player2


    private Text messageText; // Text component untuk menampilkan pesan

    void Start()
    {
        // Mulai timer
        timerIsRunning = true;
        // Ambil komponen Text dari GameObject messageTextObject
        messageText = messageTextObject.GetComponent<Text>();
        // Pastikan messageText tidak terlihat di awal
        messageTextObject.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                // Tampilkan pesan ketika waktu habis
                ShowMessage();
                // Panggil metode reset player saat waktu habis
                player.ResetPlayer();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ShowMessage()
    {
        if (messageText != null)
        {
            messageText.text = "Anda telah melindungi lingkungan anda";
        }
        messageTextObject.SetActive(true); // Tampilkan messageTextObject
    }
}
