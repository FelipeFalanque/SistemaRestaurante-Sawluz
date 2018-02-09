$.GetParamFromUrl = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }

$(document).ready(function () {

    PreencheSelectRestaurante();

    if ($.GetParamFromUrl('idPrato')) {
        var idPrato = $.GetParamFromUrl('idPrato');
        window.history.pushState("", "", "/");
        BuscarOPratoPorId(idPrato);
    }
});

function Salvar() {
    //Como o form ja se encarregou não vou verificar o nome do Restaurante para salvar
    // Desativa o comportamento padrao do form
    event.preventDefault();

    var id_pra = $('#id_pra').val();
    var nome = $('#nome').val();
    var valor = Number($('#valor').val().toString());
    var id_res = $('#restaurantes').val();

    var pratoview = { ID: id_pra, Nome: nome, Valor: valor.toString().replace(/\./g, ','), RestauranteID: id_res};

    EviarPrato(pratoview);
}

function EviarPrato(prato) {
    $.ajax({
        url: "/Prato/SalvarPrato",
        type: "POST",
        dataType: "json",
        data: prato,
        success: function (resp) {
            if (resp.Status != 2) {
                console.log("Erro");
                return;
            }
            else {
                // Devolve o user para pagina de pratos
                window.location.href = "/Prato";
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

function BuscarOPratoPorId(idPrato) {
    $.ajax({
        url: "/Prato/GetPrato",
        type: "GET",
        dataType: "json",
        data: { idPrato: idPrato },
        success: function (resp) {
            if (resp.Status != 2) {
                console.log("Erro");
                return;
            }
            else {
                var prato = resp.Result;
                $('#id_pra').val(prato.ID);
                $('#nome').val(prato.Nome);
                $('#valor').val(prato.Valor.replace(",", "."));
                $('#restaurantes').val(prato.RestauranteID);
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

function PreencheSelectRestaurante() {
    $.ajax({
        url: "/Restaurante/GetRestaurantes",
        type: "GET",
        dataType: "json",
        success: function (resp) {
            if (resp.Status != 2) {
                console.log("Erro");
                return;
            }
            else {
                $.each(resp.Result, function (i, item) {
                    $('#restaurantes').append($('<option>', { value: item.ID, text: item.Nome }));
                });
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}