using UnityEngine;
using TMPro;


public class PiggyBankSlot : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip coinClip;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Behavior")]
    public bool destroyCoin = true;       // or false to stash coins
    public Transform stashPoint;          // optional (set if destroyCoin=false)

    int score;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Coin")) return;

        score++;
        if (scoreText) scoreText.text = $"Coins: {score}";
        if (audioSource && coinClip) audioSource.PlayOneShot(coinClip);

        // tidy up the coin
        var rb = other.attachedRigidbody;
        if (destroyCoin)
        {
            Destroy(other.gameObject);
        }
        else
        {
            if (rb) rb.isKinematic = true;
            other.transform.SetParent(transform.root, true);
            if (stashPoint) other.transform.position = stashPoint.position;
        }
    }

    
}

