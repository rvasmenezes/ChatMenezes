namespace ChatMenezes.Domain
{
    public class ErroViewModel
    {
        public string RequestId { get; set; }
        public string Messagem { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
