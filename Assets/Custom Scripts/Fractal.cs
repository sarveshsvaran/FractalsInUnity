using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    [Range(0,6)]
    public int maxDepth;

    public int depth;

    public Mesh mesh;

    public Material material;

    int childIndex  = 1000;
    //public Material[] materials;

    public float childScale;

    public WaitForSeconds intervalTime;

    public float rotationSpeed;

    [Range(1,10)]
    public int fractalSpawnDistance;

    Quaternion[] alignments = {
        Quaternion.Euler(0f,0f,0f),
        Quaternion.Euler(0,180f,0),
        Quaternion.Euler(0, 0f, -90),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(0f, 90f, 90f),
        Quaternion.Euler(0f, -90f, 90f)
    };

    //int indexValue;

    Vector3[] axis =
    {
         Vector3.up,
         Vector3.down,
         Vector3.right,
         Vector3.left,
         Vector3.forward,
         Vector3.back
    };
    // Use this for initialization
    void Start () {
        //materials = new Material[maxDepth];
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        gameObject.transform.localScale = new Vector3(childScale,childScale,childScale);
        GetComponent<MeshRenderer>().material.color =
            Color.Lerp(Color.white, Color.green, (float)depth / maxDepth);
        if (depth<maxDepth)
        {
            StartCoroutine(CreateFractal());

        }
        //material = materials[depth+1];
    }


    IEnumerator CreateFractal()
    {

        for (int i = 0; i < axis.Length; i++)
        {
            if (i!=childIndex) {
                yield return new WaitForSeconds(0.5f);
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i, i,i+1);
            }
        }
    }

	void Initialize(Fractal parent, int side,int alignmentIndex,int indexValue)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth+1;
        childIndex = indexValue;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localPosition =parent.axis[side]*(parent.transform.localScale.y*1+ 0.5f*childScale);
        transform.localScale = Vector3.one * childScale;
        rotationSpeed = parent.rotationSpeed;
        transform.localRotation = parent.alignments[alignmentIndex];
    }
	// Update is called once per frame
	void Update () {
        //rotationSpeed = Random.Range(-rotationSpeed,rotationSpeed);
        transform.Rotate(0, rotationSpeed*Time.deltaTime,0);
	}
}
