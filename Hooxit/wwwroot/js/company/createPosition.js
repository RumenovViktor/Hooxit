class CreatePositionEditorSettings {
    static get getElementsIds() {
        return Object.freeze({
            companyDescriptionId: '#company-description-text-editor',
            lookingForId: '#looking-for-text-editor',
            responsibilitiesId: '#responsibilities-text-editor',
            whatWeOfferId:'#what-we-offer-text-editor'
        });
    }

    static get getSettings() {
        return Object.freeze({
            modules: {
                toolbar: true
            },
            theme: 'snow'
        });
    }
}

class PositionsManager {
    constructor() {
        const editorSettings = CreatePositionEditorSettings.getElementsIds;

        this.descriptionTextEditor = new TextEditor(editorSettings.companyDescriptionId);
        this.lookingForTextEditor = new TextEditor(editorSettings.lookingForId);
        this.responsibilitiesTextEditor = new TextEditor(editorSettings.responsibilitiesId);
        this.whatWeOfferTextEditor = new TextEditor(editorSettings.whatWeOfferId);
    }
}


new PositionsManager();