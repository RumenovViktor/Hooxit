namespace Hooxit.Presentation.Implemenation.Company.Read.Matching
{
    public class SuggestedCandidate
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public decimal MatchedPercentage { get; set; }

        public string FormatedMatchedPercentage { get; set; }

        public string UserName { get; set; }
    }
}
