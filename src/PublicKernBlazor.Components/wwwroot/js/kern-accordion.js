// kern-accordion.js
// Stabile JS-Hilfsfunktionen für das native HTML-<details>-Element.
// Blazor darf das open-Attribut nicht deklarativ setzen, weil jede DOM-Mutation
// ein natives ontoggle-Ereignis auslöst – das führt mit Blazors Re-Render-Zyklus
// zu einer Endlosschleife. Stattdessen setzt Blazor das Attribut einmalig über
// diese Funktionen direkt im DOM, ohne einen weiteren Render-Zyklus auszulösen.
window.kernAccordion = {
    /**
     * Öffnet ein <details>-Element.
     * @param {HTMLDetailsElement} el – Referenz auf das <details>-Element (via @ref übergeben).
     */
    open: function (el) {
        if (el) el.open = true;
    },

    /**
     * Schließt ein <details>-Element.
     * @param {HTMLDetailsElement} el – Referenz auf das <details>-Element.
     */
    close: function (el) {
        if (el) el.open = false;
    }
};

