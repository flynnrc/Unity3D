//using System.Collections;

//using System.Collections.Generic;

//using UnityEngine;



//public class CameraFollow_Test : MonoBehaviour

//{

//    public GameObject Player;

//    public float JoyAngX, JoyAngY;

//    public float InputSensit = 155.0f;

//    public float RotY = 0.0f, RotX = 0.0f;

//    public float LimiteAng = 65.0f;

//    private Quaternion LocalRot;

//    // Start is called before the first frame update

//    void Start()

//    {

//        Player = GameObject.FindGameObjectWithTag("Player");

//    }



//    // Update is called once per frame

//    void LateUpdate()

//    {

//        transform.position = Player.transform.position;

//    }



//    void Update()

//    {

//        transform.position = Player.transform.position;

//        Atualizacao();

//    }



//    void Atualizacao()

//    {

//        if (PlayerMovement.isInDirectMode)

//        {

//            JoyAngX = Input.GetAxis("CameraRot");

//            JoyAngY = Input.GetAxis("CameraRot2");

//        }

//        else

//        {

//            JoyAngX = Input.GetAxis("Mouse X");

//            JoyAngY = Input.GetAxis("Mouse Y");

//        }



//        RotY += JoyAngX * InputSensit * Time.deltaTime;

//        RotX += (-JoyAngY) * InputSensit * Time.deltaTime;



//        RotX = Mathf.Clamp(RotX, -LimiteAng, LimiteAng);

//        LocalRot = Quaternion.Euler(RotX, RotY, 0);

//        transform.rotation = LocalRot;

//    }

//}