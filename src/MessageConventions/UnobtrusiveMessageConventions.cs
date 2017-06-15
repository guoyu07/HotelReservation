namespace MessageConventions
{
    using NServiceBus;

    internal class UnobtrusiveMessageConventions : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.Conventions()
                .DefiningCommandsAs(t =>
                    t.Namespace != null && t.Namespace.EndsWith("Messages.Commands"))
                .DefiningEventsAs(t =>
                    t.Namespace != null && t.Namespace.EndsWith("Messages.Events"));
        }
    }
}