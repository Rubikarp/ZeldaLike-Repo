using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New_EchangeInteractif", menuName = "Dialogue/EchangeInteractif")]
    public class EchangeInteractif : ScriptableObject
    {
        [Header("Interlocuteur A qui parle à B")]
        public string interlocuteur_A, interlocuteur_B;

        [Header("Phrase/Question de A")]
        [TextArea(2, 10)] public string phrase_A0;
        public float RespondTime = 4f;

        [Header("Réaction/Réponse de B")]
        [TextArea(2, 6)] public string reaction_B1, reaction_B2;

        [Header("Réaction/Réponse de A")]
        [TextArea(2, 6)] public string answer_A1, answer_A2;
    }
}