using System.Collections.Generic;
using UnityEngine;

public class CameraObstructionHandler : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador
    public LayerMask obstructionMask; // LayerMask para identificar objetos que podem obstruir a visão
    public float transparency = 0.3f; // Transparência desejada

    private List<Renderer> obstructedRenderers = new List<Renderer>(); // Lista de renderizadores atualmente obstruindo a visão
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>(); // Armazena os materiais originais dos objetos

    void Update()
    {
        HandleObstructions();
    }

    private void HandleObstructions()
    {
        // Primeiro, restaura a opacidade dos objetos que não estão mais obstruindo a visão
        foreach (Renderer renderer in obstructedRenderers)
        {
            if (renderer != null && originalMaterials.ContainsKey(renderer))
            {
                // Restaura os materiais originais
                Material[] materials = originalMaterials[renderer];
                for (int i = 0; i < materials.Length; i++)
                {
                    Material material = materials[i];
                    SetMaterialOpaque(material);
                }
            }
        }

        // Limpa as listas para a próxima verificação
        obstructedRenderers.Clear();
        originalMaterials.Clear();

        // Lança um raycast da câmera até o jogador
        Vector3 direction = player.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Vector3.Distance(transform.position, player.position), obstructionMask);

        foreach (RaycastHit hit in hits)
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Armazena os materiais originais caso ainda não tenha sido feito
                if (!originalMaterials.ContainsKey(renderer))
                {
                    originalMaterials[renderer] = renderer.materials;
                }

                // Define os materiais como transparentes
                foreach (Material material in renderer.materials)
                {
                    SetMaterialTransparent(material);
                }

                // Adiciona à lista de renderizadores obstruídos
                obstructedRenderers.Add(renderer);
            }
        }
    }

    private void SetMaterialTransparent(Material material)
    {
        material.SetFloat("_Surface", 1); // 1 = Transparent
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        Color color = material.color;
        color.a = transparency; // Ajuste a transparência desejada
        material.color = color;
    }

    private void SetMaterialOpaque(Material material)
    {
        material.SetFloat("_Surface", 0); // 0 = Opaque
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

        Color color = material.color;
        color.a = 1f; // Restaura a opacidade total
        material.color = color;
    }
}
