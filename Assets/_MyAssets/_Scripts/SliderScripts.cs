

using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SliderScripts : MonoBehaviour
{
    [SerializeField]GameObject Handle ;
    [SerializeField] TMP_Text value;
    [SerializeField] private Slider sl;
    //private float valStart;//
   
 
    void Start()
    {
        sl = GetComponent<Slider>();//connect sl in scripts with the SLider in secens 
        //valStart = value.transform.position.x;//fix the TMP_text at a fixed start postion //
    }

    
    void Update()
    {   //Tmp_Text is the handle's value tranform into a 1.00 (two decimal points)
        value.text = sl.value.ToString("F2");
        //make the text move with handle 
        value.transform.position = new Vector3(Handle.transform.position.x,value.transform.position.y,value.transform.position.z);
    }
   


}
