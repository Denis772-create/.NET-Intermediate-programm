namespace AlteringBehavior.Task6;

public abstract class MasalaCooker
{
    public void CookMasala()
    {
        CookRice();
        CookChicken();
        CookTea();
    }

    protected abstract void CookRice();
    protected abstract void CookChicken();
    protected abstract void CookTea();
}