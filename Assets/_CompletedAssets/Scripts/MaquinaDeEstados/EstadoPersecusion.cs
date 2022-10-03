using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPersecusion : MonoBehaviour
{
    public Color ColorEstado = Color.red;

    private MaquinaDeEstados maquinaDeEstados;
    private ControladorNavMesh controladorNavMesh;
    private ControladorVision controladorVision;

    void Start()
    {
        
    }
    
    void Awake(){
        maquinaDeEstados = GetComponent<MaquinaDeEstados>();
        controladorNavMesh = GetComponent<ControladorNavMesh>();
        controladorVision = GetComponent<ControladorVision>();
    }

    void OnEnable(){
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (!controladorVision.PuedeVerAlJugador(out hit, true)){
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
            return;

        }
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();

    }
}
