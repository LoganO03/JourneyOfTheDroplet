using UnityEngine;

[ExecuteInEditMode]
public class ErodableCollectionParent : MonoBehaviour {
    [SerializeField] GameObject[] shapes;
    [SerializeField] float granularity;
    [SerializeField] float seed;
    [SerializeField] int pixelsPerUnit;
    [SerializeField] int textureSize;
    private Vector3[] shapeMem;
    public const float sqrt2 = 1.4142135623730950488016887242097f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        shapeMem = new Vector3[shapes.Length];
        for(int i = 0; i < shapes.Length; i++) {
            shapeMem[i] = shapes[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update() {
        if(!Application.isPlaying) {
            for (int j = 0; j < shapeMem.Length; j++) {
                GameObject shape = shapes[j];
                if(shape.transform.position != shapeMem[j]){
                    int xSize = (int) (shape.transform.localScale.x * pixelsPerUnit);
                    int ySize = (int) (shape.transform.localScale.y * pixelsPerUnit);
                    Texture2D rocks = new Texture2D(xSize, ySize);
                    Texture2D voronoi = new Texture2D(xSize, ySize);
                    var points = generatePoints(shape);
                    var transparencyList = new float[points.Length];
                    
                    float pixelspu = pixelsPerUnit;
                    Vector2 worldMax = new Vector2(0, 0);
                    Vector2 worldMin = new Vector2(shape.transform.localScale.x, shape.transform.localScale.y);
                    for (int x = 0; x < xSize; x++) {
                        for (int y = 0; y < ySize; y++) {
                            float leastDist = xSize * sqrt2;
                            float closest = -1;
                            
                            for (int i = 0; i < points.Length; i++) {
                                Vector2 p = PixelToWorld(x, y, xSize, ySize, worldMax, worldMin);
                                float dist = distanceApproximation(new Vector2(p.x + shape.transform.position.x, p.y + shape.transform.position.y) , points[i]);
                                if(dist < leastDist) {
                                    leastDist = dist;
                                    closest = i;
                                }
                            }
                            if(closest != -1) {
                                float rockIntensity = 1 - leastDist / (sqrt2 * 1.5f);
                                rocks.SetPixel(x, y, new Color(rockIntensity, rockIntensity, rockIntensity, 1));
                                voronoi.SetPixel(x, y, new Color(closest / points.Length, closest / points.Length, closest / points.Length, 1));
                            } else {
                                rocks.SetPixel(x, y, new Color(1,0,1,0.5f));
                                voronoi.SetPixel(x, y, new Color(1,0,1,0.5f));
                            }
                        }
                    }
                    rocks.filterMode = FilterMode.Point;
                    rocks.Apply();
                    voronoi.filterMode = FilterMode.Point;
                    voronoi.Apply();
                    shape.GetComponent<ErodableCollection>().Rocks = rocks;
                    shape.GetComponent<ErodableCollection>().Voronoi = voronoi;
                    Sprite gizmo = Sprite.Create(voronoi, 
                        new Rect(0, 0, shape.transform.localScale.x * pixelspu, shape.transform.localScale.y * pixelspu), 
                        new Vector2(0.5f, 0.5f),
                        pixelsPerUnit * shape.transform.localScale.x);
                    shape.GetComponent<SpriteRenderer>().sprite = gizmo;
                }
                shapeMem = new Vector3[shapes.Length];
                for(int i = 0; i < shapes.Length; i++) {
                    shapeMem[i] = shapes[i].transform.position;
                }
            }
        }
    }

    public static uint Hash2D(int x, int y, uint seed)
    {
        ulong h = seed;

        // Mix in coordinates with large primes
        h ^= (ulong)x * 0x9E3779B97F4A7C15;
        h ^= (ulong)y * 0xC2B2AE3D27D4EB4F;

        // Final avalanche mixing
        h = (h ^ (h >> 30)) * 0xBF58476D1CE4E5B9;
        h = (h ^ (h >> 27)) * 0x94D049BB133111EB;
        h ^= (h >> 31);

        return (uint)(h & 0xFFFFFFFF);
    }

    public static Vector2 GetOffset(int x, int y, uint seed)
    {
        uint hash = Hash2D(x, y, seed);
        float fx = (hash & 0xFFFF) / 65535f;
        float fy = ((hash >> 16) & 0xFFFF) / 65535f;
        return new Vector2(fx, fy);
    }

    Vector2[] generatePoints(GameObject shape) {
        float left = (shape.transform.position.x - (shape.transform.localScale.x / 2 + granularity * 8)) - (shape.transform.position.x - (shape.transform.localScale.x / 2 + granularity * 8)) % granularity;
        float right = (shape.transform.position.x + (shape.transform.localScale.x / 2 + granularity * 8)) - (shape.transform.position.x + (shape.transform.localScale.x / 2 + granularity * 8)) % granularity;
        
        float bottom = (shape.transform.position.y - (shape.transform.localScale.y / 2 + granularity * 8)) - (shape.transform.position.y - (shape.transform.localScale.y / 2 + granularity * 8)) % granularity;
        float top = (shape.transform.position.y + (shape.transform.localScale.y / 2 + granularity * 8)) - (shape.transform.position.y + (shape.transform.localScale.y / 2 + granularity * 8)) % granularity;
        
        var meowt = new Vector2[(int) (((right - left) / granularity + 1) * ((top - bottom) / granularity + 1) + 0.5f)];
        int i = 0;
        for(float x = left; x <= right; x += granularity) {
            for(float y = bottom; y <= top; y += granularity) {
                Vector2 point = new Vector2(x, y) + GetOffset((int)(x * 100), (int)(y * 100), (uint)(int)(seed));
                if(meowt.Length == i) {
                    return meowt;
                }
                meowt[i] = point;
                i++;
            }
        }
        return meowt;
    }

    public static float distanceApproximation(Vector2 p1, Vector2 p2) {
        float dx = Mathf.Abs(p1.x - p2.x);
        float dy = Mathf.Abs(p1.y - p2.y);

        return Mathf.Min(dx, dy) * sqrt2 + (Mathf.Max(dx, dy) - Mathf.Min(dx, dy));
    }

    public static float distanceApproximation(float x, float y, Vector2 p2) {
        float dx = Mathf.Abs(x - p2.x);
        float dy = Mathf.Abs(y - p2.y);

        return Mathf.Min(dx, dy) * sqrt2 + (Mathf.Max(dx, dy) - Mathf.Min(dx, dy));
    }

    Vector2 PixelToWorld(int px, int py, int textureWidth, int textureHeight, Vector2 worldMin, Vector2 worldMax)
    {
        float u = (float)(px + 0.5f) / (textureWidth - 1);
        float v = (float)(py + 0.5f) / (textureHeight - 1);

        float wx = Mathf.Lerp(worldMin.x, worldMax.x, u);
        float wy = Mathf.Lerp(worldMin.y, worldMax.y, v);

        return new Vector2(wx, wy);
    }
    /*void OnDrawGizmos() {
        foreach(GameObject shape in shapes) {
            Gizmos.DrawWireCube(shape.transform.position, Vector2.one * shape.transform.localScale.x);
        }
    }*/
}
