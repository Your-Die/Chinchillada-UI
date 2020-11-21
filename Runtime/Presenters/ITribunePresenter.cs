namespace Chinchillada.Foundation.UI
{
    public interface ITribunePresenter<T> : IFreezableTribune, IUtilityExecutor<T>
    {
        void Summon(object summoner, int priority, T content);
    }
}