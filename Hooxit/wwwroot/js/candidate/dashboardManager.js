class CandidateDashboardSettings {
    static get customSettings() {
        return Object.freeze({
            relationBaseUrl: '/Candidate/GetCandidateRelation',
            openMatchUrl: '/ViewPosition/'
        });
    }

    static get elementsIds() {
        return Object.freeze({
            containerId: '.suggestedPositionsWrapper'
        });
    }
}

class DashboardManager {
    constructor() {
        this.init();
        this.registerEventListeners();

        this._settings = CandidateDashboardSettings.customSettings;
        this._elementsIds = CandidateDashboardSettings.elementsIds;

        this._dashboardSuggestionsManager = new DashboardSuggestionsManager(this._elementsIds.containerId, this._settings);
    }

    onGetSuggestedPositionsResponse(response) {
        $('.defaultSuggestedPositionsWrapper').hide();
        $('.suggestedPositionsWrapper').html(response);

        this._dashboardSuggestionsManager.registerEventListeners();
    }

    registerEventListeners() {
    }

    init() {
        ajaxRequest.getData("/Candidate/SuggestedPositions/", this.onGetSuggestedPositionsResponse.bind(this));
    }
}

new DashboardManager();