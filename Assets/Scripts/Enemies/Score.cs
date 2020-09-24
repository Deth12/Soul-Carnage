using UnityEngine;

public class Score : MonoBehaviour, IHittable
{
    [Header("Kill Rewards")] 
    public int ScoreReward = 1;
    public int SoulsReward = 1;
    public float SoulEnergyReward = 10f;
    
    [Header("Movement")]
    [SerializeField] float maxRunSpeed = 10f;
    [SerializeField] float minRunSpeed = 2f;
    [SerializeField] float runSpeed = 5f;
    
    [Header("Sounds")]
    [SerializeField] AudioClip[] deathSounds = null;

    private Animator anim;
    private Transform player;
    private SphereCollider col;
    
    private void Start()
    {
        player = GameManager.Instance.Player;
        col = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        if(anim == null)
            anim = GetComponentInChildren<Animator>();
        anim.SetInteger("Run", Random.Range(1, 7));
        runSpeed = Random.Range(minRunSpeed, maxRunSpeed);
    }

    private void Update()
    {
        if (GameManager.Instance.CanEnemyMove)
        {
            transform.Translate(Vector3.forward * (runSpeed * Time.deltaTime));
        }
    }

    private void Disappear()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.3f);
        PoolManager.instance.GetObject("Blood", spawnPos, Quaternion.identity);
        Vector3 decalPos = new Vector3(col.bounds.min.x, col.bounds.min.y, col.bounds.min.z + 2.3f);
        PoolManager.instance.GetObject("Blood_Decals_" + Random.Range(1,4), decalPos, Quaternion.identity);
        //anim.SetTrigger("Death");
        if(deathSounds.Length != 0)
            AudioManager.Instance.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length)], 1f);
        anim.SetInteger("Run", 0);
        this.gameObject.SetActive(false);
    }
    
    public void TakeHit(PlayerStatus status)
    {
        status.HarvestKillRewards(ScoreReward, SoulsReward, SoulEnergyReward);
        Disappear();
    }

}