function hasNumbers(cadena) {
    return /\d/.test(cadena);
}

function hasLetters(cadena) {
    if (cadena.match(/[a-zA-Z]/g)) {
        return true;
    } else {
        return false;
    }
}
function changeToUpperCase(controlName) {
    document.getElementById(controlName).value = document.getElementById(controlName).value.toUpperCase();
    modificado = true;
}

function validateAlpha(controlName) {
    var textInput = document.getElementById(controlName).value.toUpperCase();
    textInput = textInput.replace(/[^A-Z0-9]/g, "");
    document.getElementById(controlName).value = textInput;
}

function validateNumeros(controlName) {
    var textInput = document.getElementById(controlName).value.toUpperCase();
    textInput = textInput.replace(/[^0-9]/g, "");
    document.getElementById(controlName).value = textInput;
}

function ValidaDecimales(evt, control, pos) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
        return false;
    else {
        var len = document.getElementById(control).value.length;
        var index = -1;
        index = document.getElementById(control).value.indexOf('.');

        if (index > -1 && charCode == 46) {
            return false;
        }
        if (index > -1) {
            var CharAfterdot = (len) - (index + 1);
            if (CharAfterdot >= pos) {
                return false;
            }
        }
    }
    return true;
}


function validaLongitud(controlName, longitud) {
    var textInput = document.getElementById(controlName).value;
    textInput = textInput.substring(0, longitud - 1);
    document.getElementById(controlName).value = textInput;
}

//function CancelReturnKey() {
//    if (window.event.keyCode == 13) {
//        return false;
//    }
//}

function disableEnterKey(e) {
    var key;
    if (window.event)
        key = window.event.keyCode; //IE
    else
        key = e.which; //firefox      

    return (key != 13);
}


function lastIndexOfBackSlash(cadena) {
    var pos = -1;
    for (var i = cadena.length - 1; i >= 0; i--) {
        if (cadena.substring(i, i + 1) == '\\') {
            pos = i;
            break;
        }
    }
    return pos;
}

function checkTextAreaMaxLength(textBox, e, length) {

    var mLen = textBox["MaxLength"];
    if (null == mLen)
        mLen = length;

    var maxLength = parseInt(mLen);
    if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            if (window.event)//IE
                e.returnValue = false;
            else//Firefox
                e.preventDefault();
        }
    }
}
function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}

function OpenPopupCenter(pageURL, title, w, h, scroll) {
    var left = (screen.width - w) / 2;
    var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
    var targetWin = window.open(pageURL, title, 'toolbar=no,location=no,directories=no,status=no,menubar=no,resizable=no,copyhistory=no,scrollbars=' + scroll + ' width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}

function conversor_monedas($moneda_origen, $moneda_destino, $cantidad) {
    $get = file_get_contents("https://www.google.com/finance/converter?a=$cantidad&from=$moneda_origen&to=$moneda_destino");
    $get = explode("<span class=bld>", $get);
    $get = explode("</span>", $get[1]);
    return preg_replace("/[^0-9\.]/", null, $get[0]);
}

