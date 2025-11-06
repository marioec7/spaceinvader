using UnityEngine;

public class bloque : MonoBehaviour
{
    [Header("Configuración de Salud")]
    [SerializeField] private int maximoGolpes = 4;
    private int golpesActuales;

    [Header("Configuración de Colisión")]
    [SerializeField] private string tagBalaEnemiga = "EnemyBullet";

    [Header("Sprites de Daño (del más dañado al intacto)")]
    [SerializeField] private Sprite[] spritesDeDano;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        golpesActuales = maximoGolpes;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer == null)
        {
            Debug.LogError("El componente SpriteRenderer no se encontró en el objeto " + gameObject.name +
                           ". ¡Asegúrate de que el bloque lo tenga!", this);
        }

       
        if (spritesDeDano.Length != maximoGolpes)
        {
            Debug.LogWarning("El número de Sprites de Daño (" + spritesDeDano.Length +
                             ") no coincide con el Máximo de Golpes (" + maximoGolpes +
                             ") en el bloque " + gameObject.name + ". ¡Asegúrate de tener un sprite por cada golpe!", this);
        }

        ActualizarSprite(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagBalaEnemiga))
        {
            Destroy(collision.gameObject); 

            golpesActuales--;

            GestionarDano(); 
        }
    }

    private void GestionarDano()
    {
        if (golpesActuales > 0)
        {
            ActualizarSprite(); 
        }
        else
        {
            DestruirBloque(); 
        }
    }

    private void ActualizarSprite()
    {
        if (_spriteRenderer != null && spritesDeDano != null && spritesDeDano.Length > 0)
        {
           
            int spriteIndex = golpesActuales - 1;

            if (spriteIndex >= 0 && spriteIndex < spritesDeDano.Length)
            {
                _spriteRenderer.sprite = spritesDeDano[spriteIndex];
            }
            else
            {
                Debug.LogWarning("Índice de sprite fuera de rango. ¿Has configurado suficientes sprites para todos los estados de daño?", this);
            }
        }
    }

    private void DestruirBloque()
    {
        Debug.Log("Bloque " + gameObject.name + " destruido.");
        Destroy(gameObject);
    }
}