using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    
    readonly float G = 1000f;
    GameObject[] celestials;

    [SerializeField]
    bool IsElipticalOrbit = false;

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("SkyboxCamera");

        SetInitialVelocity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gravity();
    }

    void SetInitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);

                    if (IsElipticalOrbit)
                    {
                        //Orbita eliptyczna = G * M ( 2 / r + 1 / a) gdzie G jest sta�� grawitacyjn�, M jest mas� obiektu centralnego, r jest odleg�o�ci� mi�dzy dwoma cia�ami, a a jest d�ugo�ci� p�osi g��wnej. 
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) * ((2 / r) - (1 / (r * 1.5f))));
                    }
                    else
                    {
                        //Orbita ko�owa = ((G * M) / r)^0.5, gdzie G = sta�a grawitacyjna, M to masa obiektu centralnego,
                        //a r to odleg�o�� mi�dzy dwoma obiektami Ignorujemy mas� obiektu orbituj�cego, gdy masa obiektu orbituj�cego jest znikoma, np. masa Ziemi w por�wnaniu z mas� S�o�ca.
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                    }
                }
            }
        }
    }

    void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                }
            }
        }
    }
}