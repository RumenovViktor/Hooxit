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
                $('<a>' + item.name + '</a>')
                    .attr('href', '#')
                    .attr('class', 'list-group-item waves-effect skillElement')
                    .attr('value', item.id)
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
        event.preventDefault();

        let elementValue = event.target.text.trim();
        let selectedSkillId = new Number($(event.target).attr('value'));

        $(event.target).addClass('disabled');
        $('.noSkillsText').hide();

        var elementsWithSameName = $(this.containerId)
            .not('.skillsPresentationContainer')
            .find('.selectedSkill').filter((index, value) => {
                return $(value).text().trim() === elementValue;
            });

        if (elementsWithSameName.length) {
            return;
        }
        
        $(this.containerId).not('.skillsPresentationContainer').append(
            $('<label class="badge badge-info selectedSkill">' + elementValue + '<a class="fa fa-remove"></a></label>')
                .attr('value', selectedSkillId));

        $('.skillsPresentationContainer').append(
            $('<label class="badge badge-info selectedSkill">' + elementValue + '</label>')
                .attr('value', selectedSkillId));

        let lastAddedElement = $(this.containerId).find('.selectedSkill').last();

        lastAddedElement.find('a').on('click', (event) => { this.onRemoveSelectedSkill.call(this, event) });
    }

    onRemoveSelectedSkill(event) {
        event.preventDefault();
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