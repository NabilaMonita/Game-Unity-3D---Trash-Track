using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject[] coins;
    public Vector2 coinsNumber;
    public float trackZInterval = 23f; // Jarak antara setiap track

    private float trackLength; // Panjang tiap track
    private float distanceBetweenTracks; // Jarak antara tiap track

    void Start()
    {
        trackLength = GetComponentInChildren<MeshRenderer>().bounds.size.z; // Ambil panjang dari track
        distanceBetweenTracks = trackLength + trackZInterval; // Menambahkan interval
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaceCoins(transform); // Tempatkan koin di track saat pemain menyentuhnya
        }
    }

    void PlaceCoins(Transform trackTransform)
    {
        float zPosition = 0;

        // Hitung posisi koin untuk sepanjang track
        while (zPosition < trackLength)
        {
            // Pilih posisi acak untuk coin
            float xCoinPosition = Random.Range(-1f, 1f);
            Vector3 coinPosition = new Vector3(xCoinPosition, 0.5f, zPosition); // Tinggi koin diatur ke 0.5f agar berada di atas track
            GameObject coin = Instantiate(coins[Random.Range(0, coins.Length)], trackTransform.TransformPoint(coinPosition), Quaternion.identity);
            coin.transform.parent = trackTransform;
            coin.tag = "Coin"; // Tambahkan tag "Coin" pada koin

            // Naikkan zPosition untuk posisi berikutnya
            zPosition += Random.Range(2f, 5f); // Jarak antar coin bisa disesuaikan sesuai kebutuhan
        }
    }
}
