var userExperience = (function () {
    var _properties = {
        events: {
            click: 'click'
        },
        experienceFormId: '#addExperienceForm',
        experienceContainerId: '#experienceContainer',
        addExperienceClass: '.addExperience',
        displayFieldWrapperClass: '.userInfodisplayValue',
        createdPositionClass: '.createdPosition',
        positionForUpdateClass: '.positionForUpdate',
        positionForUpdateInputClass: '.positionForUpdateInput',
        updateUserInfoPositionClass: '.updateUserInfoPosition',
        updateExperienceBtn: '.updateExperienceBtn'
    };

    this.initialize = function () {
        $(_properties.addExperienceClass).on(_properties.events.click, function () {
                $(_properties.experienceFormId).toggle();
        });

        $(_properties.updateExperienceBtn).on(_properties.events.click, function (event) {
            event.preventDefault();

            var element = $(event.target);
            var dataTargetAttrSplited = element.parent().attr('data-target').split("-");
            this.clickedExperienceId = dataTargetAttrSplited[dataTargetAttrSplited.length - 1];

        }.bind(this));
    }

    this.appendNewExperience = function (data) {
        if (!data)
            return;

        $(_properties.experienceContainerId).append(data.responseText);

        var positionForUpdate = $(_properties.updateUserInfoPositionClass);

        if (positionForUpdate.length) {
            $(_properties.positionForUpdateClass).text(positionForUpdate.text());
            $(_properties.positionForUpdateInputClass).val(positionForUpdate.text());
        }
    }

    this.appendUpdatedExperience = function (data) {
        $('#expContainer-' + userExperience.clickedExperienceId).remove();
        $(_properties.experienceContainerId).append(data.responseText);
    }

    return this;
})();

$(document).ready(function () {
    userExperience.initialize();
});