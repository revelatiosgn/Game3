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
        [SerializeField] Image healthPoints;

        void Update()
        {
            float percent = characterStats.health / 100f;
            healthPoints.transform.localScale = new Vector3(percent, 1f, 1f);

            if (percent <= 0f)
                gameObject.SetActive(false);
        }
    }
}


