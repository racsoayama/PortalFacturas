
function ChangeButtonLang() {
    var ButtonsList = {
        btnSalir: 'Salir',
        MainPlaceHolder_btnBuscar: 'btnBuscar',
        MainPlaceHolder_btnMostrarTodos: 'btnMostrarTodos',
        MainPlaceHolder_btnImportar: 'btnImportar',
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
        btnCancNuevaCta: 'btnCancNuevaCta',
        MainPlaceHolder_btnGenear: 'MainPlaceHolder_btnGenear',
        MainPlaceHolder_btnAceptarImportar: 'MainPlaceHolder_btnAceptarImportar',
        MainPlaceHolder_btnGenerar: 'MainPlaceHolder_btnGenerar',
        MainPlaceHolder_ImageButton1: 'MainPlaceHolder_ImageButton1',
        MainPlaceHolder_btnAceptarLog: 'MainPlaceHolder_btnAceptarLog',
        MainPlaceHolder_btnAceptarFin:'MainPlaceHolder_btnAceptarFin'

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