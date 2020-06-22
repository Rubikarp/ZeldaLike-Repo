using UnityEngine;
using Management;

namespace Game
{
    public class SoundEffectHandler_Player : MonoBehaviour
    {
        private SoundManager sound;

        [SerializeField] private Movement_2D_TopDown mouvement;
        [SerializeField] private Scr_FormeHandler forme;

        [SerializeField] private Actif_HeavyAttack Actif_Heavy;
        [SerializeField] private Actif_AgileAttack Actif_Agile;
        [SerializeField] private Actif_KnifeThrowing Actif_Humain;

        [SerializeField] private OnSwitch_SpeedBoost OnSwitch_Heavy;
        [SerializeField] private OnSwitch_StunZoneApparrition OnSwitch_Agile;
        [SerializeField] private OnSwitch_MarkApparrition OnSwitch_Humain;



        void Awake()
        {
            sound = SoundManager.Instance;
        }

        void Start()
        {
            
        }
        
        void Update()
        {

        }
        
        
        
    }
}