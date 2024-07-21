using UnityEngine;

public class TrashSpawner3 : MonoBehaviour
{
    // Prefab sampah yang akan diinstansiasi
    public GameObject trashPrefab1;
    public GameObject trashPrefab2;
    public GameObject trashPrefab3;

    // Jumlah sampah yang akan diinstansiasi di setiap tag
    public int numberOfTrashPerTag = 1;

    // Ketinggian y di mana sampah akan ditempatkan
    public float trashHeight = 70f;

    void Start()
    {
        // Memastikan prefab telah diisi
        if (trashPrefab1 == null || trashPrefab2 == null || trashPrefab3 == null)
        {
            Debug.LogError("Prefab sampah belum diisi.");
            return;
        }

        // Temukan semua GameObject dengan tag 'Street'
        GameObject[] streetObjects = GameObject.FindGameObjectsWithTag("Street");
        SpawnTrashAtObjects(streetObjects);

        // Temukan semua GameObject dengan tag 'Halaman'
        GameObject[] halamanObjects = GameObject.FindGameObjectsWithTag("Halaman");
        SpawnTrashAtObjects(halamanObjects);
    }

    void SpawnTrashAtObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            for (int i = 0; i < numberOfTrashPerTag; i++)
            {
                // Tentukan posisi acak di sekitar objek, dengan ketinggian y tetap 70
                Vector3 randomPosition = obj.transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                randomPosition.y = trashHeight; // Set posisi y ke 70

                // Pilih prefab sampah secara acak
                GameObject selectedTrashPrefab = GetRandomTrashPrefab();

                // Instansiasi objek sampah
                GameObject newTrash = Instantiate(selectedTrashPrefab, randomPosition, Quaternion.identity);
                newTrash.transform.position = new Vector3(newTrash.transform.position.x, trashHeight, newTrash.transform.position.z); // Pastikan posisi y tetap 70
            }
        }
    }

    GameObject GetRandomTrashPrefab()
    {
        float randomValue = Random.value;
        if (randomValue < 0.33f)
        {
            return trashPrefab1;
        }
        else if (randomValue < 0.66f)
        {
            return trashPrefab2;
        }
        else
        {
            return trashPrefab3;
        }
    }
}
