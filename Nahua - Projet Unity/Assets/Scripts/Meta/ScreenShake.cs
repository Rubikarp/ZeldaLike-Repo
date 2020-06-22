using UnityEngine;

namespace Management
{
    public class ScreenShake : MonoBehaviour
    {
        //Source https://youtu.be/tu-Qe66AvtY

        [Range(0, 1)] public float trauma;
        [Range(0, 1)] private float cameraShake;

        [Range(1, 4)] [SerializeField] float traumaMultiply = 2;
        [Range(1, 60)] [SerializeField] float traumaFrequency = 12;
        [Range(0, 10)] [SerializeField] float traumaMagnitudeTranslate = 12f;
        [Range(0, 90)] [SerializeField] float traumaMagnitudeRotation = 18f;
        [Range(0, 3)] [SerializeField] float traumaFallFactor = 1.24f;

        float cameraTime = 0;

        private void Update()
        {
            trauma = Mathf.Clamp01(trauma);
            cameraShake = trauma * trauma;

            if (cameraShake > 0)
            {
                //Le temps de la camera avance
                cameraTime += Time.deltaTime * traumaFrequency;

                //Calcul du mouvement de la caméra pour trembler
                Vector2 newPosTranslate = PerlinNoiseVector(100, 200);
                Vector3 newPosRotate = new Vector3(0, 0, PerlinNoiseRandom(400));

                //Movement de la camera multiplié par la puissance du screenshake
                transform.localPosition = newPosTranslate * traumaMagnitudeTranslate * cameraShake;
                //Rotation de la camera
                transform.localRotation = Quaternion.Euler(newPosRotate * traumaMagnitudeRotation * cameraShake);

                //Déclin du screen shake
                trauma -= Time.deltaTime * (1 / traumaFallFactor) + (cameraShake / 300);
            }
            else
            {
                Vector2 newPos = Vector2.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
                float newRot = Mathf.Lerp(transform.localRotation.z, 0, Time.deltaTime);

                //Movement de la camera
                transform.localPosition = Vector2.zero;
                //Rotation de la camera
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, newRot));
            }
        }

        Vector2 PerlinNoiseVector(float seed1, float seed2)
        {
            return new Vector2(PerlinNoiseRandom(seed1), PerlinNoiseRandom(seed2));
        }

        float PerlinNoiseRandom(float seed)
        {
            float value;

            //valeur entre 0 et 1
            value = Mathf.PerlinNoise(seed, cameraTime);
            //valeur entre -1 et 1
            value = (value - 0.5f) / 2;
            //valeur entre -traumaMultiply et traumaMultiply
            value *= traumaMultiply;

            return value;
        }
    }
}