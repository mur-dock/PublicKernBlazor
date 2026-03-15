// kern-dialog.js
// Stabile JS-Hilfsfunktionen für das native HTML-<dialog>-Element.
// Blazor ruft diese Funktionen via InvokeVoidAsync auf, da das native
// HTMLDialogElement-Prototype-Muster je nach Runtime-Version instabil ist.
window.kernDialog = {
    /**
     * Öffnet einen modalen Dialog.
     * @param {HTMLDialogElement} el – Referenz auf das <dialog>-Element (von Blazor via @ref übergeben).
     */
    showModal: function (el) {
        if (el && typeof el.showModal === 'function') {
            el.showModal();
        }
    },

    /**
     * Schließt einen Dialog.
     * @param {HTMLDialogElement} el – Referenz auf das <dialog>-Element.
     */
    close: function (el) {
        if (el && typeof el.close === 'function') {
            el.close();
        }
    }
};

