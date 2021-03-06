using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragButton : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    public CarStatsMath stats;

    public Sprite imageUp;

    public Sprite imageDown;

    private Image image;

    private GameObject car;

    private float DefaultDrag;
    //public float DuckDrag;
    
    private bool flashbutton;
    // Start is called before the first frame update
    void Start()
    {
        
        image = this.GetComponent<Image>();
        image.sprite = imageUp;
        flashbutton = true;
        StartCoroutine(Initiate());
    }
    

    IEnumerator Initiate()
    {
        yield return new WaitForSeconds(1.1f);
        car = GameObject.Find("Car(Clone)");
        DefaultDrag = car.GetComponent<Rigidbody2D>().drag;
        yield return new WaitForSeconds(1.2f);
        image.sprite = imageDown;
        yield return new WaitForSeconds(0.25f);
        image.sprite = imageUp;
        yield return new WaitForSeconds(0.25f);
        image.sprite = imageDown;
        yield return new WaitForSeconds(0.25f);
        image.sprite = imageUp;

        flashbutton = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!flashbutton) image.sprite = imageDown;
        //Output the name of the GameObject that is being clicked
        //Debug.Log(name + " Game Object Click in Progress");


        car.GetComponent<Rigidbody2D>().drag = stats.Drag;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = imageUp;
        car.GetComponent<Rigidbody2D>().drag = DefaultDrag;
    }
}
