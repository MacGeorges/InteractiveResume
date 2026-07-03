using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IsometricGameManager : MonoBehaviour
{
    [SerializeField]
    private IsometricAudioManager audioManager;

    [SerializeField]
    private GameObject scoreScreen;

    [SerializeField]
    private IsometricFighterNPC boss;

    [SerializeField]
    private Transform NPCRoot;

    [SerializeField] //Displayed for debug
    private List<IsometricFighterNPC> NPCs;

    private void Start()
    {
        boss.OnNPCDie.AddListener(OnBossDead);

        NPCs = NPCRoot.GetComponentsInChildren<IsometricFighterNPC>().ToList();

        foreach (IsometricFighterNPC NPC in NPCs)
        {
            NPC.OnNPCDie.AddListener(() => OnNPCDie(NPC));
        }
    }

    private void OnNPCDie(IsometricFighterNPC NPC)
    {
        NPCs.Remove(NPC);

        if(NPCs.Count == 0)
        {
            boss.gameObject.SetActive(true);
        }
    }

    private void OnBossDead()
    {
        scoreScreen.SetActive(true);
        audioManager.StopArenaMusic();
    }
}
