using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulla : MonoBehaviour
{
    public Transform[] WayPoints;
    public Color ColorEstado = Color.green;

    private MaquinaDeEstados maquinaDeEstados;
    private ControladorNavMesh controladorNavMesh;
    private ControladorVision controladorVision;
    private int siguienteWayPoint;
    void Awake(){
        maquinaDeEstados = GetComponent<MaquinaDeEstados>();
        controladorNavMesh = GetComponent<ControladorNavMesh>();
        controladorVision = GetComponent<ControladorVision>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //ve al jugador?
        if (controladorVision.PuedeVerAlJugador(out hit))
        {
            controladorNavMesh.perseguirObjetivo = hit.transform;
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecusion);
            return;
        }
        
        if (controladorNavMesh.HemosLlegado()){
            siguienteWayPoint = (siguienteWayPoint + 1) % WayPoints.Length;
            ActualizarWayPointDestino();
        }
    }

    void OnEnable(){
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
        //siguienteWayPoint = 0; esto haria q vuelvas a patrullar desde el punto 0, pero es una opcion
        ActualizarWayPointDestino();

    }
    void ActualizarWayPointDestino(){
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[siguienteWayPoint].position);

    }
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);

        }
    }
}
