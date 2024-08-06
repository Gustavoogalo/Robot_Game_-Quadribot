using UnityEngine;

public class doorScript : MonoBehaviour
{
    public Animator doorAnimator; // Refer�ncia ao Animator da porta
    public Transform chaveposition;
    void Start()
    {
        // Verifica se o Animator est� atribu�do
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
            if (doorAnimator == null)
            {
                Debug.LogWarning("Animator n�o encontrado no objeto da porta. Verifique se o Animator est� atribu�do no Inspector ou se est� no mesmo GameObject que o script.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu tem a tag "itemDoor"
        if (other.CompareTag("itemDoor"))
        {
                

            // Verifica se o Animator foi encontrado
            if (doorAnimator != null)
            {
                Collectable_Item coletable = GetComponent<Collectable_Item>();
                other.transform.position = chaveposition.position;
                doorAnimator.SetTrigger("opendoor");
                // Debug.Log("Objeto com tag 'itemDoor' entrou, abrindo a porta.");
                
            }
            else
            {
                Debug.LogWarning("Animator n�o est� atribu�do corretamente. Verifique a configura��o do Animator no objeto da porta.");
            }
        }
    }
}
