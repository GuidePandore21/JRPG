using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;
using Random = UnityEngine.Random;
using TMPro;

public class TourParTour : MonoBehaviour
{
    public List<GameObject> listePersonnages;
    private List<GameObject> listeAllies;
    private List<GameObject> listeEnnemis;

    private bool winAllies;
    private bool winEnnemis;
    private bool fincombat;

    private bool isAttacking = false;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        initCombat();
    }

    // Update is called once per frame
    void Update()
    {

        if (fincombat == false)
        {
            if (!isAttacking)
            {
                if (QuiJoue().GetComponent<VieController>().isAlive)
                {
                    DebutTour();

                    Joue();

                    finTour();
                }

                NextTurn();

                checkFinCombat();
            }
        }
    }



    // ---------------------------------------------------------------------------------------
    // Fonctions Tour par Tour

    public void Joue()
    {
        int damage;

        GameObject Joueur = QuiJoue();
        GameObject cible = RandomTarget();

        while(!cible.GetComponent<VieController>().isAlive)
        {
            cible = RandomTarget();
        }

        damage = Joueur.GetComponent<AttaqueController>().lanceAttaque(PlayRandomInput(), cible);

        StartCoroutine(animationCombat(cible));
    }
    public void NextTurn()
    {
        index++;

        if (index >= listePersonnages.Count)
        {
            index = 0;
        }
        
        DebutTour();
    }
    public void initCombat()
    {
        triListePersonnage();

        listeAllies = new List<GameObject>();
        listeEnnemis = new List<GameObject>();

        TriAllieEnnemi();

        winAllies = false;
        winEnnemis = false;
        fincombat = false;
    }


    public void DebutTour()
    {
        
    }
    public void finTour()
    {
        return;
    }

    public void checkFinCombat()
    {
        winAllies = true;

        for (int i = 0; i < listeEnnemis.Count; i++)
        {
            if (listeEnnemis[i].GetComponent<VieController>().isAlive)
            {
                winAllies = false; break;
            }
        }

        winEnnemis = true;

        for (int i = 0; i < listeAllies.Count; i++)
        {
            if (listeAllies[i].GetComponent<VieController>().isAlive)
            {
                winEnnemis = false; break;
            }
        }

        if (winAllies || winEnnemis)
        {
            fincombat = true;
        }
    }

    public GameObject QuiJoue()
    {
        return listePersonnages[index];
    }

    public int PlayRandomInput()
    {
        int nbAttaque = QuiJoue().GetComponent<AttaqueController>().attaques.Count;
        return Random.Range(0, nbAttaque);
    }

    public GameObject RandomTarget()
    {
        if (QuiJoue().GetComponent<Personnage>().Allie)
        {
            int target = Random.Range(0, listeEnnemis.Count);
            return listeEnnemis[target];
        }
        else
        {
            int target = Random.Range(0, listeAllies.Count);
            return listeAllies[target];
        }
    }

    // ---------------------------------------------------------------------------------------
    // Coroutines

    IEnumerator animationCombat(GameObject cible)
    {
        isAttacking = true;
        // lancer animation
        GetComponent<Personnage>().animator.SetBool("isAttacking", true);
        GetComponent<Personnage>().animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(2.0f);
        isAttacking = false;
    }



    // ---------------------------------------------------------------------------------------
    // Fonctions de tris

    public void triListePersonnage() // A opti plus tard
    {
        List<GameObject> intermediaire = new List<GameObject>();
        intermediaire.AddRange(listePersonnages);

        listePersonnages.Clear();

        int plusGrandeVitesse = -1;
        int index = 0;

        while (intermediaire.Count != 0)
        {
            for (int i = 0; i < intermediaire.Count; i++)
            {
                if (i == 0)
                {
                    plusGrandeVitesse = intermediaire[0].GetComponent<Stats>().vitesse;
                    index = 0;
                }
                else
                {
                    if (plusGrandeVitesse < intermediaire[i].GetComponent<Stats>().vitesse)
                    {
                        plusGrandeVitesse = intermediaire[i].GetComponent<Stats>().vitesse;
                        index = i;
                    }
                }
            }
            listePersonnages.Add(intermediaire[index]);
            intermediaire.RemoveAt(index);
        }
    }

    public void TriAllieEnnemi()
    {
        listeAllies.Clear();
        listeEnnemis.Clear();
        for(int i = 0; i < listePersonnages.Count; i++)
        {
            if (listePersonnages[i].GetComponent<Personnage>().Allie)
            {
                listeAllies.Add(listePersonnages[i]);            
            }
            else
            {
                listeEnnemis.Add(listePersonnages[i]);
            }
        }
    }

    
}
