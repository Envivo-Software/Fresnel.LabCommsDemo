namespace LabCommsModel.Design1
{
    /// <summary>
    /// The different types of results received from a Lab
    /// </summary>
    public enum TestResultType
    {
        /// <summary>
        /// The test result is unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// The test did not reveal any banned substances
        /// </summary>
        Negative,

        /// <summary>
        /// The test revealed banned substances
        /// </summary>
        Positive,

        /// <summary>
        /// The test results are inconclusive
        /// </summary>
        Atypical,
    }
}