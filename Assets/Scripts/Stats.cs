using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Statistiques Personnage :")] 
    public int PV;
    public int attaque;
    public int vitesse;
    public int armure;
    public int tauxCritique; // peut-être mettre en float pour %
    public int degatsCritique; // peut-être mettre en float pour %

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

;   }

    // Fonctions :

    // PV :
    public void ajouterPV(int valeur)
    {
        PV += valeur;
    }

    public void retirerPV(int valeur)
    {
        PV -= valeur;
    }

    // Attaque :

    // Vitesse :

    // Armure : 

    // TauxCritique :

    // DégatsCritiques :

}
