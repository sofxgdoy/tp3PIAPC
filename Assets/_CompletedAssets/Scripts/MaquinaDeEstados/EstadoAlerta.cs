using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EstadoAlerta : MonoBehaviour
{
    public float velocidadGiroBusqueda = 120f;
    public float duracionBusqueda = 4f;
    public Color ColorEstado = Color.yellow;
    private MaquinaDeEstados maquinaDeEstados;
    private ControladorNavMesh controladorNavMesh;
    private ControladorVision controladorVision;
    private float tiempoBuscando;
    

    void Awake()
    {
        maquinaDeEstados = GetComponent<MaquinaDeEstados>();
        controladorNavMesh = GetComponent<ControladorNavMesh>();
        controladorVision = GetComponent<ControladorVision>();
    }

    void OnEnable(){
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
        controladorNavMesh.DetenerNavMeshAgent();
        tiempoBuscando = 0f;
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

        transform.Rotate(0f, velocidadGiroBusqueda*Time.deltaTime, 0f);
        tiempoBuscando += Time.deltaTime;
        if (tiempoBuscando >= duracionBusqueda){
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);
            return;
        }
    }
}
