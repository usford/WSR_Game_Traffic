using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    public GameObject road; //Дорога
    public GameObject canister; //Канистра
    public GameObject CarAI1; //Голубая машина AI
    private GameObject car; //Ненужное говно
    public static string model; //Название машины
    public static Sprite sprite; //Спрайт машины
    public static int maxSpeed; //Максимальная скорость
    private int glabeSpeed = 30; //Скорость по траве
    private int wasSpeed = maxSpeed; //Сохранение максимальной скорости
    private float speed = 0; //Скорость машины
    private float fuel = 100; //Топливо
    private float roadY = 5.9f; //Создание дороги
    private float distance = 0; //Пройденное расстояние 
    private float time = 0; //Время на поле
    public GameObject dirt; //Префаб грязи

    private bool isGlabe = false; //Является ли поверхность полем

    private int countCanister = 0; //Счёт до следующей канистры
    private int countRoadCar = 0; //Счёт до следующей машины на нашей полосе 
    private int oncomingCar = 0; //Счёт до следующей машины на встречной полосе
    private int countDirt = 0; //Счёт до следующей грязи
    private GameObject arrow; //Стрелка спидометра
    private Vector3 arrowRotation; //Поворот стрелки

    private GameObject textFuel; //Текущие кол-во бензина
    private GameObject textDistance; //Пройденная дистанция

    public Transform selfTransform; //Наша тачка
    private Vector3 _force; //Ускорение для езды

    void Start()
    {
        textFuel = GameObject.Find("Text_Fuel");
        textFuel.GetComponent<Text>().text = fuel.ToString();
        textDistance = GameObject.Find("Text_Distance");
        textDistance.GetComponent<Text>().text = distance + " м.";
        arrow = GameObject.Find("Arrow");

        arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 144.967f);
    }

    void Update()
    {
        //-28,387 120
        //144,967 0
        //135,196 5
        //128,905 10 

        selfTransform.position += _force;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {

            if (speed < maxSpeed)
            {
                speed += 0.5f;
                _force += (selfTransform.up * Time.deltaTime) * (speed / 800);
            }
            else if (speed >= maxSpeed)
            {
                speed = maxSpeed;
            }
            
            if (speed < 40 && speed > 0)
            {
                fuel -= 0.01f;
            }else if (speed < 80 && speed >= 40)
            {
                fuel -= 0.05f;
            }else if (speed < 120 && speed >= 80)
            {
                fuel -= 0.1f;
            }else if (speed < 200 && speed >= 120)
            {
                fuel -= 0.2f;
            }
            textFuel.GetComponent<Text>().text = Mathf.RoundToInt(fuel).ToString();
            textDistance.GetComponent<Text>().text = Mathf.RoundToInt(gameObject.transform.position.y) + " м.";
            arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 144.967f - (speed * 1.444f));
            //Debug.Log(speed.ToString());
            //Debug.Log(gameObject.transform.position.y);
        }
        else
        {
            _force = Vector3.Lerp(_force, Vector3.zero, 0.03f);
            if (speed >= 2 )
            {
                speed -= 1f;
                fuel -= 0.03f;
                textFuel.GetComponent<Text>().text = Mathf.RoundToInt(fuel).ToString();
                textDistance.GetComponent<Text>().text = Mathf.RoundToInt(gameObject.transform.position.y) + " м.";
                //Debug.Log(speed.ToString());    
            }
            else if (speed <= 2)
            {
                speed = 0;
                //Debug.Log(speed.ToString());
            }

            arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 144.967f - (speed * 1.444f));
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (speed > 60)
            {
                selfTransform.position = new Vector3(selfTransform.position.x - 0.07f, selfTransform.position.y, selfTransform.transform.position.z);
            }

            if (speed > 5 && speed <= 60)
            {
                selfTransform.position = new Vector3(selfTransform.position.x - 0.04f, selfTransform.position.y, selfTransform.transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (speed > 60)
            {
                selfTransform.position = new Vector3(selfTransform.position.x + 0.07f, selfTransform.position.y, selfTransform.transform.position.z);
            }

            if (speed > 5 && speed <= 60)
            {
                selfTransform.position = new Vector3(selfTransform.position.x + 0.04f, selfTransform.position.y, selfTransform.transform.position.z);
            }
        }

        //Проигрыш из-за топлива
        if (fuel <= 0)
        {
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }

        //Проигрыш на полде
        if (isGlabe == true)
        {
            time += Time.deltaTime;
            //Debug.Log(time);

            if (time >= 3)
            {
                maxSpeed = wasSpeed;
                SceneManager.LoadScene("Start", LoadSceneMode.Single);
            }

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Столкновение с машиной
        if (collision.CompareTag("CarAI"))
        {
            SceneManager.LoadScene("Start", LoadSceneMode.Single);
        }

        //Машина на поле
        if (collision.CompareTag("Glabe"))
        {
            maxSpeed = glabeSpeed;
            isGlabe = true;
            if (speed > 40)
            {
                _force = Vector3.Lerp(_force, Vector3.zero, 0.5f);
            }
       
        }

        //Машина возвращается на дорогу
        if (collision.CompareTag("Road"))
        {
            time = 0;
            maxSpeed = wasSpeed;
            isGlabe = false;
            //Debug.Log("Вышел с поля");
        }

        //Динамическое создание дороги
        if (collision.CompareTag("Strip"))
        {
            road.transform.position = new Vector3(-0.04922909f, 4.68f + roadY, 0);
            Instantiate(road);           
            roadY += 5.9f;
            countCanister++;
            countRoadCar++;
            oncomingCar++;
            countDirt++;

            if (countDirt == 10)
            {

                int randomBand = Random.Range(0, 4);
                float randomX = 0;
                switch (randomBand)
                {
                    case 0:
                        {
                            randomX = 1.262f;
                            break;
                        }

                    case 1:
                        {
                            randomX = 0.468f;
                            break;
                        }

                    case 2:
                        {
                            randomX = -0.472f;
                            break;
                        }

                    case 3:
                        {
                            randomX = -1.308f;
                            break;
                        }
                }

                dirt.transform.position = new Vector3(randomX, 4.68f + roadY, 0);
                Instantiate(dirt);
                countDirt = 0;
            }

            if (countCanister == 5)
            {
                float randomX = Random.Range(-1.5f, 1.41f);
                canister.transform.position = new Vector3(randomX, 4.68f + roadY, 0);
                Instantiate(canister);              
                countCanister = 0;
            }

            if (countRoadCar == 6)
            {
                int randomBand = Random.Range(0, 2);
                float randomX = 0;
                switch (randomBand)
                {
                    case 0:
                        {
                            randomX = 1.227f;
                            break;
                        }

                    case 1:
                        {
                            randomX = 0.432f;
                            break;
                        }
                }

                //int maxSpeedAI = 0;
                //int random = Random.Range(0, 3);
                //switch (random)
                //{
                //    case 0:
                //        maxSpeedAI = 60;
                //        break;

                //    case 1:
                //        maxSpeedAI = 80;
                //        break;

                //    case 2:
                //        maxSpeedAI = 100;
                //        break;
                //}

                //CarAI1.GetComponent<MoveAI>().maxSpeedAI = maxSpeedAI;

                CarAI1.GetComponent<MoveAI>().maxSpeedAI = 50;
                CarAI1.GetComponent<SpriteRenderer>().flipY = false;
                CarAI1.GetComponent<MoveAI>().oncomingTraffic = false;
                CarAI1.transform.position = new Vector3(randomX, 4.68f + roadY, 0);
                Instantiate(CarAI1);
                countRoadCar = 0;
            }

            if (oncomingCar == 20)
            {
                int randomBand = Random.Range(0, 2);
                float randomX = 0;
                switch (randomBand)
                {
                    case 0:
                        {
                            randomX = -0.5f;
                            break;
                        }

                    case 1:
                        {
                            randomX = -1.32f;
                            break;
                        }
                }

                //int maxSpeedAI = 0;
                //int random = Random.Range(0, 3);
                //switch (random)
                //{
                //    case 0:
                //        maxSpeedAI = 60;
                //        break;

                //    case 1:
                //        maxSpeedAI = 80;
                //        break;

                //    case 2:
                //        maxSpeedAI = 100;
                //        break;
                //}

                //CarAI1.GetComponent<MoveAI>().maxSpeedAI = maxSpeedAI;

                CarAI1.GetComponent<MoveAI>().maxSpeedAI = 40;
                CarAI1.GetComponent<SpriteRenderer>().flipY = true;
                CarAI1.GetComponent<MoveAI>().oncomingTraffic = true;
                CarAI1.transform.position = new Vector3(randomX, 4.68f + roadY, 0);
                Instantiate(CarAI1);
                oncomingCar = 0;
            }
        }


        //Вляпался в грязь
        if (collision.CompareTag("Dirt"))
        {
            _force = Vector3.Lerp(_force, Vector3.zero, 0.5f);
            Debug.Log("Вляпались в грязь");
        }

        //Поднятие канистры
        if (collision.CompareTag("Canister"))
        {
            if (fuel + 35f >= 100)
            {
                fuel = 100;
            }
            else
            {
                fuel += 35f;
            }

            Destroy(collision.gameObject);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
    }

    void OnTriggerExit2D(Collider2D collision)
    {
    }
}
