using UnityEngine;

public class BonusSouls : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound = null;
    [SerializeField] private int minValue = 3;
    [SerializeField] private int maxValue = 7;
    private int value = 0;
    
    private void OnEnable()
    {
        value = Random.Range(minValue, maxValue + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerStatus>().HarvestRewards(value);
            PoolManager.instance.GetObject("CollectSouls", transform.position, Quaternion.identity);
            AudioManager.Instance.PlayOneShot(breakSound, .4f);
            this.gameObject.SetActive(false);
        }
    }
}
