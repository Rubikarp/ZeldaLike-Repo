using Game;
using UnityEngine;

public class Scr_CombatEnemiesTheme : MonoBehaviour
{
    private MusicManager _music;

    private void Awake()
    {
        _music = MusicManager.Instance;
    }

    private void OnEnable()
    {
        _music.StopMusic("OST_MondeOuvert");
        _music.PlayMusic("OST_CombatEnemies");
    }

    private void OnDisable()
    {
        _music.StopMusic("OST_CombatEnemies");
        _music.PlayMusic("OST_MondeOuvert");
    }
}
