$(document).ready(function () {
    table = $('#tableRestaurantes');

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
                $.each(resp.Result, function (i, dados_corrente) {
                    table.bootstrapTable('insertRow', {
                        index: 0,
                        row: dados_corrente
                    });
                });
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
});

function Acoes(value) {
    return '<button type="button" class="btn btn-primary btn-xs" title="Editar" onclick="UpdateRestaurante(' + value + ')"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button> <button type="button" class="btn btn-danger btn-xs" title="Excluir" onclick="RemoveRestaurante('+ value +')"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>';
}

function AddRestaurante() {
    bootbox.confirm("Deseja Adicionar um Novo Restaurante ?",
        function (result) { if (result) { window.location.href = "/Restaurante/Formulario"; } });
}

function UpdateRestaurante(idRestaurante) {
    bootbox.confirm("Deseja Editar o Restaurante ?",
        function (result) { if (result) { window.location.href = "/Restaurante/Formulario?idRestaurante=" + idRestaurante; } });}

function RemoveRestaurante(idRestaurante) {
    bootbox.confirm("Deseja Remover o Restaurante ?",
        function (result) { if (result) { EviarRestauranteParaRemover(idRestaurante) } });
}

function EviarRestauranteParaRemover(idRestaurante) {
    $.ajax({
        url: "/Restaurante/RemoveRestaurante",
        type: "POST",
        dataType: "json",
        data: { idRestaurante: idRestaurante },
        success: function (resp) {
            if (resp.Status != 2) {
                console.log("Erro");
                return;
            }
            else {
                table.bootstrapTable('removeByUniqueId', idRestaurante);
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}