using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraScript : MonoBehaviour
{
    public Transform player; // Refer�ncia ao Transform do jogador
    public LayerMask wallLayer; // Layer das paredes
    public float transparency = 0.3f; // Valor da transpar�ncia quando obstru�do
    public float rayDistance = 50f; // Dist�ncia m�xima do raycast

    private Material lastHitMaterial; // Material do �ltimo objeto atingido
    private Color originalColor; // Cor original do �ltimo objeto atingido
    private bool isTransparent = false; // Se o material est� transparente ou n�o

    void Update()
    {
        // Cria um raycast a partir da posi��o da c�mera at� a posi��o do jogador
        Ray ray = new Ray(transform.position, player.position - transform.position);
        RaycastHit hit;

        // Se o raycast atingir algo na layer das paredes e a dist�ncia for menor ou igual � dist�ncia m�xima
        if (Physics.Raycast(ray, out hit, player.position.y, wallLayer))
        {
            // Obt�m o material do objeto atingido e altera sua opacidade
            Material hitMaterial = hit.collider.GetComponent<Renderer>().material;
            if (hitMaterial != null)
            {
                if (lastHitMaterial != hitMaterial)
                {
                    // Restaura a opacidade do material anterior
                    if (lastHitMaterial != null)
                    {
                        RestoreMaterialTransparency(lastHitMaterial);
                    }

                    // Armazena a cor original do novo material
                    originalColor = hitMaterial.color;
                    SetMaterialTransparency(hitMaterial, transparency); // Altera a transpar�ncia
                    lastHitMaterial = hitMaterial; // Armazena o material atual
                    isTransparent = true;
                }
            }
        }
        else
        {
            // Se o raycast n�o atinge nada ou est� fora da dist�ncia m�xima, restaura a opacidade do �ltimo objeto atingido
            if (isTransparent && lastHitMaterial != null)
            {
                RestoreMaterialTransparency(lastHitMaterial); // Restaura a transpar�ncia do material
                lastHitMaterial = null; // Limpa o material armazenado
                isTransparent = false;
            }
        }
    }

    private void SetMaterialTransparency(Material material, float alpha)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;

        // Se o material n�o estiver configurado para renderizar transpar�ncias, ajusta as propriedades de renderiza��o
        if (alpha < 1.0f)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
    }

    private void RestoreMaterialTransparency(Material material)
    {
        // Restaura a cor original do material
        Color color = originalColor;
        color.a = 1.0f; // Opacidade completa
        material.color = color;

        // Restaura as configura��es de renderiza��o do material
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
}



