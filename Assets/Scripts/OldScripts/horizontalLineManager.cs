using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalLineManager : MonoBehaviour
{
   [SerializeField] private verticalLineManager vMng;
   
   public void SetX(float num)
   {
      foreach (Transform line in transform)
      {
         var posX = line.position.x + num;
         line.position = new Vector3(posX, line.position.y, line.position.z);
      }
   }
}
