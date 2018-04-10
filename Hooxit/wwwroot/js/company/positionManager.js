// TODO: Refactor: think of a better design !!!
// 1. Move to updatePosition.js
// 2. Move updating of Skills to updateSkills.js

class PositionSettings {
    static get elementsIds() {
        return Object.freeze({
        });
    }

    static get editorSettings() {
        return Object.freeze({
            modules: {
                toolbar: true
            },
            theme: 'snow'
        });
    }

    static get updateRequestSettings() {
        return Object.freeze({
            'descriptionEditor': {
                url: '/Company/Positions/ChangeDescription'
            },
            'lookingForEditor': {
                url: '/Company/Positions/ChangeLookingForDescription'
            },
            'whatWeOfferEditor': {
                url: '/Company/Positions/ChangeWhatWeOfferDescription'
            },
            'responsibilitiesEditor': {
                url: '/Company/Positions/ChangeResponsibilitiesDescription'
            }
        });
    }
}

class PositionManager{
    constructor() {
        this.elementIds = PositionSettings.elementsIds;
        this.descriptionEditor = new TextEditor('#descriptionEditor', PositionSettings.editorSettings);
        this.lookingForEditor = new TextEditor('#lookingForEditor', PositionSettings.editorSettings);
        this.whatWeOfferEditor = new TextEditor('#whatWeOfferEditor', PositionSettings.editorSettings);
        this.responsibilitiesEditor = new TextEditor('#responsibilitiesEditor', PositionSettings.editorSettings);
        this.skillsManager = new SkillsManager('#skillsContainer', '#searchSkill');

        this.editorConfirmedToUpdate = null;

        this.editorIDRelation = new Map([
            ['#descriptionEditor', this.descriptionEditor],
            ['#lookingForEditor', this.lookingForEditor],
            ['#whatWeOfferEditor', this.whatWeOfferEditor],
            ['#responsibilitiesEditor', this.responsibilitiesEditor]
        ]);

        this.init();
        this.registerEventHandlers();
    }

    populatePositionSections(sectionWrapper) {
        let hiddenEditorValue = $(sectionWrapper).find('.hiddenEditorContent').val();
        let editorId = $($(sectionWrapper).find('.editor')[0]).attr('id');
        let editorInstance = this.editorIDRelation.get('#' + editorId);

        editorInstance.setText(hiddenEditorValue);

        $(sectionWrapper).find('.sectionPresentation').append(editorInstance.textEditor.root.innerHTML);
        $(sectionWrapper).find('.sectionPresentation').find('br').remove();
    }

    init() {
        $('.sectionWrapper').toArray().forEach((element) => this.populatePositionSections.call(this, element));
    }

    updateClick(event) {
        event.preventDefault();

        let wrapper = $(event.target).closest('.sectionWrapper');
        let presentationContainer = $(wrapper.find('.sectionPresentation')[0]);
        let editorId = $(wrapper.find('.editor')).show();

        presentationContainer.hide();

        wrapper.find('.ql-toolbar').show();
        wrapper.find('.toggle-save').show();
        wrapper.find('.toggle-close').show();
        wrapper.find('.toggle-update').hide();
    }

    closeClick(event) {
        event.preventDefault();

        let wrapper = $(event.target).closest('.sectionWrapper');
        let presentationContainer = $(wrapper.find('.sectionPresentation')[0]);
        let editorId = $(wrapper.find('.editor')).hide();

        presentationContainer.show();

        wrapper.find('.ql-toolbar').hide();
        wrapper.find('.toggle-save').hide();
        wrapper.find('.toggle-close').hide();
        wrapper.find('.toggle-update').show();
    }

    onSkillFieldChange(event) {
        event.preventDefault();

        this.skillsManager.search();
    }

    onPositionSuccessfulyUpdated(response) {
        toastr.success("Position updated successfuly!");
    }

    onPositionUpdateError(response) {
        toastr.error("Something wrong happened! Position was not updated.");
    }

    onPositionSuccessfulyUpdatedCallBack(newValue) {
        $('.positionName').text(newValue);
        $('.positionNameContainer').hide();
        $('.positionName').show();
        $('.togglePositionNameUpdate').show();

        toastr.success("Position updated successfuly!");
    }

