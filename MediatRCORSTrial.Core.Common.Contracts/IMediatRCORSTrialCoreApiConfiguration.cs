namespace MediatRCORSTrial.Core.Common.Contracts
{
    public interface IMediatRCORSTrialCoreApiConfiguration
    {

        public string GetContextKey();
        public string GetContextName();
    }
}
