$(document).ready(function () {
    table = $('#tablePratos');

    $.ajax({
        url: "/Prato/GetPratos",
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
    return '<button type="button" class="btn btn-primary btn-xs" title="Editar" onclick="UpdatePrato(' + value + ')"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button> <button type="button" class="btn btn-danger btn-xs" title="Excluir" onclick="RemovePrato(' + value + ')"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>';
}

function FormataPrecos(value) {
    var preco = Number(value.replace(",", ".").toString());
    var preco_str = preco.toFixed(2);
    return preco_str.replace(".", ",");
}

function AddPrato() {
    bootbox.confirm("Deseja Adicionar um Novo Prato ?",
        function (result) { if (result) { window.location.href = "/Prato/Formulario"; } });
}

function UpdatePrato(idPrato) {
    bootbox.confirm("Deseja Editar o Prato ?",
        function (result) { if (result) { window.location.href = "/Prato/Formulario?idPrato=" + idPrato; } });
}

function RemovePrato(idPrato) {
    bootbox.confirm("Deseja Remover o Prato ?",
        function (result) { if (result) { EviarPratoParaRemover(idPrato) } });
}

function EviarPratoParaRemover(idPrato) {
    $.ajax({
        url: "/Prato/RemovePrato",
        type: "POST",
        dataType: "json",
        data: { idPrato: idPrato },
        success: function (resp) {
            if (resp.Status != 2) {
                console.log("Erro");
                return;
            }
            else {
                table.bootstrapTable('removeByUniqueId', idPrato);
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

//Sucesso
