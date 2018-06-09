class CandidateDashboardSettings {
    static get customSettings() {
        return Object.freeze({
            suggestionsContainer: '#suggestedPositionsWrapper',
            relationBaseUrl: '/Candidate/GetRelation',
            openMatchUrl: '/ViewPosition/'
        });
    }

    static get elementsIds() {
        return Object.freeze({
            containerId: '.suggestedCandidatesWrapper'
        });
    }
}

class DashboardManager {
    constructor() {
        this.init();
        this.registerEventListeners();

        this._settings = CandidateDashboardSettings.customSettings;
        this._elementsIds = CandidateDashboardSettings.elementsIds;
    }

    onGetSuggestedPositionsResponse() {
        $('.defaultSuggestedPositionsWrapper').hide();
        $('.suggestedPositionsWrapper').html(response);
    }

    registerEventListeners() {

    }

    init() {
        ajaxRequest.getData("/Candidate/SuggestedPositions/", this.onGetSuggestedPositionsResponse.bind(this));
    }
}

new DashboardManager();