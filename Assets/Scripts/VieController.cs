using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VieController : MonoBehaviour
{
    public int currentPV;
    public TourParTour tourParTour;
    public bool isAlive = true;

    public TextMeshProUGUI texteDegatSubit;


    // Start is called before the first frame update
    void Start()
    {
        initCurrentPV();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ---------------------------------------------------------------------------------------
    // Fonction PV

    public void initCurrentPV() // initialise les currentPV avec les PV de base
    {
        currentPV = this.GetComponent<Stats>().PV;
        isAlive = true;

    }

    public void subirAttaque(GameObject cible, int damage) // retranche à la vie les dégats d'une attaque
    {
        currentPV -= damage;
        StartCoroutine(affichageDegats(damage));
        isDead();
    }

    public void isDead() // vérifie si le personnage est mort
    {
        if (currentPV <= 0)
        {
            gameObject.SetActive(false);

            isAlive = false;
        }
        else
        {
            isAlive = true;
        }
    }

    // ---------------------------------------------------------------------------------------
    // Coroutines

    IEnumerator affichageDegats(int degat)
    {
        texteDegatSubit.text = "- " + degat;

        texteDegatSubit.enabled = true;
        yield return new WaitForSeconds(2.00f);
        texteDegatSubit.enabled = false;
        yield return new WaitForSeconds(1.00f);
    }

}
