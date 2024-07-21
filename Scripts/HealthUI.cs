using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public List<Image> hearts; // Daftar Image untuk hati
    public Sprite heartSprite; // Sprite untuk hati penuh
    public Sprite emptyHeartSprite; // Sprite untuk hati kosong

    public void SetHealth(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = heartSprite;
            }
            else
            {
                hearts[i].sprite = emptyHeartSprite;
            }
        }
    }
}
