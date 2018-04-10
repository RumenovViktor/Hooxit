class CompanyDesctiptionSettings {
    static get elementsIds() {
        return Object.freeze({
            descriptionUpdateBtn: 'descriptionUpdateBtn',
            companyDescriptionEditorId: 'companyDescriptionEditor',
            companyDescriptionReminderId: 'descriptionReminder',
            companyDescriptionPresentation: 'companyDescriptionPresentation',
            companyDescriptionHidden: 'companyDescriptionHidden',
            toolbar: '.ql-toolbar'
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
}

class CompanyDescriptionManager {
    constructor() {
        this.elementsIdsSettings = CompanyDesctiptionSettings.elementsIds;
        this.editorSettings = CompanyDesctiptionSettings.editorSettings;
        this.comapanyDescriptionEditor = new TextEditor('#' + this.elementsIdsSettings.companyDescriptionEditorId, this.editorSettings);

        this.registerEventListeners();
        this.populateDescriptionView()
    }

    populateDescriptionView() {
        let hiddenDescription = $('#' + this.elementsIdsSettings.companyDescriptionHidden).val();
        this.comapanyDescriptionEditor.setText(hiddenDescription);
        $('.' + this.elementsIdsSettings.companyDescriptionPresentation).append(this.comapanyDescriptionEditor.textEditor.root.innerHTML);
        $('.update').show();
        $('.' + this.elementsIdsSettings.companyDescriptionPresentation).find('br').remove();
    }

    onDescriptionSuccessfullUpdate() {
        $('.' + this.elementsIdsSettings.companyDescriptionPresentation).show();
        $('#' + this.elementsIdsSettings.companyDescriptionEditorId).hide();
        $('.ql-toolbar').hide();
        $('.closeIcon, .save').hide();
        $('.update').show();

        $(wrapper).find('.sectionPresentation').empty();
        $('.' + this.elementsIdsSettings.companyDescriptionPresentation).append(this.comapanyDescriptionEditor.textEditor.root.innerHTML);
        $('.' + this.elementsIdsSettings.companyDescriptionPresentation).find('br').remove();

        toastr.success("Description updated successfuly!");
    }

    onDescriptionUpdateError() {
        toastr.error("Something wrong happened! Position was not updated.");
    }

    registerEventListeners() {
        $('.' + this.elementsIdsSettings.descriptionUpdateBtn).on('click', (event) => {
            $('.' + this.elementsIdsSettings.companyDescriptionReminderId).hide();
            $('#' + this.elementsIdsSettings.companyDescriptionEditorId).show();
            $('.ql-toolbar').show();

            $('.closeIcon, .save').show();
        });

        $('.closeIcon').on('click', (event) => {
            $('.' + this.elementsIdsSettings.companyDescriptionReminderId).show();
            $('#' + this.elementsIdsSettings.companyDescriptionEditorId).hide();
            $('.ql-toolbar').hide();
            $('.closeIcon, .save').hide();
            $('.update').show();
        });

        $('#doSaveDescription').on('click', (event) => {
            let newDescription = this.comapanyDescriptionEditor.getContents();
            let data = {
                'CompanyDescription': JSON.stringify(newDescription)
            };

            ajaxRequest.sendAjax('/Company/Profile/ChangeDescription', data, 'POST', 'application/json', null, this.onDescriptionSuccessfullUpdate.bind(this), this.onDescriptionUpdateError);
        });

        $('.update').on('click', (event) => {
            $('.' + this.elementsIdsSettings.companyDescriptionReminderId).hide();
            $('#' + this.elementsIdsSettings.companyDescriptionEditorId).show();
            $('.ql-toolbar').show();

            $('.closeIcon, .save').show();
            $('.update').hide();
            $('.companyDescriptionPresentation').hide();
        });
    }
}

new CompanyDescriptionManager();