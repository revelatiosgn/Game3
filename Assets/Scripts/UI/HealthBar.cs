using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Stats;

namespace ARPG.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] CharacterStats characterStats;
        [SerializeField] Image background;
        [SerializeField] Image healthPoints;

        void Update()
        {
            float percent = characterStats.health / 100f;
            healthPoints.transform.localScale = new Vector3(percent, 1f, 1f);

            bool isEnabled = 0f < percent && percent < 1f;
            background.enabled = isEnabled;
            healthPoints.enabled = isEnabled;
        }
    }
}


