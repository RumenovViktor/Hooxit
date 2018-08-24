class ProductsManagerSettings {
    static get elementsIds() {
        return Object.freeze({
            descriptionEditorId: 'productDescriptionEditor'
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

class ProductsManager {
    constructor() {
        this.registerEventListeners();
        this.editorSettings = ProductsManagerSettings.editorSettings;
        this.elementsIdsSettings = ProductsManagerSettings.elementsIds;

        this.descriptionEditor = new TextEditor('#' + this.elementsIdsSettings.descriptionEditorId, this.editorSettings);
    }

    onProductSuccessfulyCreated() {
        $('.check_mark').show();
        $('#createProduct').hide();
        $(".sa-success").addClass("hide");
        setTimeout(function () {
            $(".sa-success").removeClass("hide");
        }, 10);
    }

    onProductCreateError() {

    }

    registerEventListeners() {
        $('#createProduct').on('click', (event) => {
            event.preventDefault();

            let container = $('#companyAddProductModal');
            const url = container.find('#url').val();
            const name = container.find('#productName').val();
            const description = this.descriptionEditor.textEditor.getContents();
            const data = {
                'Url': url,
                'Description': JSON.stringify(description),
                'Name': name 
            };

            ajaxRequest.sendAjax('/Company/Profile/CreateProduct', data, 'POST', 'application/json', null, this.onProductSuccessfulyCreated, this.onProductCreateError);
        });
    }
}

new ProductsManager();