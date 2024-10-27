using Envivo.Fresnel.ModelAttributes;

namespace LabCommsModel.Design2.Messages.IncomingMessages
{
    public interface IIncomingMessage : ICommsMessage
    {

        /// <summary>
        /// The time when the message was delivered
        /// </summary>
        [AllowedOperations(canModify: false)]
        public DateTime ReceivedAt { get; set; }
    }
}
