using UnityEngine;
using System.Collections;

namespace Ennemis
{
    public class Ennemis_NoBrain : MonoBehaviour
    {
        [Header("Stat")]
        public float _dashSpeed = 10f;
        public float _dashDuration = 0.5f;
        public float _dashDelay = 0.5f;

        public Rigidbody2D body;

        private void Start()
        {
            body = this.gameObject.GetComponent<Rigidbody2D>();
        }

        IEnumerator Dash(float CoolDown)
        {
            float time = 0f;

            /*attackZone.SetActive(true);
            haveAttack = true;
            soundManager.Play("RobotAttack");


            while (attackDuration > time)
            {
                time += Time.deltaTime;
                myBody.velocity = playerDirection.normalized * dashSpeed;
                yield return 0;
            }

            attackZone.SetActive(false);
            _my.velocity = Vector2.zero;

            */
            yield return new WaitForSeconds(CoolDown);

        }
    }
}