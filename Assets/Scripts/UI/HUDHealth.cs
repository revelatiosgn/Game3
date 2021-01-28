using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Stats;

namespace ARPG.UI
{
    public class HUDHealth : MonoBehaviour
    {
        CharacterStats playerStats;
        Text text;

        void Start()
        {
            playerStats = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<CharacterStats>();
            text = GetComponent<Text>();
        }

        void Update()
        {
            text.text = playerStats.health.ToString();
        }
    }
}


