class ProfileSettings {
    static get elementsIds() {
        return Object.freeze({
            addSkillBtn: '.candidateAddSkillBtn',
            noSkillsContainer: '.noSkillsContainer',
            skillsPanel: '.skillsPanel',
            skillsContainerId: '.skillsContainer',
            searchSkillFieldId: '#searchSkill',
            selectedSkillClass: '.selectedSkill',
            saveSkills: '.saveSkills'
        });
    }

    static get urls() {
        return Object.freeze({
            updateSkills: '/Candidate/Profile/ChangeSkills'
        });
    }
}

class ProfileManager {
    constructor() {
        this.elementsIds = ProfileSettings.elementsIds;
        this.urls = ProfileSettings.urls;
        this.skillsManager = new SkillsManager(this.elementsIds.skillsContainerId, this.elementsIds.searchSkillFieldId);

        this.registerEventListeners();
    }

    onSkillsSuccessfulyUpdate() {
        toastr.success("Skills updated successfuly!");
    }

    onError() {
        toastr.error("Something wrong happened! Profile was not updated.");
    }

    changeSkillsHandler() {
        let selectedSkills = [];

        $(this.elementsIds.skillsContainerId).not('.skillsPresentationContainer')
            .find(this.elementsIds.selectedSkillClass)
            .toArray()
            .forEach((item, index) => {
                let selectedSkillId = new Number($(item).attr('value'));
                selectedSkills.push(new Number(selectedSkillId));
        });

        ajaxRequest.sendAjax(this.urls.updateSkills, { 'Skills': selectedSkills }, 'POST', 'application/json', null, this.onSkillsSuccessfulyUpdate, this.onError);
    }

    onSkillFieldChange() {
        this.skillsManager.search();
    }

    onAddSkillClick() {
        $(this.elementsIds.noSkillsContainer).hide();
        $('.updateSkills').hide();
        $(this.elementsIds.skillsPanel).show();
        $('.skillsCloseUpdate').show();
        $('.saveSkills').show();
        $('.skillsPresentationContainer').hide();
    }

    registerEventListeners() {
        $(this.elementsIds.addSkillBtn + ', .updateSkills').on('click', this.onAddSkillClick.bind(this));
        $(this.elementsIds.searchSkillFieldId).on('change', this.onSkillFieldChange.bind(this));
        $(this.elementsIds.saveSkills).on('click', this.changeSkillsHandler.bind(this));
        $(this.elementsIds.skillsContainerId).find('a.fa.fa-remove').on('click', (event) => { this.skillsManager.onRemoveSelectedSkill.call(this, event) });
        $('.skillsCloseUpdate').on('click', (event) => {
            $('.updateSkills').show();
            $(this.elementsIds.skillsPanel).hide();
            $('.skillsCloseUpdate').hide();
            $('.saveSkills').hide();
            $('.skillsPresentationContainer').show();
        });
    }
}

new ProfileManager();