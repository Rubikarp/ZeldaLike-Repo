using UnityEngine;
using Management;

namespace Game
{
    public class MarkBehaviour : MonoBehaviour
    {
        private GameObject _player;

        [SerializeField] private InputManager _input = null;
        [SerializeField] private Scr_FormeHandler _forme = default;

        private OnSwitch_MarkApparrition _human = null;
        private OnSwitch_SpeedBoost _heavy = null;
        private OnSwitch_StunZoneApparrition _agile = null;
        public float _distanceLimit;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();

            _forme = _player.GetComponent<Scr_FormeHandler>();

            if (_forme._switchForm == Scr_FormeHandler.Forme.Humain)
            {
                 _human = GameObject.FindGameObjectWithTag("Player/Forme/Human").GetComponent<OnSwitch_MarkApparrition>();
            }
            if (_forme._switchForm == Scr_FormeHandler.Forme.Agile)
            {
                _agile = GameObject.FindGameObjectWithTag("Player/Forme/Agile").GetComponent<OnSwitch_StunZoneApparrition>(); 
            }
            if (_forme._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                 _heavy = GameObject.FindGameObjectWithTag("Player/Forme/Heavy").GetComponent<OnSwitch_SpeedBoost>(); 
            }

        }

        void Update()
        {
            if (_input._mark)
            {
                _player.transform.position = gameObject.transform.position;

                if (_forme._switchForm == Scr_FormeHandler.Forme.Humain)
                {
                    if(_human == null)
                    { _human = GameObject.FindGameObjectWithTag("Player/Forme/Human").GetComponent<OnSwitch_MarkApparrition>(); }

                    //En fait je garde juste la marque vu que la pos ne change pas
                    //_human.MarkApparrition();

                }
                else
                if (_forme._switchForm == Scr_FormeHandler.Forme.Agile)
                {
                    if (_agile == null)
                    { _agile = GameObject.FindGameObjectWithTag("Player/Forme/Agile").GetComponent<OnSwitch_StunZoneApparrition>(); }

                    _agile.StunZoneApparrition();
                    Destroy(gameObject, 0.2f);

                }
                else
                if (_forme._switchForm == Scr_FormeHandler.Forme.Heavy)
                {
                    if (_heavy == null)
                    { _heavy = GameObject.FindGameObjectWithTag("Player/Forme/Heavy").GetComponent<OnSwitch_SpeedBoost>(); }

                    _heavy.SpeedBoost();
                    Destroy(gameObject, 0.2f);

                }
            }

            if (Vector2.Distance(_player.transform.position, transform.position) > _distanceLimit)
            {
                transform.position = _player.transform.position;
            }
        }

    }
}
