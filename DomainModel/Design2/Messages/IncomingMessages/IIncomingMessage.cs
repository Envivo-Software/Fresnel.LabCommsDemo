namespace LabCommsModel.Design2.Messages.IncomingMessages
{
    public interface IIncomingMessage : ICommsMessage
    {

        /// <summary>
        /// The time when the message was delivered
        /// </summary>
        public DateTime ReceivedAt { get; set; }
    }
}
