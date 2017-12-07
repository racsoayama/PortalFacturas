
function ChangeButtonLang() {
    var ButtonsList = {
        btnSalir: 'Salir',
        MainPlaceHolder_btnBuscar: 'Buscar',
        MainPlaceHolder_btnMostrarTodos: 'Mostrar',
        MainPlaceHolder_btnImportar: 'Importar',
        btnAceptarImportar: 'Aceptar',
        btnCancelarImportar: 'Cancelar',
        btnCambioPassword: 'Cambiar Password',
        btnAceptar: 'Aceptar',
        btnRecuperaCambio: 'Recuperar',
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