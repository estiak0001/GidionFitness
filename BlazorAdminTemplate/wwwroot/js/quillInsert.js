// Quill Editor Helper Functions for inserting placeholders at cursor position

window.quillEditorHelpers = {
    /**
     * Insert text at cursor using direct DOM manipulation
     */
    insertTextAtCursor: function (editorId, text) {
        console.log('insertTextAtCursor called with:', editorId, text);
        
        try {
            // Find the contenteditable element
            const editorElement = document.querySelector('.ql-editor');
            
            if (!editorElement) {
                console.error('Editor element (.ql-editor) not found');
                return false;
            }
            
            console.log('Editor element found');
            
            // Focus the editor
            editorElement.focus();
            
            // Get the current selection
            const selection = window.getSelection();
            if (!selection) {
                console.error('Could not get window selection');
                return false;
            }
            
            console.log('Selection range count:', selection.rangeCount);
            
            // If there's a selection, insert at that position
            if (selection.rangeCount > 0) {
                const range = selection.getRangeAt(0);
                
                // Delete any selected content
                range.deleteContents();
                
                // Create a bold element with our placeholder text
                const strongElement = document.createElement('strong');
                strongElement.textContent = text;
                
                // Insert the bold element at the cursor position
                range.insertNode(strongElement);
                
                // Move the cursor after the inserted text
                range.setStartAfter(strongElement);
                range.setEndAfter(strongElement);
                selection.removeAllRanges();
                selection.addRange(range);
                
                console.log('✓ Bold text inserted at cursor position using DOM manipulation');
                return true;
            } else {
                // No selection, append to the end
                const strongElement = document.createElement('strong');
                strongElement.textContent = text;
                editorElement.appendChild(strongElement);
                
                console.log('✓ Bold text appended to end of editor');
                return true;
            }
        } catch (error) {
            console.error('✗ Error inserting text:', error);
            console.error('Error stack:', error.stack);
            return false;
        }
    },

    /**
     * Focus the editor
     */
    focusEditor: function (editorId) {
        const quill = this.getQuillInstance(editorId);
        if (quill) {
            quill.focus();
            return true;
        }
        return false;
    },

    /**
     * Get the current cursor position
     */
    getCursorPosition: function (editorId) {
        const quill = this.getQuillInstance(editorId);
        if (!quill) {
            return -1;
        }

        const selection = quill.getSelection();
        return selection ? selection.index : quill.getLength();
    }
};
