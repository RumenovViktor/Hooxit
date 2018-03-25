class PositionSettings {
    static get presentationHiddenRelation() {
        return new Map([
                ['#description', '#hiddenDescription'],
                ['#lookingFor', '#hiddenLookingFor'],
                ['#responsibilities', '#hiddenResponsibilities'],
                ['#whatWeOffer', '#hiddenWhatWeOffer']
            ]);
    }

    static get elementsIds() {
        return Object.freeze({
            generalEditorId: '#generalEditor'
        });
    }

    static get generalEditorSettings() {
        return Object.freeze({
            modules: {
                toolbar: true
            },
            theme: 'snow'
        });
    }
}

class PositionManager{
    constructor() {
        this.elementIds = PositionSettings.elementsIds;
        this.presentationHiddenRelation = PositionSettings.presentationHiddenRelation;

        this.generalEditor = new TextEditor(this.elementIds.generalEditorId, PositionSettings.generalEditorSettings);

        this.init();
    }

    init() {
        this.presentationHiddenRelation.forEach((key, value) => {
            let hiddenValue = $(key).val();
            this.generalEditor.setText(hiddenValue);
            let html = this.generalEditor.textEditor.root.innerHTML;
            let formattedText = this.generalEditor.getText();

            $(value).append(html);
            $(value).find('br').remove()
            this.generalEditor.deleteText();
        });
    }
}

new PositionManager();