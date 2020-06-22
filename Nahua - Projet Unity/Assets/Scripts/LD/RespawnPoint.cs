using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Management
{
    [RequireComponent(typeof(Collider2D))]
    public class RespawnPoint : MonoBehaviour
    {
        [SerializeField] Transform respawnPos = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player/HurtBox"))
            {
                var lifeSystem = collision.gameObject.GetComponent<Scr_PlayerLifeSystem>();
                lifeSystem._respawnPoint = respawnPos.position;
            }
        }
    }
}