namespace EmailService
{
    public class EmailConfiguration
    {
        public string FromAsress { get; set; }
        public string FromName { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsUseSsl { get; set; }
    }
}
