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
        X create the areas
        - receive a sentence
        X split the sentence depending on the number of tries and create the tokens
        X create the result placeholders
        - create the spots
        - distribute the tokens so that one has one valid spot and one valid area
    */

    const int MAX_NUMBER_OF_PLAYERS = 3;
    const int MAX_NUMBER_OF_TRIES = 3;

    public int numberOfPlayers;
    public int numberOfTries;
    public string sentence;
    public GameObject squareTemplate;
    public Canvas textCanvas;

    void Start()
    {
        createAreas();
        createTokens();
    }

    void createAreas()
    {
        createArea(1, new Vector3(-7, 0, 0), new Vector3(3, 7, 1));
        createArea(2, new Vector3(7, 0, 0), new Vector3(3, 7, 1));
        if (numberOfPlayers == MAX_NUMBER_OF_PLAYERS)
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
        string[] tokens = sentence.Split(' ');
        if(numberOfTries > 1){
            string[] finalTokens = new string[tokens.Length / numberOfTries];
            for (int i = 0; i < tokens.Length; i++)
            {
                finalTokens[i / numberOfTries] += tokens[i] + " ";
            }
            tokens = finalTokens;
        }

        Canvas canvas = Instantiate(textCanvas);
        canvas.name = "Token Canvas";
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }
        canvas.gameObject.SetActive(true);
        
        foreach (string token in tokens)
        {
            createToken(token, canvas);
        }

        //TODO: animation to dispatch these pieces around the board
        //TODO: add rotation option to the tokens for the users to use
        
        createResultPlaceholders(tokens, canvas);        
        createSpots(GameObject.Find("Area 1"), tokens);
        createSpots(GameObject.Find("Area 2"), tokens);
        if (numberOfPlayers == MAX_NUMBER_OF_PLAYERS)
        {
            createSpots(GameObject.Find("Area 3"), tokens);
        }
        
        // distribute the tokens so that one has one valid spot and one valid area
    }

    TextMeshProUGUI createToken(string token, Canvas mainCanvas)
    {
        TextMeshProUGUI tokenText = Instantiate(mainCanvas.GetComponentInChildren<TextMeshProUGUI>());
        tokenText.GetComponent<TextMeshProUGUI>().enabled = true;
        tokenText.text = token;
        tokenText.tag = "Token";
        tokenText.transform.SetParent(mainCanvas.transform);
        tokenText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(1, 3, 0));
        //TODO: more customization
        return tokenText;
    }

    void createResultPlaceholders(string[] tokens, Canvas mainCanvas)
    {
        string result = "";
        foreach (string token in tokens)
        {
            result += "[] ";
        }
        TextMeshProUGUI resultText = createOneResultPlaceholder(1, mainCanvas, new Vector3(-4.25f, 2.5f, 0), new Vector3(0, 0, -90));
        resultText.text = result;
        resultText = createOneResultPlaceholder(2, mainCanvas, new Vector3(4.25f, 2.5f, 0), new Vector3(0, 0, 90));
        resultText.text = result;
        if (numberOfPlayers == MAX_NUMBER_OF_PLAYERS)
        {
            resultText = createOneResultPlaceholder(3, mainCanvas, new Vector3(0, 1, 0), new Vector3(0, 0, 0));
            resultText.text = result;
        }
    }

    TextMeshProUGUI createOneResultPlaceholder(int numArea, Canvas resultText, Vector3 position, Vector3 rotation)
    {
        TextMeshProUGUI result = Instantiate(resultText.GetComponentInChildren<TextMeshProUGUI>());
        result.GetComponent<TextMeshProUGUI>().enabled = true;
        result.transform.SetParent(resultText.transform);
        result.transform.position = Camera.main.WorldToScreenPoint(position);
        result.transform.Rotate(rotation);
        result.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        result.alignment = TextAlignmentOptions.Center;
        //TODO: more customization
        return result;
    }

    void createSpot(GameObject area, Vector3 position, Vector3 scale, int index)
    {
        GameObject spot = Instantiate(squareTemplate);
        spot.name = "" + index;
        spot.tag = "Spot";
        spot.SetActive(true);
        spot.transform.position = position;
        spot.transform.localScale = scale;
        spot.transform.SetParent(area.transform);
        spot.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }
    
    /*
        Cut the area in many spots, depending on the number of tokens
    */
    void createSpots(GameObject area, string[] tokens)
    {
        int numberOfTokens = tokens.Length;
        float areaWidth = area.transform.localScale.x;
        float areaHeight = area.transform.localScale.y;
        float spotWidth = areaWidth / numberOfTokens;
        float spotHeight = areaHeight / numberOfTokens;
        float x = area.transform.position.x - areaWidth / 2 + spotWidth / 2;
        float y = area.transform.position.y - areaHeight / 2 + spotHeight / 2;
        for (int i = 0; i < numberOfTokens; i++)
        {
            createSpot(area, new Vector3(x, y, 0), new Vector3(spotWidth, spotHeight, 1), i);
            /*x += spotWidth;
            y += spotHeight;*/
        }
    }
}
