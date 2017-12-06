
function ChangeButtonLang() {
    var ButtonsList = {
        btnSalir: 'Salir',
        btnBuscar: 'btnBuscar',
        btnMostrarTodos: 'btnMostrarTodos',
        btnImportar: 'btnImportar',
        btnAceptarImportar: 'btnAceptarImportar',
        btnCancelarImportar: 'btnCancelarImportar',
        btnCambioPassword: 'btnCambioPassword',
        btnAceptar: 'btnAceptar',
        btnRecuperaCambio: 'btnRecuperaCambio',
        btnCrearCta: 'btnCrearCta',
        btnVerManual: 'btnVerManual',
        btnAceptarCambio: 'btnAceptarCambio',
        btnCancelarCambio: 'btnCancelarCambio',
        btnRegistrar: 'btnRegistrar',
        btnCancNuevaCta: 'btnCancNuevaCta'
    };

    console.log(ButtonsList.length);
    for (var k in ButtonsList) {

        if (ButtonsList.hasOwnProperty(k)) {
            var field = document.getElementById(k);

            if (field != null) {
                field.value = ButtonsList[k];
            }
          
        }
    }
}