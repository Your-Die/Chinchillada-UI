namespace Chinchillada.UI
{
    public interface ITribunePresenter<T> : IFreezableTribune, IPriorityExecutor<T>
    {
        void Summon(object summoner, int priority, T content);
    }
}