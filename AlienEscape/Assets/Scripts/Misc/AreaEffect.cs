using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    private bool damageTick = false;
    private float damageTime = 120;
    private float potionTime = 420;

    [SerializeField]
    private List<EnemyAI> enemylist = new List<EnemyAI>();

    private void Start() {
        PotionItem pItem = (PotionItem)gameObject.GetComponent<ThrowableGameObj>().GetItem();
        if(pItem.potionSort == 1)
        {
            Sprite Poisonpotion = Resources.Load<Sprite>("Poisonpotion");
            Sprite PoisonEffect = Resources.Load<Sprite>("Poisoneffect");
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = Poisonpotion;
            ParticleSystem partSys = gameObject.GetComponent<ParticleSystem>(); 
            ParticleSystemRenderer partSysRen = gameObject.GetComponent<ParticleSystemRenderer>();
            partSys.Stop();
            poisonEffect(partSys, PoisonEffect, partSysRen);
            partSys.Play();
        }
        if(pItem.potionSort == 2)
        {
            Sprite Firepotion = Resources.Load<Sprite>("Firepotion");
            Sprite FireEffect = Resources.Load<Sprite>("Fireeffect");
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            spriteR.sprite = Firepotion;
            ParticleSystem partSys = gameObject.GetComponent<ParticleSystem>(); 
            ParticleSystemRenderer partSysRen = gameObject.GetComponent<ParticleSystemRenderer>();
            partSys.Stop();
            poisonEffect(partSys, FireEffect, partSysRen);
            partSys.Play();
        }
    }
    private void Update() {
        potionTime -= 1;
        if(potionTime <= 0)
        {
            destroyEffect(gameObject);
        }
        if(damageTick){
            damageTime -= 1;
            if(damageTime <= 0) 
            {
                for(int i = 0; i < enemylist.Count; i++)
                {
                    enemylist[i].DamageHP(4);
                }
                damageTime = 120;
            }
        }     
    }
    
    public void destroyEffect(GameObject gameObject)
    {
        Destroy(gameObject);
        PotionItem.effectDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageTick = true;
        if(collision.GetComponent<EnemyAI>())
        {
            enemylist.Add(collision.GetComponent<EnemyAI>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        damageTick = false;
    }

    private void poisonEffect(ParticleSystem partSys, Sprite PoisonEffect, ParticleSystemRenderer partSysRen)
    {
        var main = partSys.main;
        var emission = partSys.emission;
        var shape = partSys.shape;
        shape.enabled = true;
        var tsa = partSys.textureSheetAnimation;
        tsa.enabled = true;
        var color = partSys.colorOverLifetime;
        color.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.white, 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 1.0f), new GradientAlphaKey(1.0f, 0.0f)});        
        color.color = grad;
        partSysRen.maxParticleSize = 0.025f;
        partSysRen.sortingLayerName = "Characters";
        partSysRen.material = (Material)Resources.Load("Default", typeof(Material));    
        tsa.mode = ParticleSystemAnimationMode.Sprites;
        tsa.AddSprite(PoisonEffect);
        tsa.SetSprite(0, PoisonEffect);
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 1.0f;
        emission.rateOverTime = 5.0f;
        emission.rateOverDistance = 0.0f;
        main.duration = 1.0f;
        main.startDelay = 0.0f;
        main.startLifetime = 4.0f;
        main.startSpeed = 0.25f;
        main.startSize = 1.0f;
        main.maxParticles = 50;
        main.loop = true;
    }
    private void fireEffect(ParticleSystem partSys, Sprite FireEffect, ParticleSystemRenderer partSysRen)
    {
        var main = partSys.main;
        var emission = partSys.emission;
        var shape = partSys.shape;
        shape.enabled = true;
        var tsa = partSys.textureSheetAnimation;
        tsa.enabled = true;
        var color = partSys.colorOverLifetime;
        color.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.white, 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 1.0f), new GradientAlphaKey(1.0f, 0.0f)});        
        color.color = grad;
        partSysRen.maxParticleSize = 0.025f;
        partSysRen.sortingLayerName = "Characters";
        partSysRen.material = (Material)Resources.Load("Default", typeof(Material));    
        tsa.mode = ParticleSystemAnimationMode.Sprites;
        tsa.AddSprite(FireEffect);
        tsa.SetSprite(0, FireEffect);
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 1.0f;
        emission.rateOverTime = 5.0f;
        emission.rateOverDistance = 0.0f;
        main.duration = 1.0f;
        main.startDelay = 0.0f;
        main.startLifetime = 4.0f;
        main.startSpeed = 0.25f;
        main.startSize = 1.0f;
        main.maxParticles = 50;
        main.loop = true;
    }
}
