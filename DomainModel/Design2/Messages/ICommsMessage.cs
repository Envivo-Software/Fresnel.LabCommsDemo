using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design2.Messages
{
    public interface ICommsMessage : IPersistable, IValueObject
    {
        /// <summary>
        /// The public facing ID for the associated Sample
        /// </summary>
        [AllowedOperations(canModify: false)]
        public string? ExternalId { get; set; }

        /// <summary>
        /// The Laboratory associated with this Message
        /// </summary>
        [UI(renderOption: UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)]
        public Laboratory Laboratory { get; set; }

        [Visible(false)]
        public DateTime? MessageDate { get; }

        /// <summary>
        /// Optional: Any comments or instructions for the Lab 
        /// </summary>
        public string? Comments { get; }
    }
}
