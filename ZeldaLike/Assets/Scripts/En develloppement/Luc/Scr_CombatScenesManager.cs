using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CombatScenesManager : MonoBehaviour
{
    public List<GameObject> _actualFightScenes;
    public List<GameObject> _dataFightScenes;
    [SerializeField] private List<Vector3> _fightScenesPos;
    [SerializeField] private List<string> _fightScenesNames;
    private string _replacedFSBarrier;

    private void Start()
    {
        for (int j = 0; j < _actualFightScenes.Count; j++)
        {
            _fightScenesPos.Add(_actualFightScenes[j].transform.position);
        }

        for (int k = 0; k < _actualFightScenes.Count; k++)
        {
            _fightScenesNames.Add(_dataFightScenes[k].name);
        }
    }

    public void RespawnFightReset()
    {
        for (int i  = 0; i < _actualFightScenes.Count; i++)
        {
           if (_actualFightScenes[i].GetComponentInChildren<Scr_LD_SpawnEnnemies>()._fightStarted == true)
           {
                _replacedFSBarrier = _actualFightScenes[i].GetComponentInChildren<Scr_LD_SpawnEnnemies>()._limitTilemap.name;
                Destroy(_actualFightScenes[i]);
                Instantiate(_dataFightScenes[i], _fightScenesPos[i], transform.rotation, transform);
                _actualFightScenes[i] = GameObject.Find(_fightScenesNames[i] + "(Clone)");
                _actualFightScenes[i].GetComponentInChildren<Scr_LD_SpawnEnnemies>()._limitTilemap = GameObject.Find(_replacedFSBarrier);
           }
        }
    }
}