    onSkillsUpdateClick(event) {
        event.preventDefault();
        $('.skillsPresentation').hide();
        $('.updateSkill').show();
        $('.skill-update').hide();
        $('.skill-close').show();
        $('.skill-save').show();
    }

    onSkillsCloseClick(event) {
        event.preventDefault();

        $('.skillsPresentation').show();
        $('.updateSkill').hide();
        $('.skill-update').show();
        $('.skill-close').hide();
        $('.skill-save').hide();
    }

    onSkillsUpdate(event) {
        event.preventDefault();

        let selectedSkills = [];

        $('.selectedSkill').toArray().forEach((element) => {
            let skillValue = $(element).attr('value');
            selectedSkills.push(new Number(skillValue));
        });

        let request = {
                PositionId: new Number($('#positionId').val()),
        }

        ajaxRequest.sendAjax('/Company/Positions/ChangeSkills', { 'PositionId': new Number($('#positionId').val()), 'RequiredSkills': selectedSkills}, 'POST', 'application/json', null, this.onPositionSuccessfulyUpdated, this.onPositionUpdateError);
    }

    registerEventHandlers() {
        $('.toggle-update').on('click', this.updateClick.bind(this));
        $('.toggle-close').on('click', this.closeClick.bind(this));
        $('#searchSkill').on('change', this.onSkillFieldChange.bind(this));

        $('.togglePositionClose').on('click', (event) => {
            $('.positionNameContainer').hide();
            $('.positionName').show();
            $('.togglePositionNameUpdate').show();
        });

        $('#doPositionNameSave').on('click', (event) => {
            let newPositionName = $('.positionNameUpdate').val();
            ajaxRequest.sendAjax('/Company/Positions/ChangePositionName',
                { 'PositionId': new Number($('#positionId').val()), 'PositionName': newPositionName },
                'POST',
                'application/json',
                null,
                this.onPositionSuccessfulyUpdatedCallBack.bind(this, newPositionName),
                this.onPositionUpdateError);
        });

        $('.skill-update').on('click', (event) => {
            this.onSkillsUpdateClick.call(this, event)
        });

        $('.skill-close').on('click', (event) => {
            this.onSkillsCloseClick.call(this, event);
        });

        $('#doSkillsSave').on('click', (event) => {
            this.onSkillsUpdate.call(this, event);
        });

        $('.toggle-save').on('click', (event) => {
            event.preventDefault();

            let sectionWrapper = $(event.target).closest('.sectionWrapper');
            let editorId = $($(sectionWrapper).find('.editor')[0]).attr('id');
            let editorInstance = this.editorIDRelation.get('#' + editorId);

            this.editorConfirmedToUpdate = {
                id: editorId,
                instance: editorInstance
            };
        });

        $('#doSave').on('click', (event) => {
            event.preventDefault();

            let wrapper = $('#' + this.editorConfirmedToUpdate.id).closest('.sectionWrapper');
            let presentationContainer = $(wrapper.find('.sectionPresentation')[0]);
            let contentForSubmit = this.editorConfirmedToUpdate.instance.getContents();
            let updateRequestSettings = PositionSettings.updateRequestSettings;
            let positionId = $('#positionId').val();

            var data = {
                'Id': new Number(positionId),
                'Content': JSON.stringify(contentForSubmit)
            };

            ajaxRequest.sendAjax(updateRequestSettings[this.editorConfirmedToUpdate.id].url, data, 'POST', 'application/json', null, this.onPositionSuccessfulyUpdated, this.onPositionUpdateError);
            presentationContainer.show();

            $(wrapper.find('.editor')).hide();
            wrapper.find('.ql-toolbar').hide();
            wrapper.find('.toggle-save').hide();
            wrapper.find('.toggle-close').hide();
            wrapper.find('.toggle-update').show();

            $(wrapper).find('.sectionPresentation').empty();
            $(wrapper).find('.sectionPresentation').append(this.editorConfirmedToUpdate.instance.textEditor.root.innerHTML);
            $(wrapper).find('.sectionPresentation').find('br').remove();
        });

        $('.togglePositionNameUpdate').on('click', (event) => {
            $('.positionNameContainer').show();
            $('.positionName').hide();
            $('.togglePositionNameUpdate').hide();

            let positionName = $('.positionName').text();
            $('.positionNameUpdate').val(positionName);

        });
    }
}

new PositionManager();