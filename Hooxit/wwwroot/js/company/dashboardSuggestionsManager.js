class SuggestionsSettings {
    static get elementIds() {
        return Object.freeze({
            viewRelationClass: '.viewRelation',
            matchId: '#matchId',
            openMatchCls: '.openMatch'
        });
    }
}

class DashboardSuggestionsManager{
    constructor(containerId, customSettings) {
        this.containerId = containerId;
        this.customSettings = customSettings;
        this.settings = SuggestionsSettings.elementIds;
    }

    registerEventListeners() {
        let relationViewBtn = $(this.containerId).find(this.settings.viewRelationClass);
        let openMatchBtn = $(this.containerId).find(this.settings.openMatchCls);

        relationViewBtn.on('click', (event) => {
            event.preventDefault();

            let selectedBtn = event.target;
            let id = $(selectedBtn).attr('value')
            let matchId = $(this.settings.matchId).val();

            window.open(this.customSettings.relationBaseUrl + '?matchId=' + matchId + '&id=' + id);
        });

        openMatchBtn.on('click', (e) => {
            e.preventDefault();

            let selectedBtn = e.target;
            let matchValue = $(selectedBtn).attr('value');

            window.open(this.customSettings.openMatchUrl + matchValue);
        });
    }
}

new DashboardSuggestionsManager();