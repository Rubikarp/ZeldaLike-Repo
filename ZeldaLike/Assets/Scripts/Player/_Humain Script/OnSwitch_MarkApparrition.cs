﻿using UnityEngine;

namespace Game
{
    public class OnSwitch_MarkApparrition : Singleton<OnSwitch_MarkApparrition>
    {
        [SerializeField] GameObject _markPrefab = null;
        [SerializeField] Transform  _markContainer = null;
        [SerializeField] private SoundManager sound;

        void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void OnEnable()
        {
            //Pour ne laisser qu'une marque active
            if(_markContainer.childCount >=1)
            {
                Destroy(_markContainer.GetChild(0).gameObject);
            }
            
            Instantiate(_markPrefab, this.transform.position, Quaternion.identity, _markContainer);
        }

        public void MarkApparrition()
        {
            sound.PlaySound("OnSwitchMark");

            if (_markContainer.childCount >= 1)
            {
                Destroy(_markContainer.GetChild(0).gameObject);
            }

            Instantiate(_markPrefab, this.transform.position, Quaternion.identity, _markContainer);

        }
    }
}