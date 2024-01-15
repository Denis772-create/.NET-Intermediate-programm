namespace AlteringBehavior.Task6;

public class Restaurant
{
    public void CookMasala(Country country)
    {
        MasalaCooker cooker = country switch
        {
            Country.India => new IndianMasalaCooker(),
            Country.Ukraine => new UkrainianMasalaCooker()
        };

        cooker.CookMasala();
    }
}
