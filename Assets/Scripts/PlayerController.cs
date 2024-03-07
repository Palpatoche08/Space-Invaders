using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 25f;
    private Rigidbody rb;
    private Collider col;

    public AudioSource fireSound;


    public TextMeshProUGUI scoreCountText;
    private int scoreCount = 0;
    public TextMeshProUGUI highScoreText;
    private int highScore = 0;
    private string highScoreFilePath;
    public TextMeshProUGUI livesCountText;
    private int livesCount = 3;

    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;

    private bool canShoot = true;
    public float shootingCooldown = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        highScoreFilePath = Path.Combine(Application.persistentDataPath, "highscore.txt");
        LoadHighScore();
    }

    void Update()
    {
        scoreCountText.text = "Score : " + scoreCount;
        highScoreText.text = "Highscore : " + highScore;
        if (scoreCount > highScore)
        {
            highScore = scoreCount;
            SaveHighScore();
        }
        scoreCountText.text = "Score : " + scoreCount.ToString("D4");
        highScoreText.text = "HighScore : " + highScore.ToString("D4");
        livesCountText.text = "Lives: " + livesCount;
        Move();

        if (canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShootWithCooldown());
        }

        if (scoreCount == 11000)
        {
            Debug.Log("You win !");
        }
    }

    public void IncreaseScore(int amount)
    {
        scoreCount += amount;
        scoreCountText.text = "Score : " + scoreCount;
    }

    IEnumerator ShootWithCooldown()
    {
        canShoot = false;
        Fire();
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;
    }

    void Fire()
    {
        GameObject temporaryBulletHandler;
        temporaryBulletHandler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
        temporaryBulletHandler.transform.rotation = Quaternion.Euler(Vector3.up * 90);

        Rigidbody temporaryRigidBody;
        temporaryRigidBody = temporaryBulletHandler.GetComponent<Rigidbody>();
        temporaryRigidBody.AddForce(Vector3.up * (Bullet_Forward_Force * 0.5f), ForceMode.Impulse);
        fireSound.Play();
        Destroy(temporaryBulletHandler, 5f);
    }

    void Move()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        Vector3 movement = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0f);
        rb.velocity = movement;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EvilBullet"))
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        livesCount--;
        if (livesCount <= 0)
        {
            Debug.Log("You Lost!");
            Time.timeScale = 0f;
        }
    }

    public int GetScore()
    {
        return scoreCount;
    }

    void LoadHighScore()
    {
        if (File.Exists(highScoreFilePath))
        {
            string content = File.ReadAllText(highScoreFilePath);
            if (int.TryParse(content, out int loadedHighScore))
            {
                highScore = loadedHighScore;
            }
        }
    }

    void SaveHighScore()
    {
        File.WriteAllText(highScoreFilePath, highScore.ToString());
    }
}
