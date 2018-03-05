class TextEditor {
    constructor(textEditorId) {
        this.Id = textEditorId;
        this.textEditor = new Quill(this.Id, CreatePositionEditorSettings.getSettings);
    }

    get editor() {
        return this.textEditor;
    }
}