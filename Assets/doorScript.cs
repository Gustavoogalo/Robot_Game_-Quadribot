using UnityEngine;

public class doorScript : MonoBehaviour
{
    public Animator doorAnimator; // Referência ao Animator da porta
    public Transform chaveposition;
    void Start()
    {
        // Verifica se o Animator está atribuído
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
            if (doorAnimator == null)
            {
                Debug.LogWarning("Animator não encontrado no objeto da porta. Verifique se o Animator está atribuído no Inspector ou se está no mesmo GameObject que o script.");
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
                Debug.LogWarning("Animator não está atribuído corretamente. Verifique a configuração do Animator no objeto da porta.");
            }
        }
    }
}
