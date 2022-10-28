using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject _player;
    public float playerSpeed = 5f;
    private Vector3 destination;
    private TextMeshProUGUI _timerText;
    private float _timer = 0;

    void Start()
    {
        _player = GameObject.FindWithTag(TagConstants.PLAYER_TAG);
        _timerText = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        destination = _player.transform.position;

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var target = hit.transform.GameObject();

                if (target.CompareTag(TagConstants.PLANE_TAG))
                {
                    destination = hit.point + new Vector3(0, 0.5f, 0);
                }
            }
        }

        if (destination != _player.transform.position)
        {
            var step = playerSpeed * Time.deltaTime; // calculate distance to move
            _player.transform.position = Vector3.MoveTowards(_player.transform.position,
                destination,
                step
            );
        }
    }
    
    
    IEnumerator Timer()
    {
        while (true)
        {
            _timer += 0.1f;
            _timerText.text = _timer.ToString("F1");
            yield return new WaitForSeconds(0.1f);
        }
    }
}