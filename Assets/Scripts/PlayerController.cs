using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour // CLASSE QUI CONTROLE LES DEPLACEMENT DU JOUEUR
{
    
    public float moveSpeeed;// VITESSE DE DEPLACEMENT DU JOUEUR
    private bool isMoving;// STATUT SI LE JOUEUR BOUGE OU PAS

    private Vector2 input; // VECTEUR DE POSITION ET DE DEPLACEMENT

    private Animator animator; // Anmiation du joueur

    private void Awake() //S'execute des le d�but du programme
    {
        animator = GetComponent<Animator>();
    }

    // Update est appeler � chaque Frame 
    private void Update()
    {
        // si le joueur n'est pas en train de bouger
        if (!isMoving)
        {
            //On r�cupere le statut des fleches directionnel
            input.x = Input.GetAxisRaw("Horizontal");// Fleche haut/bas
            input.y = Input.GetAxisRaw("Vertical");// Fleche droite/gauche

            //afficher les valeurs dans la console
            Debug.Log("Input.x = " + input.x);
            Debug.Log("Input.y = " + input.y);


            if (input.x != 0 ) { input.y = 0; } // Fait qu'on peut pas se d�placer ne diagonale

            if(input != Vector2.zero)// Si on � appuyer sur un touche
            {

                //Partie animation

                animator.SetFloat("moveX", input.x);// On met la valeurs moveX de l'animator �gale � la valeurs de la direction � la quelle on va
                animator.SetFloat("moveY", input.y);

                //Fin anim

                // on d�finit une variable qui possede la postion actuelle du joueur
                var targetPos = transform.position;

                //On ajoute � la position du joueur la direction � laquelle on veut allez
                targetPos.x += input.x;
                targetPos.y += input.y;
                
                //Puis on execute parallement la fonction Move j'usqu'a quelle se met en pause
                StartCoroutine(Move(targetPos));
            }
        }
        
        animator.SetBool("isMoving", isMoving);// on met la valeur 'isMoving' de l'animator qui est �gale � la valeurs du script isMoving
    }

    //Fonction qui effectuer les d�placement du joueur
    IEnumerator Move(Vector3 targetPos)
    {
        //Comme on va se d�placer on met isMoving � true
        isMoving = true;

        //tant que la postion de d'arriver n'est pas �gale � celle ou on est
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //On bouge notre personnage vers cette position avec sa vitesse
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeeed * Time.deltaTime);
            yield return null;//On met en pause le thread
        }
        //Des que on est arriver on met la position du personnage exactement � la ou on devais arriver
        transform.position = targetPos;
        //Puis on met qu'on est pas en train de bouger
        isMoving = false;
    }
}
