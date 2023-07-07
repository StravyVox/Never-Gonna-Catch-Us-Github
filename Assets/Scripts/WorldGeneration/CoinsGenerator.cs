using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    [SerializeField] GameObject coin;
    private CoinPool pool;
    public void GenerateOnObject(Vector3 pos, Transform parent)
    {
        pool = CoinPool.instance;
        pos = new Vector3(pos.x, 5f, pos.z - 3);
        if (Random.Range(1,5)>2)
        {
            //Debug.Log("GeneratingOnObject");
            Vector3 adder;
            
            adder = new Vector3(0, 0, 1.5f);
            for (int i = 0; i < 5; i++)
            {

                GameObject objCoin = pool.GetObject();
                objCoin.transform.position = pos;
                objCoin.transform.parent = parent;
                pos += adder;
            }
        }
    }
    public void GenerateLine(Vector3 pos, Transform parent)
    {
     
        pool = CoinPool.instance; 
           // Debug.Log("GeneratingLine");
            int typeOfLine = Random.Range(1, 3);

            pos = new Vector3(pos.x,2f,pos.z+3);
            Vector3 adder;
            switch (typeOfLine)
            {
                case 1:
                    adder = new Vector3(0, 0, 3);
                    for(int i = 0; i < 2; i++)
                    {

                        GameObject objCoin = pool.GetObject();
                         GameObject objCoin1 = pool.GetObject();
                        GameObject objCoin2 = pool.GetObject();
                        objCoin.transform.position = pos;
                        objCoin1.transform.position = new Vector3(pos.x, pos.y, pos.z + 1);
                        objCoin2.transform.position = new Vector3(pos.x, pos.y, pos.z + 2);
                        objCoin.transform.parent = parent;
                        objCoin1.transform.parent = parent;
                        objCoin2.transform.parent = parent;
    
                    pos += adder;
                    }

                    break;
                case 2:
                    Vector3 adderZ = new Vector3(0, 0, 0.5f);
                    Vector3 adderY = new Vector3(0, 0.5f, 0);
                    pos.y = 1.5f;
                   
                    for (int i = 1; i <= 2; i++)
                    {
                        GameObject objCoin = pool.GetObject();
                        GameObject objCoin1 = pool.GetObject();
                        objCoin.transform.position = pos+(adderY*i)+(adderZ*i);
                        objCoin1.transform.position = pos + (adderY * i) - (adderZ * i);
                        objCoin.transform.parent = parent;
                        objCoin1.transform.parent = parent;
                    }
                    pos.y = 4.5f;
                    for (int i = 2; i >= 1; i--)
                    {
                        GameObject objCoin = pool.GetObject();
                        GameObject objCoin1 = pool.GetObject();
                        objCoin.transform.position = pos - (adderY * i) + (adderZ * i);
                        objCoin1.transform.position = pos - (adderY * i) - (adderZ * i);
                        objCoin.transform.parent = parent;
                        objCoin1.transform.parent = parent;
                }
                break;
                case 3:
                    adder = new Vector3(0, 0, 1 );
                    pos.y += 5;
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject objCoin = pool.GetObject();
                        
                        objCoin.transform.position = pos;
                        objCoin.transform.parent = parent;
                    pos += adder;
                    }
                    break;
            }
    }
}
