using UnityEngine;

public class Follow_player_2 : MonoBehaviour {

    public Transform player;
    public SpriteRenderer map; 
    private Camera cam;
    private float abs_X;
    private float abs_Y;
    private float cam_x;
    private float cam_y;

    void Start() {
        // Limites map - taille caméra
        abs_X = map.bounds.extents.x;
        abs_Y = map.bounds.extents.y;
    }

    void Update() {
        cam_x = Mathf.Clamp(player.position.x, -abs_X, abs_X);
        cam_y = Mathf.Clamp(player.position.y, -abs_Y, abs_Y);
        transform.position = new Vector3(cam_x, cam_y, -10);
    }
}
