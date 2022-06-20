var btn = document.querySelector("#btn-search");
var form = document.querySelectorAll("#formDomain");
btn.addEventListener("click", function () {

    $.get('/api/domain', form.serialize()).done(
        function (data) {
            if (data.erro != undefined) {
                alert("Erro na execução");
            } else {
                if (data.length > 0) {
                    //BindGridLotesEmail(data);
                    $("#resultado").css("visibility", "visible");
                }
                else {
                    SetModalInfo('Operação realizada', "A pesquisa não retornou dados."); ShowModal();
                }
            }
        })
        .always(function () {
        })
        .fail(function () {

        });
});


