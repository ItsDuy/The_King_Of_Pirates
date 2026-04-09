using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class LiveBar : MonoBehaviour
{
    
    [SerializeField] private GameObject[] heartImages;
    public void MinusHeart()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i].activeSelf)
            {
                heartImages[i].SetActive(false);
                break;
            }
        }
    }
}
