class CreatePositionEditorSettings {
    static get getElementsIds() {
        return Object.freeze({
            companyDescriptionId: '#company-description-text-editor',
            lookingForId: '#looking-for-text-editor',
            responsibilitiesId: '#responsibilities-text-editor',
            whatWeOfferId: '#what-we-offer-text-editor',
            descriptionHiddenId: '#description',
            lookingForHiddenId: '#lookingFor',
            responsibilitiesHiddenId: '#responsibilities',
            whatWeOfferHiddenId: '#whatWeOffer',
            createPosition: '#createPosition',
            btnCreateId: '#btnCreate',
            skillsContainerId: '#skillsContainer',
            searchSkillFieldId: '#searchSkill'
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

class CreatePositionManager {
    constructor() {
        this.editorSettings = CreatePositionEditorSettings.getElementsIds;

        this.skillsManager = new SkillsManager(this.editorSettings.skillsContainerId, this.editorSettings.searchSkillFieldId);

        this.descriptionTextEditor = new TextEditor(this.editorSettings.companyDescriptionId);
        this.lookingForTextEditor = new TextEditor(this.editorSettings.lookingForId);
        this.responsibilitiesTextEditor = new TextEditor(this.editorSettings.responsibilitiesId);
        this.whatWeOfferTextEditor = new TextEditor(this.editorSettings.whatWeOfferId);

        this.attachEventListeners();
    }

    prepareTextForSubmit(event) {
        let description = this.descriptionTextEditor.textEditor.getContents();
        let lookingFor = this.lookingForTextEditor.textEditor.getContents();
        let responsibilities = this.responsibilitiesTextEditor.textEditor.getContents();
        let whatWeOffer = this.whatWeOfferTextEditor.textEditor.getContents();

        $(this.editorSettings.descriptionHiddenId).val(JSON.stringify(description));
        $(this.editorSettings.lookingForHiddenId).val(JSON.stringify(lookingFor));
        $(this.editorSettings.responsibilitiesHiddenId).val(JSON.stringify(responsibilities));
        $(this.editorSettings.whatWeOfferHiddenId).val(JSON.stringify(whatWeOffer));

        $(this.editorSettings.createPosition).submit();
    }

    onSkillFieldChange() {
        let skillName = $(this.editorSettings.searchSkillFieldId).val();

        this.skillsManager.search(skillName);
    }

    attachEventListeners() {
        $(this.editorSettings.btnCreateId).on('click', this.prepareTextForSubmit.bind(this, event));
        $(this.editorSettings.searchSkillFieldId).on('change', this.onSkillFieldChange);
    }
}


new CreatePositionManager();