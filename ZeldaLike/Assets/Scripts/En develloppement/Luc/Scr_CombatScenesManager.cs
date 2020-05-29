using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CombatScenesManager : MonoBehaviour
{
    public List<GameObject> _actualFightScenes;
    public List<GameObject> _dataFightScenes;
    [SerializeField] private List<Vector3> _fightScenesPos;


    private void Start()
    {
        for (int j = 0; j < _actualFightScenes.Count; j++)
        {
            _fightScenesPos.Add(_actualFightScenes[j].transform.position);
        }
    }

    public void RespawnFightReset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _actualFightScenes.Clear();
        _actualFightScenes.TrimExcess();

        for (int i = 0; i < _dataFightScenes.Count; i++)
        {
            Instantiate(_dataFightScenes[i], _fightScenesPos[i], Quaternion.identity, transform);
        }

        foreach (Transform child in transform)
        {
            _actualFightScenes.Add(child.gameObject);
        }
    }
}
