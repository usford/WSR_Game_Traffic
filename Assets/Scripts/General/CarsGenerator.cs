using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsGenerator : MonoBehaviour
{
    public static CarsGenerator _CarsGenerator;

    public List<CarDataBase> CarList = new List<CarDataBase>(); //Список машин

    void Awake()
    {
        _CarsGenerator = this;   
    }

    //Генерация машины
    public CarDataBase CarGen(int win_id)
    {
        CarDataBase car = new CarDataBase();

        car.id = CarList[win_id].id;
        car.model = CarList[win_id].model;
        car.iconPatch = CarList[win_id].iconPatch;
        car.maxSpeed = CarList[win_id].maxSpeed;

        return car;
    }
}
