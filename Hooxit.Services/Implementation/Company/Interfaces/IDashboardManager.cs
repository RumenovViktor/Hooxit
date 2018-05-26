﻿using Hooxit.Presentation.Company.Read.Matching;
using Hooxit.Presentation.Read.Matching;

namespace Hooxit.Services.Company.Interfaces
{
    public interface IDashboardManager
    {
        MatchModel<SuggestedCandidate, int> GetSuggestions(int Id);
    }
}
