using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssembleSetup : MonoBehaviour
{
    /*
        This script's point is to setup the game, including creating the areas, the spots, the tokens, and the result placeholder.
        - receive the number of players
        - receive the global variable indicating which try it is
        - create the areas
        - receive a sentence
        - split the sentence depending on the number of tries and create the tokens
        - create the result placeholders
        - create the spots
        - distribute the tokens so that one has one valid spot and one valid area
    */

    public int numberOfPlayers;
    public int numberOfTries;
    public string sentence;

    public GameObject squareTemplate;
    public Canvas textTemplate;

    void Start()
    {
        createAreas(numberOfPlayers);
    }

    void createAreas(int nbPlayers)
    {
        createArea(1, new Vector3(-7, 0, 0), new Vector3(3, 7, 1));
        createArea(2, new Vector3(7, 0, 0), new Vector3(3, 7, 1));
        if (nbPlayers == 3)
        {
            createArea(3, new Vector3(0, -3, 0), new Vector3(7, 3, 1));
        }
    }

    void createArea(int numArea, Vector3 position, Vector3 scale)
    {
        GameObject area = Instantiate(squareTemplate);
        area.name = "Area " + numArea;
        area.tag = "Area";
        area.SetActive(true);
        area.transform.position = position;
        area.transform.localScale = scale;
    }

    void createTokens()
    {
        // split the sentence depending on the number of tries and create the tokens
        // create the result placeholders
        // distribute the tokens so that one has one valid spot and one valid area
    }

    void createSpots()
    {
        // create the spots
    }




}
