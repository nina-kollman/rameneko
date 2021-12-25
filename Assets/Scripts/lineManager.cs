using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineManager : MonoBehaviour
{
    [SerializeField] private line[] verticalLines;
    [SerializeField] private line[] horizontalLines;


    private int clickCounter = 0;

    public void PlusClick()
    {
        clickCounter++;
    }

    public void Restart(int levelNum)
    {
        SetLevel(levelNum);
    }
    public void SetLevel(int levelNum)
    {
        switch (levelNum)
        {
            case 1:
                //level 1 - only h2 is active
                foreach (var vLine in verticalLines)
                {
                    vLine.SetActive(false);
                    vLine.SetBasePosition();
                }

                foreach (var hLine in horizontalLines)
                {
                    if (hLine.index / 10 != 2) //not h2
                        hLine.SetActive(false);
                    else // is h2
                        hLine.SetActive(true);
                    
                    hLine.SetBasePosition();
                    
                }
                break;
        }
    }

     public void SetScreen(bool vertical, int index)
     {
         int num = index / 10;
         if (vertical)
         {
             switch (num)
             {
                 case 1:
                     for (int i = 0; i < 3; i++)
                     {
                         verticalLines[i].SetActive(false);
                     }
                     horizontalLines[0].SetActive(false);
                     horizontalLines[3].SetActive(false);
                     horizontalLines[6].SetActive(false);
                     horizontalLines[9].SetActive(false);
                     break;
                 case 2:
                     for (int i = 9; i < 12; i++)
                     {
                         verticalLines[i].SetActive(false);
                     }
                     horizontalLines[2].SetActive(false);
                     horizontalLines[5].SetActive(false);
                     horizontalLines[8].SetActive(false);
                     horizontalLines[11].SetActive(false);
                     break;
             }
         }
         else // is horizontal
         {
             switch (num)
             {
                 case 1:
                     for (int i = 0; i < 3; i++)
                     {
                         horizontalLines[i].SetActive(false);
                     }
                     verticalLines[0].SetActive(false);
                     verticalLines[3].SetActive(false);
                     verticalLines[6].SetActive(false);
                     verticalLines[9].SetActive(false);
                     break;
                 case 2:
                     for (int i = 9; i < 12; i++)
                     {
                         horizontalLines[i].SetActive(false);
                     }
                     verticalLines[2].SetActive(false);
                     verticalLines[5].SetActive(false);
                     verticalLines[8].SetActive(false);
                     verticalLines[11].SetActive(false);
                     break;
             }
         }

     }
    
  
}
