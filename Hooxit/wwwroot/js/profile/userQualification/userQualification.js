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
        updateUserInfoPositionClass: '.updateUserInfoPosition'
    };

    this.initialize = function () {
        $(_properties.addExperienceClass).on(_properties.events.click, function () {
                $(_properties.experienceFormId).toggle();
        });
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

    return this;
})();

$(document).ready(function () {
    userExperience.initialize();
});