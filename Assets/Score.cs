using UnityEngine;
using System.Collections;
using System.Linq;

public class Score : MonoBehaviour
{

    public int playerNumber;
    public int score;
    public UnityEngine.UI.Text text;

    void BulletCollision(MoveForwardReflectOnWalls bullet)
    {
        if (bullet.playerNumber == playerNumber) {
            score--;
            text.text = "Score: " + score;

        } else {
            var sco = FindObjectsOfType<Score>().First(s => s.playerNumber == bullet.playerNumber);
            sco.score += 1;
            sco.text.text = "Score: " + sco.score;

        }

        Instantiate(bullet.explosionPrefab, bullet.transform.position, Quaternion.identity);

        Destroy(bullet.gameObject);
    }



}
