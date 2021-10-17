namespace MediatRCORSTrial.Core.Common.Contracts
{
    public class MediatRCORSTrialCoreApiConfiguration : IMediatRCORSTrialCoreApiConfiguration
    {
        public string ContextKey { get; set; }
        public string ContextName { get; set; }
        public bool MockIntegration { get; set; }

        public string GetContextKey() { return ContextKey; }
        public string GetContextName() { return ContextName; }


    }

}
