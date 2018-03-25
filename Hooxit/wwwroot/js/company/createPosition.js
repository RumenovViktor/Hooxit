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
            skillsContainerId: '#skillsContainer'
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

class CreatePositionSettings {
    static get elementsIds() {
        return Object.freeze({
            btnCreateId: '#btnCreate',
            selectedSkillClass: '.selectedSkill',
            createPosition: '#createPosition',
            searchSkillFieldId: '#searchSkill'
        });
    }
}

class CreatePositionManager {
    constructor() {
        this.editorSettings = CreatePositionEditorSettings.getElementsIds;
        this.createPositionSettings = CreatePositionSettings.elementsIds;

        this.skillsManager = new SkillsManager(this.editorSettings.skillsContainerId, this.createPositionSettings.searchSkillFieldId);

        this.descriptionTextEditor = new TextEditor(this.editorSettings.companyDescriptionId, CreatePositionEditorSettings.getSettings);
        this.lookingForTextEditor = new TextEditor(this.editorSettings.lookingForId, CreatePositionEditorSettings.getSettings);
        this.responsibilitiesTextEditor = new TextEditor(this.editorSettings.responsibilitiesId, CreatePositionEditorSettings.getSettings);
        this.whatWeOfferTextEditor = new TextEditor(this.editorSettings.whatWeOfferId, CreatePositionEditorSettings.getSettings);

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

        let selectedSkills = [];

        $(this.createPositionSettings.selectedSkillClass).toArray().forEach((item, index) => {
            let selectedSkillId = new Number($(item).attr('value'));

            selectedSkills.push(selectedSkillId)

            let skillHiddenInput = $('<input />').attr('type', 'hidden')
                .attr('name', "RequiredSkills[" + index + "]")
                .attr('value', selectedSkillId);

            $(this.createPositionSettings.createPosition).append(skillHiddenInput);
        });


        $(this.createPositionSettings.createPosition).submit();
    }

    onSkillFieldChange() {
        this.skillsManager.search();
    }

    attachEventListeners() {
        $(this.createPositionSettings.btnCreateId).on('click', this.prepareTextForSubmit.bind(this, event));
        $(this.createPositionSettings.searchSkillFieldId).on('change', this.onSkillFieldChange.bind(this));
    }
}


new CreatePositionManager();