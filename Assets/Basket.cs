using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public Text scoreGUIText;

    // Start is called before the first frame update
    void Start()
    {
        // find a reference to the scoreCounter GameObject
        GameObject scoreGameObject = GameObject.Find("ScoreCounter");
        scoreGUIText = scoreGameObject.GetComponent<Text>();
        scoreGUIText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;                           // 1
        // The Camera's z position sets the how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;                   // 2
        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);    // 3
        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // find out what hit this basket
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
            // parse the text of the ScoreGUIText into an int
            int score = int.Parse(scoreGUIText.text);
            // add points
            score += 100;
            scoreGUIText.text = score.ToString();
            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
            if (score > 2000)
            {
                SceneManager.LoadScene("Level_2");
            }
        }
    }
}
