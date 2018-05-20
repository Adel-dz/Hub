namespace DGD.Hub.Jobs
{
    interface IJobFeedback
    {
        string FeedbackText { get; set; }

        void Start();
        void Close();
    }
}
