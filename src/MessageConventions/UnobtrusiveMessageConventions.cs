namespace MessageConventions
{
    using NServiceBus;

    internal class UnobtrusiveMessageConventions : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.Conventions()
                .DefiningCommandsAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("Messages") &&
                    t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("Messages") &&
                    t.Namespace.EndsWith("Events"));
        }
    }
}