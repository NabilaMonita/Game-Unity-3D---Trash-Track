using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject coinPrefab;        // Prefab untuk coin
    public GameObject obstaclePrefab;    // Prefab untuk rintangan
    public GameObject trackPrefab;       // Prefab untuk track
    public int numberOfTracks = 5;       // Jumlah track yang diulang
    public float trackLength = 30f;      // Panjang tiap track
    public float distanceBetweenElements = 5f; // Jarak antara coin dan obstacle

    private List<GameObject> tracks = new List<GameObject>();

    void Start()
    {
        SpawnTracks();
    }

    void SpawnTracks()
    {
        for (int i = 0; i < numberOfTracks; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, i * trackLength);
            GameObject track = Instantiate(trackPrefab, spawnPosition, Quaternion.identity);
            tracks.Add(track);

            // Panggil fungsi untuk menempatkan coin dan obstacle di sepanjang track
            PlaceCoinsAndObstacles(track.transform);
        }
    }

    void PlaceCoinsAndObstacles(Transform trackTransform)
    {
        // Hitung jumlah coin dan obstacle yang akan ditempatkan di setiap track
        int numberOfElements = Mathf.FloorToInt(trackLength / distanceBetweenElements);

        // Iterasi untuk menempatkan coin dan obstacle di sepanjang track
        for (int i = 0; i < numberOfElements; i++)
        {
            // Hitung posisi relatif coin dan obstacle di sepanjang track
            float zPosition = i * distanceBetweenElements;
            float xPosition = Random.Range(-2.5f, 2.5f); // Asumsi lebar track adalah 5 unit

            // Tentukan posisi coin
            Vector3 coinPosition = new Vector3(xPosition, 1, zPosition);
            // Instansiasi coin dan pasang di bawah parent track
            GameObject coin = Instantiate(coinPrefab, trackTransform.TransformPoint(coinPosition), Quaternion.identity);
            coin.transform.parent = trackTransform;

            // Hitung posisi rintangan (obstacle) yang berjarak sedikit dari coin
            Vector3 obstaclePosition = new Vector3(xPosition, 1, zPosition + distanceBetweenElements / 2); 
            // Instansiasi obstacle dan pasang di bawah parent track
            GameObject obstacle = Instantiate(obstaclePrefab, trackTransform.TransformPoint(obstaclePosition), Quaternion.identity);
            obstacle.transform.parent = trackTransform;
        }
    }

    void Update()
    {
        // Cek apakah track telah melewati kamera
        foreach (GameObject track in tracks)
        {
            if (track.transform.position.z + trackLength < Camera.main.transform.position.z)
            {
                // Jika iya, pindahkan track ke depan
                Vector3 newPosition = track.transform.position + new Vector3(0, 0, numberOfTracks * trackLength);
                track.transform.position = newPosition;

                // Hapus semua coin dan obstacle lama
                foreach (Transform child in track.transform)
                {
                    Destroy(child.gameObject);
                }

                // Pasang coin dan obstacle baru
                PlaceCoinsAndObstacles(track.transform);
            }
        }
    }
}
