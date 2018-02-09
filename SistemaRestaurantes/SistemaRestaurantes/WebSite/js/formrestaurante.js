$.GetParamFromUrl = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }

$(document).ready(function () {
    if ($.GetParamFromUrl('idRestaurante')) {
        var idRestaurante = $.GetParamFromUrl('idRestaurante');
        window.history.pushState("", "", "/");
        BuscarORestaurantePorId(idRestaurante);
    }
});

function Salvar() {
    //Como o form ja se encarregou, não vou verificar o nome do Restaurante para salvar
    // Desativa o comportamento padrao do form
    event.preventDefault();

    var id_res = $('#id_res').val();
    var nome = $('#nome').val();

    var restaurante = { ID: id_res, Nome: nome };

    EviarRestaurante(restaurante);
}

function EviarRestaurante(restaurante) {
    $.ajax({
        url: "/Restaurante/SalvarRestaurante",
        type: "POST",
        dataType: "json",
        data: restaurante,
        success: function (resp) {
            if (resp.Status != 2) {
                console.log("Erro");
                return;
            }
            else {
                // Devolve o user para pagina de restaurantes
                window.location.href = "/Restaurante";
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

function BuscarORestaurantePorId(idRestaurante) {
    $.ajax({
        url: "/Restaurante/GetRestaurante",
        type: "GET",
        dataType: "json",
        data: { idRestaurante : idRestaurante },
        success: function (resp) {
            if (resp.Status != 2) {
                console.log("Erro");
                return;
            }
            else {
                var restaurante = resp.Result;
                $('#id_res').val(restaurante.ID);
                $('#nome').val(restaurante.Nome);
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}