using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsometricRandomNPCController : IsometricNPC
{
    [SerializeField]
    private Image npcImage;

    [SerializeField]
    private List<Sprite> npcSprites;

    void Start()
    {
        npcImage.sprite = npcSprites[Random.Range(0, npcSprites.Count)];
    }
}
