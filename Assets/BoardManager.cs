using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int SELECTION_X = -1;
    private int SELECTION_Y = -1;

    public int n = 5;
    public int numImmunesInitial = 1;
    public int numPseudosInitial = 2;
    public int numInfectedInitial = 1;
    public GameObject cubePrefab;

    public float percentImunidade = 20.0f;

    private int turn = 0;
    private int numImmunesCurrent = 0; 
    private int numPseudosCurrent = 0;
    private int numInfectedCurrent = 0;
    private int numHealthyCurrent = 0;

    //1= sadio, 2=infectado, 3=imune, 4=pseudo
    private int[,] charactersMatrix;
    private List<Vector2> infectedsPosition = new List<Vector2>();

    private GameObject[,] cubeMatrix;

    void CreateText()
    {
        string new_Content = "\n" + numImmunesCurrent + "    " + numPseudosCurrent + "   " + numInfectedCurrent + "  " + numHealthyCurrent;
        string path = Application.dataPath + "/Log.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Log do Experimento \n \n");           
        }

        File.AppendAllText(path, new_Content);



    }

    // Start is called before the first frame update
    void Start()
    {
        charactersMatrix = new int[n, n];
        cubeMatrix = new GameObject[n, n];
        DrawBoard();
        InitializeBoard();
    }

    // Update is called once per frame
    void Update()
    {
        //StartTurn();
    }

    private void DrawBoard()
    {
        float posXAnterior = -1.5f * (n/2);
        for (int i = 0; i < n; i++)
        {
            float posYAnterior = 1.5f * (n/2);
            for (int j = 0; j < n; j++)
            {
               posYAnterior -= 1.5f;  
               GameObject cube = Instantiate(cubePrefab);
               cube.transform.localPosition = new Vector3(posXAnterior, 1, posYAnterior);
               cubeMatrix[i, j] = cube;
            }
            posXAnterior += 1.5f ;
        }
    }

    public void InitializeBoard()
    {
        int posX = Random.Range(0,n);
        int posY = Random.Range(0,n);
        bool hit = true;
        charactersMatrix[posX,posY] = 2;
        infectedsPosition.Add(new Vector2(posX, posY));
        ChangeColor(2, posX, posY);
        numInfectedCurrent++;
        for (int i = 0; i < numPseudosInitial; i++)
        {
            hit = true;
            while (hit)
            {
                posX = Random.Range(0,n);
                posY = Random.Range(0,n);
                if (charactersMatrix[posX,posY] == 0)
                {
                    charactersMatrix[posX,posY] = 4;
                    ChangeColor(4, posX, posY);
                    numPseudosCurrent++;
                    hit = false;
                }
            }
        }
        for (int i = 0; i < numImmunesInitial; i++)
        {
            hit = true;
            while (hit)
            {
                posX = Random.Range(0,n);
                posY = Random.Range(0,n);
                if (charactersMatrix[posX,posY] == 0)
                {
                    charactersMatrix[posX,posY] = 3;
                    ChangeColor(3, posX, posY);
                    numImmunesCurrent++;
                    hit = false;
                }
            }
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (charactersMatrix[i,j] == 0)
                {
                    charactersMatrix[i,j] = 1;
                    ChangeColor(1, i, j);
                    numHealthyCurrent++;
                }
            }
        }
        StartCoroutine(NextTurn());
    }

    void InfectedWalk(int posx, int posy, int infectedIndex)
    {     
        //teste baixo
        if (posy + 1 < n && charactersMatrix[posx, posy + 1] != 2)
            TesteDeInfeccao(posx, posy + 1);
        //teste cima
        if (posy - 1 >= 0 && charactersMatrix[posx, posy - 1] != 2)
            TesteDeInfeccao(posx, posy - 1);
        //teste direita
        if (posx + 1 < n && charactersMatrix[posx + 1, posy] != 2)
            TesteDeInfeccao(posx + 1, posy);
        //teste esquerda
        if (posx - 1 >= 0 && charactersMatrix[posx - 1, posy] != 2)
            TesteDeInfeccao(posx - 1, posy);

        trocar(posx, posy, infectedIndex);
    }
    //1= sadio, 2=infectado, 3=imune, 4=pseudo
    void trocar(int posx, int posy, int infectedIndex)
    {
        int aux = 0;
        int randomNumber = Random.Range(1,4);
        if (randomNumber == 1)
        {
            if (posy + 1 >= n)
            {
                randomNumber =+ 1;
            }
            else
            {
                aux = charactersMatrix[posx, posy];
                charactersMatrix[posx, posy] = charactersMatrix[posx, posy + 1];
                ChangeColor(charactersMatrix[posx, posy + 1], posx, posy);
                charactersMatrix[posx, posy + 1] = aux;
                ChangeColor(aux, posx, posy + 1);
                infectedsPosition[infectedIndex] = new Vector2(posx,posy + 1);

                
                
                
            }
        }
        if (randomNumber == 2)
        {
            if (posy - 1 < 0)
            {
                randomNumber = +1;
            }
            else
            {
                aux = charactersMatrix[posx, posy];
                charactersMatrix[posx, posy] = charactersMatrix[posx, posy - 1];
                ChangeColor(charactersMatrix[posx, posy - 1], posx, posy);
                charactersMatrix[posx, posy - 1] = aux;
                ChangeColor(aux, posx, posy - 1);
                infectedsPosition[infectedIndex] = new Vector2(posx, posy - 1);
                
                
               
            }
        }
        if (randomNumber == 3)
        {
            if (posx + 1 >= n)
            {
                randomNumber = +1;
            }
            else
            {
                aux = charactersMatrix[posx, posy];
                charactersMatrix[posx, posy] = charactersMatrix[posx + 1, posy];
                ChangeColor(charactersMatrix[posx + 1, posy], posx, posy);
                charactersMatrix[posx + 1, posy] = aux;
                ChangeColor(aux, posx + 1, posy);
                infectedsPosition[infectedIndex] = new Vector2(posx + 1, posy);
                
                
               

            }
        }
        if (randomNumber == 4)
        {
            if (posx - 1 < 0)
            {
                randomNumber = -3;
            }
            else
            {
                aux = charactersMatrix[posx, posy];
                charactersMatrix[posx, posy] = charactersMatrix[posx - 1, posy];
                ChangeColor(charactersMatrix[posx - 1, posy], posx, posy);
                charactersMatrix[posx - 1, posy] = aux;
                ChangeColor(aux, posx - 1, posy);
                infectedsPosition[infectedIndex] = new Vector2(posx - 1, posy);

                
                

            }
        }

    }

    void TesteDeInfeccao(int posx, int posy)
    {
        if (charactersMatrix[posx, posy] == 1)
        {
            Infectar(posx, posy);
        }
        else
        {
            float randomNumber = Random.Range(0, 100);
            if (randomNumber > percentImunidade)
            {
                Infectar(posx, posy);
            }
        }
    }

    void Infectar(int posx, int posy)
    {
        if(charactersMatrix[posx, posy] != 2 && charactersMatrix[posx, posy] != 3)
        {
            infectedsPosition.Add(new Vector2(posx, posy));
            ChangeColor(2, posx, posy);
            numInfectedCurrent++;
            if(charactersMatrix[posx, posy] == 4)
            {
                numPseudosCurrent--;
            }
            else
            {
                numHealthyCurrent--;
            }
            charactersMatrix[posx, posy] = 2;
        }
    }

    void AtualizarNumerosRodada()
    {
        //1= sadio, 2=infectado, 3=imune, 4=pseudo
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                switch (charactersMatrix[i,j])
                {
                    case 1:
                        numHealthyCurrent++;
                        break;
                    case 2:
                        numInfectedCurrent++;
                        break;
                    case 3:
                        numImmunesCurrent++;
                        break;
                    case 4:
                        numPseudosCurrent++;
                        break;

                }
            }
        }
    }

    void ZerarRodada()
    {
        numHealthyCurrent = 0;
        numInfectedCurrent = 0;
        numImmunesCurrent = 0;
        numPseudosCurrent = 0;
    }

    void imprimirImunes()
    {
        Debug.Log(numImmunesCurrent);
    }

    void imprimirPseudoImunes()
    {
        Debug.Log(numPseudosCurrent);
    }

    void imprimirInfectantesGerados()
    {
        int value = 0;
        value = numInfectedCurrent - numInfectedInitial;
        Debug.Log(value);
    }

    void imprimirDoentes()
    {
        Debug.Log(numInfectedCurrent);
    }

    void imprimirSadios()
    {
        Debug.Log(numHealthyCurrent);
    }

    void imprimirMedia()
    {

    }

    void imprimirDesvioPadrao()
    {

    }

    void imprimirMediana()
    {
        
    }

    void StartTurn()
    {
        turn = turn + 1;
        Debug.Log(turn);
        int i = 0;
        foreach(Vector2 v in infectedsPosition.ToArray())
        {
            InfectedWalk((int)v.x, (int)v.y, i);
            i++;
        }
        //AtualizarNumerosRodada();
        CreateText();
        //ZerarRodada();
        StartCoroutine(NextTurn());
    }

    void InfectedsDoWalk()
    {

    }

    void ChangeColor(int colorOption, int posX, int posY)
    {
        switch (colorOption)
        {
            case 1:
                cubeMatrix[posX, posY].transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                break;
            case 2:
                cubeMatrix[posX, posY].transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case 3:
                cubeMatrix[posX, posY].transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
                break;
            case 4:
                cubeMatrix[posX, posY].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0.73f, 0.31f, 0.09f);
                break;
        }
    }

    public IEnumerator NextTurn()
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("H:" + numHealthyCurrent + "/I:" + numInfectedCurrent + "/P:" + numPseudosCurrent + "/Im:" + numImmunesCurrent);
        StartTurn();
    }

}
