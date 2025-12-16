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

    public void Init()
    {
        npcImage.sprite = npcSprites[Random.Range(0, npcSprites.Count)];
    }
}
