using UnityEngine;

public class ParticleEffecter : MonoBehaviour
{
    [SerializeField] GameObject _particleObject;
    
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = _particleObject.GetComponent<ParticleSystem>();
    }
    public void StartParticle()
    {
        GameObject particleObject = Instantiate(_particleObject);
        particleObject.transform.position = _particleObject.transform.position;
        particleObject.transform.localScale = _particleObject.transform.localScale;
        
        particleObject.SetActive(true);
        //MoveByZ script = particleObject.AddComponent<MoveByZ>();
        //script._zDestroyPosition = -50;
        Destroy(particleObject, 2f);
    }
}
