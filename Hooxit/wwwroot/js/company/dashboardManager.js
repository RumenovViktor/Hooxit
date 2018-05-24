class CandidateSuggestionsSettings {
    static get customSettings() {
        return Object.freeze({
            suggestionsContainer: '#suggestedCandidatesWrapper',
            relationBaseUrl: '/Company/GetRelation',
            openMatchUrl: '/ViewCandidate/'
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
        this._settings = CandidateSuggestionsSettings.customSettings;
        this._elementsIds = CandidateSuggestionsSettings.elementsIds;
        this._dashboardSuggestionsManager = new DashboardSuggestionsManager(this._elementsIds.containerId, this._settings);
    }

    onGetSuggestedCandidatesResponse(response) {
        $('.defaultSuggestedCandidatesWrapper').hide();
        $('.suggestedCandidatesWrapper').html(response);

        this._dashboardSuggestionsManager.registerEventListeners();
    }

    registerEventListeners() {
        $('#positionList').find('a').on('click', (event) => {
            event.preventDefault();
            $('#positionList').find('.active').removeClass('active');

            let selectedPositionElement = event.target;
            $(selectedPositionElement.closest('li')).addClass('active');
            $('.defaultSuggestedCandidatesWrapper').show();
            $('.suggestedCandidatesWrapper').html("");

            let initialSelectedPositionValue = this.getSelectedPosition();
            ajaxRequest.getData("/Company/SuggestedCandidates/" + initialSelectedPositionValue, this.onGetSuggestedCandidatesResponse.bind(this));
        });

        $('#viewRelation').on('click', (event) => {
            event.preventDefault();
            var clickedButton = event.target;
            var rowWrapper = $(clickedButton).closest("tr");
            var userName = rowWrapper.find('td a').val();
            let selectedPositionId = this.getSelectedPosition();

            ajaxRequest.getData("/Company/SuggestedCandidates/" + initialSelectedPositionValue, this.onGetSuggestedCandidatesResponse.bind(this));
        });
    }

    getSelectedPosition() {
        let initialSelectedPosition = $('#positionList').find('.active')[0];
        let initialSelectedPositionValue = $(initialSelectedPosition).find('a').attr('value');

        return initialSelectedPositionValue;
    }

    init() {
        let initialSelectedPositionValue = this.getSelectedPosition();

        ajaxRequest.getData("/Company/SuggestedCandidates/" + initialSelectedPositionValue , this.onGetSuggestedCandidatesResponse.bind(this));
    }
}

new DashboardManager();