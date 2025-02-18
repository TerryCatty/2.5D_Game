using UnityEngine;

public class MusicDontDestroy : MonoBehaviour
{
    public static MusicDontDestroy instace;

    private void Awake()
    {
        if(instace != null)
        {
            Destroy(instace.gameObject);
        }
        instace = this;

        DontDestroyOnLoad(gameObject);
    }
}
