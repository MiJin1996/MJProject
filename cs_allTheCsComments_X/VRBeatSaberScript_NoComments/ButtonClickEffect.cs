using UnityEngine;

public class ButtonClickEffect : MonoBehaviour
{
    [Header("--- 클릭 이펙트 설정 ---")]
    public GameObject clickEffectPrefab;
    public float destroyDelay = 2f;

    public void PlayClickEffect()
    {
        if (clickEffectPrefab == null) return;

        Vector3 buttonPosition = transform.position;

        GameObject effect = Instantiate(clickEffectPrefab, buttonPosition, Quaternion.identity);

        effect.transform.SetParent(transform.parent, false);
        effect.transform.position = buttonPosition;
        effect.transform.localScale = Vector3.one;

        Destroy(effect, destroyDelay);
    }
}

