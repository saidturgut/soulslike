public class Player : PlayerRanged
{   
    public static Player s { get; private set; }

    private void Awake() { s = this; }

    private void Start()
    {
        base.Init();
    }

    private void Update()
    {
        if (health <= 0) { KillCharacter(); return; }

        base.Tick();
    }
}
