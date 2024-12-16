using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pistas : MonoBehaviour // Pistas = Misiones
{
    public TextMeshProUGUI[] pistasTextos; // Array para los textos de las misiones
    public Image[] faceImages; // Array de las imagenes de los tripulantes
    public TextMeshProUGUI mensajeSinPistas; // Mensaje de "No hay pistas activas"
    private bool[] estadoMisiones; // Almacena el estado de cada misi�n (activa o completada)
    //private Color colorGrisClaro = new Color(0.7f, 0.7f, 0.7f); // Definir el color gris claro
    //private DataManager dataManager;
    private List<int> listRecruitedCrewMembers = new List<int>();

    void Start()
    {
        // if (dataManager == null)
        // {
        //     dataManager = FindObjectOfType<DataManager>();
        // }
        // if(dataManager != null) {
        //     // Guardo los tripulantes reclutados para comparar ids y activar/completar pistas correspondientes
        //     listRecruitedCrewMembers = dataManager.playerData.collectedCrewMembers; 
        // }
        

        // Inicializa el estado de cada misi�n como no activa
        estadoMisiones = new bool[pistasTextos.Length];
        // Desactiva todas las pistas al inicio
        foreach (var pista in pistasTextos)
        {
            pista.gameObject.SetActive(false);
        }
        foreach (var imagen in faceImages)
        {
            imagen.gameObject.SetActive(false);
        }
        ActualizarMensajeSinPistas(); // Llama a la funcion

        // Activa las pistas correspondientes a los miembros de la tripulación reclutados
        for (int i = 0; i < pistasTextos.Length; i++)
        {
            if (listRecruitedCrewMembers.Contains(i)) // Verifica si el índice está en la lista
            {
                ActivarPista(i);
                CompletarPista(i);
            }
        }
    }

        // M�todo para activar una pista espec�fica
        public void ActivarPista(int index)
    {
        if (index >= 0 && index < pistasTextos.Length && !estadoMisiones[index])
        {
            pistasTextos[index].gameObject.SetActive(true);
            faceImages[index].gameObject.SetActive(true);
            estadoMisiones[index] = true;
            ActualizarMensajeSinPistas();
        }
    }

    // M�todo para tachar una pista cuando la misi�n se complete
    public void CompletarPista(int index)
    {
        if (index >= 0 && index < pistasTextos.Length && estadoMisiones[index])
        {
            pistasTextos[index].fontStyle = FontStyles.Strikethrough; // Tacha el texto
            //pistasTextos[index].color = colorGrisClaro; // Cambia el color a gris claro
            estadoMisiones[index] = false; // Marcar la misi�n como completada
        }
    }
    // Funcion para mostrar o no un mensaje de que no hay pistas activas.
    private void ActualizarMensajeSinPistas()
    {
        // Verifica si hay alguna pista activa
        bool hayPistasActivas = false;
        foreach (var estado in estadoMisiones)
        {
            if (estado) hayPistasActivas = true;
        }

        // Muestra o oculta el mensaje seg�n haya o no pistas activas
        mensajeSinPistas.gameObject.SetActive(!hayPistasActivas);
    }
}
