using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private int _score = 0;
    private Rigidbody rb;
    private GameObject _canvas;
    private TextMeshProUGUI _scoreText;
    private GameObject _pickupPrefab;
    private TextMeshProUGUI _gameOverText;
    private bool _gameOver = false;

    private const int MAX_PICKUPS = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _canvas = GameObject.Find("Canvas");
        _scoreText = _canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        UpdateScoreText(_score);
        _gameOverText = _canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        _pickupPrefab = Resources.Load("Prefabs/Pickup", typeof(GameObject)) as GameObject;
        SpawnPickup();
    }

    void FixedUpdate()
    {
        if (_gameOver)
        {
            return;
        }

        float oX = Input.GetAxis("Horizontal");
        float oZ = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(oX, 0, oZ));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            Eat(other.gameObject);
        }
        // Debug.Log("Collided with " + other.gameObject.name);
    }

    private void UpdateScoreText(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    private Vector3 GetRandomPosition()
    {
        var randomPosition = new Vector3(Random.Range(-4, 4), 0.5f, Random.Range(-4, 4));
        var distance = Vector3.Distance(transform.position, randomPosition);
        if (distance < 3)
        {
            return GetRandomPosition();
        }

        return randomPosition;
    }

    private void SpawnPickup()
    {
        Instantiate(_pickupPrefab, GetRandomPosition(), Quaternion.identity);
    }

    private void Eat(GameObject pickup)
    {
        _score++;
        UpdateScoreText(_score);
        if (_score == MAX_PICKUPS)
        {
            GameOver(pickup);
        }
        else
        {
            pickup.transform.position = GetRandomPosition();
        }
    }

    private void GameOver(GameObject pickup)
    {
        _scoreText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(true);
        _gameOverText.text = "You Win!";
        _gameOver = true;
        pickup.SetActive(false);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }


    // Update is called once per frame
    void Update()
    {
    }
}