using UnityEngine;
using UnityEngine.UI;

namespace Ennemis
{
    public class EnnemisHealthBar : MonoBehaviour
    {
        public Slider healthBar;
        [SerializeField] private Scr_EnnemisLifeSystem enemisLifeSyst;

        private void Start()
        {
            healthBar.maxValue = enemisLifeSyst._life;
            healthBar.value = enemisLifeSyst._life;
        }

        private void Update()
        {
            healthBar.value = enemisLifeSyst._life;
        }

    }
}