using UnityEngine;

public class TrashManager : MonoBehaviour
{
    private int trashCount = 0; // Variabel untuk menyimpan jumlah sampah

    // Metode untuk menambah jumlah sampah
    public void AddTrash()
    {
        trashCount++;
        Debug.Log("Trash collected: " + trashCount);
    }

    // Metode untuk mereset jumlah sampah
    public void ResetTrashCount()
    {
        trashCount = 0;
        Debug.Log("Trash count reset.");
    }
}
