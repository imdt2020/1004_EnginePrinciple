using UnityEngine;
using System.Collections;

public class RPGController : MonoBehaviour
{
    public RPGCharacter character;

    public RPGHUD hud;
    public RPGInput input;
    public RPGCamera camera;


    // Use this for initialization
    void Start()
    {
        if(this.character)
        {
            this.character.SetController(this);
            this.camera.SetCharacter(character.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Possess(RPGCharacter _character)
    {
        UnPossess();
        this.character = _character;
        this.character.SetController(this);
        this.camera.SetCharacter(_character.transform);
    }

    public void UnPossess()
    {
        if(this.character)
        {
            this.character.SetController(null);
        }
        this.character = null;
        this.camera.SetCharacter(null);
    }


}