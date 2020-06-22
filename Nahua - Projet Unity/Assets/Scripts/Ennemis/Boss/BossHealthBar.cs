using UnityEngine;
using UnityEngine.UI;

namespace Ennemis
{
    public class BossHealthBar : MonoBehaviour
    {
        public Slider healthBar;
        [SerializeField] private Scr_BossLifeSystem bossLifeSyst;

        private void Start()
        {
            healthBar.maxValue = bossLifeSyst._life;
            healthBar.value = bossLifeSyst._life;
        }

        private void Update()
        {
            healthBar.value = bossLifeSyst._life;
        }

    }
}