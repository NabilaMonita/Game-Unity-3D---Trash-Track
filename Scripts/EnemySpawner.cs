using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab musuh
    public GameObject enemyPrefab;
    // Jumlah musuh yang akan disebar
    public int jumlahMusuh = 10;
    // Batas area minimum dan maksimum untuk menyebar musuh
    public Vector3 areaMin;
    public Vector3 areaMax;

    void Start()
    {
        // Menyebarkan musuh di area yang ditentukan
        for (int i = 0; i < jumlahMusuh; i++)
        {
            Vector3 posisiAcak = new Vector3(
                Random.Range(areaMin.x, areaMax.x),
                Random.Range(areaMin.y, areaMax.y),
                Random.Range(areaMin.z, areaMax.z)
            );
            Instantiate(enemyPrefab, posisiAcak, Quaternion.identity);
        }
    }
}
