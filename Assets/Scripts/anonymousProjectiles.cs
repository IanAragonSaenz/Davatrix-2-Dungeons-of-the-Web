using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anonymousProjectiles : MonoBehaviour
{

    public float AttackAradius;
    public bool AttackACanMove;
    public char attackType;
    public float[] attackAPositionsX;
    public float[] attackAPositionsY;
    Vector3 originalPosition;
    int attackAI;

    // Start is called before the first frame update
    void Start()
    {
       
            attackAPositionsX = new float[360];
            attackAPositionsY = new float[360];
            attackAPositionsX[0] = 1;
            attackAPositionsY[0] = 0;

            for(int i = 1; i <= 89; i ++){
                attackAPositionsX[i] =  (float) attackAPositionsX[ i - 1 ] - 0.011111111f ;
                attackAPositionsY[i] = (float) attackAPositionsY[ i - 1 ] +  0.011111111f;
            }


            for(int i = 90; i <= 179; i ++){
                attackAPositionsX[i] =  (float)attackAPositionsX[ i - 1 ] - 0.011111111f;
                attackAPositionsY[i] =  (float)attackAPositionsY[ i - 1 ] - 0.011111111f;
            }   



            for(int i = 180; i <= 269; i ++){
                attackAPositionsX[i] =  (float)attackAPositionsX[ i - 1 ] +  0.011111111f;
                attackAPositionsY[i] =  (float)attackAPositionsY[ i - 1 ] -  0.011111111f;
            }


            
            for(int i = 270; i <= 359; i ++){
                attackAPositionsX[i] =  (float)attackAPositionsX[ i - 1 ] + 0.011111111f;
                attackAPositionsY[i] =  (float)attackAPositionsY[ i - 1 ] + 0.011111111f;
            }
            

            originalPosition = transform.position;
            AttackACanMove = false;
            attackAI = 0;

        
    }

    void fillArrays(int beggin, int end, float amount){
      
    }


    // Update is called once per frame
    void Update()
    {
        if(attackType == 'A'){
            if(AttackACanMove){
          
              StartCoroutine(AttackA());
              AttackACanMove = false;
            
            }
        }
    }

    IEnumerator AttackA(){
        StartCoroutine(AttackADestroy());
        while(true){
            
            Vector3 direction =  new Vector3(( attackAPositionsX[ attackAI ] * AttackAradius * 1.5f) ,
                                             (attackAPositionsY[ attackAI ] * AttackAradius * 1.5f ),
                                             0);
            transform.position =  direction + originalPosition;
            attackAI ++;

            if( attackAI == 360){

                attackAI = 0;

            }
            yield return new WaitForSeconds(0.01f);
        }
    }


    IEnumerator AttackADestroy(){
        yield return new WaitForSeconds(40f);
        Destroy(this.gameObject);
    }

}
