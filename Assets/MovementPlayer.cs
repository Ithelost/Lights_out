using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovementPlayer : MonoBehaviour
{
    public float BatteryCharge = 100f;
    public float speed;
    public GameObject statsui;
    private TextMesh statText;
    public GameObject fogVisual;
    // Use this for initialization
    void Start()
    {
        statText = statsui.GetComponent<TextMesh>();

    }

    // Update is called once per frame
    void Update()
    {
        BatteryCharge -= 10 * Time.deltaTime;
        float horizontal = -Input.GetAxis("Horizontal") * Time.deltaTime * -speed;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //this.transform.Translate((Vector3.right * Time.deltaTime * speed * vertical ));

        //this.transform.Rotate(0, 0, horizontal * rotationspeed);
        this.transform.Translate(horizontal, 0, 0, Space.World);
        this.transform.Translate(0, 0, vertical, Space.World);
        statText.text = "powah: " + ((int)BatteryCharge).ToString();
        if (BatteryCharge < 50)
        {
            fogVisual.SetActive(true);
        }
        else
        {
            fogVisual.SetActive(false);

        }
    }
}
