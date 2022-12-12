using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class AttaqueController : MonoBehaviour
{
    public static Stats statistiques;

    public List<Attaque> attaques;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int lanceAttaque(int index, GameObject target)
    {
        int damage = 0;

        for(int i = 0; i < attaques[index].nbHits; i++)
        {
            damage += (int) calculDamage(attaques[index]);
        }

        target.GetComponent<VieController>().subirAttaque(target, damage);

        return damage;
        
    }

    public float calculDamage(Attaque attaque)
    {
        return this.GetComponent<Stats>().attaque * attaque.coefAttaque;
    }

}
