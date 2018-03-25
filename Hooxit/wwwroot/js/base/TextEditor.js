class TextEditor {
    constructor(textEditorId, settings) {
        this.textEditor = new Quill(textEditorId, settings);
    }

    setText(text) {
        this.textEditor.setContents(JSON.parse(text));
    }

    deleteText() {
        this.textEditor.deleteText(0, this.textEditor.getLength() - 1);
    }

    getText() {
        return this.textEditor.getText();
    }
}