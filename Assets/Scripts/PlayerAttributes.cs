public class PlayerAttributes
{
    private HUD hud;

    private int health;
    private int score;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            hud.SetHealth(value);
        }
    }


    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            hud.SetScore(value);
        }
    }

    //Constructor
    public PlayerAttributes(HUD hud)
    {
        this.hud = hud;
        health = 3;
        score = 0;
    }

    public void UpdateHUD()
    {
        hud.SetHealth(health);
        hud.SetScore(score);
    }
}
