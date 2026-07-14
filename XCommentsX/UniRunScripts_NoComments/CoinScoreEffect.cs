using UnityEngine;
using BlackMassSoftware.FloatingTextEngine.Lite;
using BlackMassSoftware.FloatingTextEngine.Lite.Behaviors;

public class CoinScoreEffect : MonoBehaviour
{
    [Header("Floating Text")]
    public Vector3 textOffset = new Vector3(0f, 0.8f, 0f);
    public Color textColor = Color.yellow;

    [Header("Effect")]
    public float popScale = 1.4f;
    public float moveUpDistance = 1.2f;
    public float effectDuration = 0.6f;

    public void Play(int scoreAmount)
    {
        Vector3 spawnPosition = transform.position + textOffset;

        FloatingTextEngine
            .CreateTextAtWorldSpace(spawnPosition, "+" + scoreAmount, textColor)
            .With(FloatingTextBehaviors.Pop(popScale, 0.08f, 0.15f))
            .With(FloatingTextBehaviors.MoveUp(moveUpDistance, effectDuration))
            .With(FloatingTextBehaviors.FadeOut(effectDuration));
    }
}
