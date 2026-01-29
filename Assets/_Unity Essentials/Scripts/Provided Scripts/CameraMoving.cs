// Source - https://stackoverflow.com/a
// Posted by Zaphod, modified by community. See post 'Timeline' for change history
// Retrieved 2026-01-29, License - CC BY-SA 4.0

using UnityEngine;

public class Follow_player : MonoBehaviour {

    public Transform player;
    public float abs_X;
    public float abs_Y;
    private float cam_x;
    private float cam_y;

    // Update is called once per frame
    void Update () {
        if (player.transform.position.x > abs_X)
            cam_x = abs_X - player.transform.position.x;
        else if (player.transform.position.x < -abs_X)
            cam_x = -abs_X - player.transform.position.x;
        else
            cam_x = 0;
        if (player.transform.position.y > abs_Y)
            cam_y = abs_Y - player.transform.position.y;
        else if (player.transform.position.y < -abs_Y)
            cam_y = -abs_Y - player.transform.position.y;
        else
            cam_y = 0;
        transform.position = player.transform.position + new Vector3(cam_x, cam_y, -10);

    }
}
