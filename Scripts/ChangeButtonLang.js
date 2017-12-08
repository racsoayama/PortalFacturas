lang = "";
function ChangeButtonLang(lang) {

    alert(lang);
    if (lang=='es') {
        lang = 0;
    }
    else if (lang=='en') {
        lang = 1;
    } 
    else{
        lang = 0;
    }
    var ButtonsListes = {
        btnSalir: 'Salir',
        MainPlaceHolder_btnBuscar: 'Buscar',
        MainPlaceHolder_btnMostrarTodos: 'Mostrar',
        MainPlaceHolder_btnImportar: 'Importar',
        btnAceptarImportar: 'Aceptar',
        btnCancelarImportar: 'Cancelar',
        btnCambioPassword: 'Cambiar Password',
        btnAceptar: 'Aceptar',
        btnRecuperaCambio: 'Recuperar Password',
        btnCrearCta: 'Crear Cuenta',
        btnVerManual: 'Ver Manual',
        btnAceptarCambio: 'Aceptar',
        btnCancelarCambio: 'Cancelar',
        btnRegistrar: 'Registrar',
        btnCancNuevaCta: 'Cancelar',
        MainPlaceHolder_btnGenear: 'Generar',
        MainPlaceHolder_btnAceptarImportar: 'Aceptar',
        MainPlaceHolder_btnGenerar: 'Generar',
        MainPlaceHolder_ImageButton1: 'Generar',
        MainPlaceHolder_btnAceptarLog: 'Aceptar',
        MainPlaceHolder_btnAceptarFin: 'Aceptar',
        MainPlaceHolder_btnGuardar: 'Guardar',
        MainPlaceHolder_btnCancelar: 'Cancelar'
    };
    var ButtonsListen = {
        btnSalir: 'Exit',
        MainPlaceHolder_btnBuscar: 'Search',
        MainPlaceHolder_btnMostrarTodos: 'Show',
        MainPlaceHolder_btnImportar: 'Import',
        btnAceptarImportar: 'Acept',
        btnCancelarImportar: 'Cancel',
        btnCambioPassword: 'Change Password',
        btnAceptar: 'Acept',
        btnRecuperaCambio: 'Retriver Password',
        btnCrearCta: 'Create Account',
        btnVerManual: 'Show Manual',
        btnAceptarCambio: 'Acept',
        btnCancelarCambio: 'Cancel',
        btnRegistrar: 'Register',
        btnCancNuevaCta: 'Cancel',
        MainPlaceHolder_btnGenear: 'Generate',
        MainPlaceHolder_btnAceptarImportar: 'Acept',
        MainPlaceHolder_btnGenerar: 'Generate',
        MainPlaceHolder_ImageButton1: 'Generate',
        MainPlaceHolder_btnAceptarLog: 'Acept',
        MainPlaceHolder_btnAceptarFin: 'Acept',
        MainPlaceHolder_btnGuardar: 'Save',
        MainPlaceHolder_btnCancelar: 'Cancel'
    };

    var ButtonsList = new Array(ButtonsListes, ButtonsListen);

    console.log(ButtonsList[lang].length);
    for (var k in ButtonsList[lang]) {

        if (ButtonsList[lang].hasOwnProperty(k)) {
                var field = document.getElementById(k);

                if (field != null) {
                    field.value = ButtonsList[lang][k];
                }

            }
        }
    
}