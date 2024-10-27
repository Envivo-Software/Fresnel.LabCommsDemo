namespace LabCommsModel.Design2.Messages.OutgoingMessages
{
    public interface IOutgoingMessage : ICommsMessage
    {
        /// <summary>
        /// The time when the message was sent
        /// </summary>
        public DateTime? SentAt { get; }
    }
}
