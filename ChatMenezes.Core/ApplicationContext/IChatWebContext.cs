namespace ChatMenezes.Core.ApplicationContext
{
    public interface IChatWebContext
    {
        public string UserLoggedId { get; }
        public string UserLoggedEmail { get; }
        public string? UserLoggedName { get; }
    }
}
