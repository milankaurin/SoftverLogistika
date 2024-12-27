namespace SoftverLogistikaBack.Services
{
    public class AuthService
    {
        private readonly List<Guid> _activeTokens = new();

        public Guid GenerateToken()
        {
            var token = Guid.NewGuid();
            _activeTokens.Add(token);
            return token;
        }

        public bool ValidateToken(Guid token)
        {
            return _activeTokens.Contains(token);
        }

        public void RemoveToken(Guid token)
        {
            _activeTokens.Remove(token);
        }
    }
}
