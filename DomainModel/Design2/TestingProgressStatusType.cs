namespace LabCommsModel.Design2
{
    /// <summary>
    /// The different states that the Testing Process may be in
    /// </summary>
    public enum TestingProgressStatusType
    {
        Unknown,
        Pending,
        Started,
        InProgress,
        Blocked,
        Complete,
        Aborted
    }
}