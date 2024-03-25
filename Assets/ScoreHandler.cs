using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static int score = 0;


    // Update is called once per frame
    void Update()
    {
        // get the text component of the object this script is attached to
        var text = GetComponent<UnityEngine.UI.Text>();

        // set the text to the current score
        text.text = "Score: " + score;
    }
}
