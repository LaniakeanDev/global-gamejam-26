// Source - https://stackoverflow.com/a
// Posted by Zaphod, modified by community. See post 'Timeline' for change history
// Retrieved 2026-01-29, License - CC BY-SA 4.0

using UnityEngine;

public class Follow_player : MonoBehaviour {

    public Transform player;
    public SpriteRenderer map; 
    private Camera cam;
    private float abs_X;
    private float abs_Y;
    private float cam_x;
    private float cam_y;

    void Start() {
        cam = GetComponent<Camera>();
        
        // Taille de la caméra (important !)
        float camVert = cam.orthographicSize;
        float camHoriz = cam.aspect * camVert;
        
        // Limites map - taille caméra
        abs_X = map.bounds.extents.x - camHoriz;
        abs_Y = map.bounds.extents.y - camVert;
        
        // Debug.Log($"Limites caméra: X={abs_X}, Y={abs_Y}");
    }

    void Update() {
        cam_x = Mathf.Clamp(player.position.x, -abs_X, abs_X);
        cam_y = Mathf.Clamp(player.position.y, -abs_Y, abs_Y);
        transform.position = new Vector3(cam_x, cam_y, -10);
    }
}
