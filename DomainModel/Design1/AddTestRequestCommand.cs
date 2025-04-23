using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design1
{
    /// <summary>
    /// Executes the process of sending a TestRequest to a Laboratory
    /// </summary>
    [Visible(isVisibleInLibrary: false)]
    public class AddTestRequestCommand :
        ICommandObject<Sample>,
        IValueObject
    {
        public Guid Id { get; set; }

        /// <summary>
        /// The Laboratory that currently possesses the B-Sample
        /// </summary>
        public Laboratory Laboratory { get; set; }

        /// <summary>
        /// Optional: Comments for the Lab
        /// </summary>
        public string? CommentsToLab { get; set; }

        /// <summary>
        /// Is the request for the B Sample?
        /// </summary>
        public bool Is_B_Sample { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsReadyToExecute => Laboratory != null;

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns></returns>
        public void Execute(Sample sample)
        {
            var newTestRequest = new TestRequest
            {
                Id = Guid.NewGuid(),
                TargetLab = Laboratory,
                Sample = sample,
                Comments = CommentsToLab,
                Is_B_Sample = Is_B_Sample
            };

            sample.TestRequests.Add(newTestRequest);
            newTestRequest.SendTestRequest();
        }
    }
}
