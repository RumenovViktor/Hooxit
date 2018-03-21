class SkillsManager {
    constructor(containerId, searchFieldId) {
        this.containerId = containerId;
        this.searchFieldId = searchFieldId;
    }

    get skillsSelectWrapper() {
        return $('.skillsList');
    }

    onGetSkillsSuccess(response) {
        let skillsListWrapper = this.skillsSelectWrapper;

        $('.skillElement').remove();

        if (!response.length) {
            $('.skillElement').off();
            skillsListWrapper.hide();
            return;
        }

        if (response.length > 10) {
            this.skillsSelectWrapper.removeClass('autoHeight');
            this.skillsSelectWrapper.addClass('fixedHeight');
        } else {
            this.skillsSelectWrapper.removeClass('fixedHeight');
            this.skillsSelectWrapper.addClass('autoHeight');
        }

        response.forEach((item) => {
            skillsListWrapper.append(
                $('<a>' + item.name + '</a>').attr('href', '#').attr('class', 'list-group-item waves-effect skillElement')
            );
        });

        this.skillsSelectWrapper.show();
        this.registerEventHandlers.call(this);
    }

    onGetSkillsError() {

    }

    search() {
        let name = $(this.searchFieldId).val();

        if (name === '') {
            $('.skillElement').remove();
            this.skillsSelectWrapper.hide();

            return;
        }

        const data = { 'skillName': name };
        ajaxRequest.getData('/Skills/GetSkills/' + name, this.onGetSkillsSuccess.bind(this));
    }

    createSkillTags(event) {
        let elementValue = event.target.text.trim();
        $(event.target).addClass('disabled');
        $('.noSkillsText').hide();

        var elementsWithSameName = $(this.containerId + ' ' + '.selectedSkill').filter((index, value) => {
            return $(value).text() === elementValue;
        });

        if (elementsWithSameName.length) {
            return;
        }

        $(this.containerId).append(
            $('<label class="badge badge-info selectedSkill">' + elementValue + '<a class="fa fa-remove"></a></label>')
        );

        let lastAddedElement = $(this.containerId).find('.selectedSkill').last();

        lastAddedElement.find('a').on('click', (event) => { this.onRemoveSelectedSkill.call(this, event) });
    }

    onRemoveSelectedSkill(event) {
        $(event.target).off()
        $(event.target).parent().remove()

        let numberOfSelectedSkills = $(this.containerId).find('.selectedSkill').length;

        if (!numberOfSelectedSkills) {
            $('.noSkillsText').show();
        }
    }

    registerEventHandlers() {
        $('.skillElement').on('click', (event) => { this.createSkillTags.call(this, event) });
    }
}