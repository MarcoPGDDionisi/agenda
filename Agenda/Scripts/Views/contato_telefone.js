/*****************************************************************
    Carrega Inputs --> Variavel
   ******************************************************************/
telefone_readInputs = function () {
    var InputValues = {
        IdTelefone: $("#IdTelefone").val(),
        IdContato: $("#IdContato").val(),
        TipoTelefone: $("#TipoTelefone").val(),
        UsoTelefone: $("#UsoTelefone").val(),
        NumTelefone: $("#NumTelefone").val()
    }

    return InputValues;
}


/*****************************************************************
 Evento ==>  $("#btn_telefone_incluir").click()
******************************************************************/
$("#btn_telefone_incluir").on("click", function () {
    $("#IdTelefone").val('0');
    $("#TipoTelefone").val('0');
    $("#UsoTelefone").val('0');
    $("#NumTelefone").val('');

})

/*****************************************************************
 Carrega  Variavel --> Inputs
******************************************************************/
telefone_readWrite = function (obj) {

    $("#IdTelefone").val(obj.IdTelefone);
    $("#TipoTelefone").val(obj.TipoTelefone);
    $("#UsoTelefone").val(obj.UsoTelefone);
    $("#NumTelefone").val(obj.NumTelefone);
}

/*****************************************************************
 Evento ==>  $("#btn_telefone_save").click()
******************************************************************/

$("#btn_telefone_save").on("click", function () {
    user_contatos = telefone_readInputs();
    if (telefone_validade()) {
        obj = comum.post('/Home/Telefone_InsertUpdate/', { 'objList': user_contatos });
        telefone_readWrite(obj.objList);
        Telefone_populateDataTable($('#IdContato').val())
        alert("Inclusão/Alteração efetuada com Sucesso !!!")
    }

})

/*****************************************************************
 Evento ==>  $("#btn_telefone_delete").click()
******************************************************************/
$("#btn_telefone_delete").on("click", function () {
    if ($("#IdContato").val() === '0') {
        alert('Contato Inexistente !!!')
    }
    else if ($("#IdTelefone").val() === '0') {
        alert('Telefone Inexistente !!!')
    }
    else {
        if (confirm('Confirma exclusão desse Telefone ?')) {
            comum.post('/Home/Telefone_Excluir/', { 'IdTelefone': $("#IdTelefone").val() });
            alert('Telelfone Excluído com Sucesso !!!')
            Telefone_populateDataTable($('#IdContato').val())
            debugger
            $("#btn_telefone_incluir").click();
        }
        else {

            alert('Exclusão Cancelada pelo Usuário');
        }
    }
})

/*****************************************************************
 Contato --> Valida os Dados Inseridos
******************************************************************/
telefone_validade = function () {

    var msg = '';

    if ($("#IdContato").val() === '0') {
        msg += '\n\n Efetivar o Cadastro do Contato';
    }

    if ($("#TipoTelefone").val() === '0' || $("#TipoTelefone").val() === null) {
        msg += '\n\n Informar o Tipo Telefone (Celular, Fixo... ;';

    }
    if ($("#UsoTelefone").val() === '0' || $("#TipoTelefone").val() === null) {
        msg += '\n\n Informar o Uso do Telefone (Casa, Empresa...;';

    }

    if ($("#NumTelefone").val().replace(new RegExp(' ', 'g'), '') === '') {
        msg += '\n\nNumero do Telefone - Preenchimento Obrigatório;';
    }
    if (msg !== '') {
        alert('Favor Corrigir as seguintes informações:' + msg)
        return false
    }
    return true;

}

Telefone_populateDataTable = function (IdContato) {

    objList = comum.post('/Home/Telefone_SelectList/', { 'IdContato': IdContato });

    $('#TbTelefone').DataTable({
        "data": objList.objList,
        "dom": 'Bfrtip',
        "async": "false",
        "bDestroy": true,
        "paging": false,
        "extend": 'excelHtml5',
        "pageLength": 50,
        "scrollY": "200px",
        "scrollCollapse": true,
        "height": "600px",
        "decimal": ",",
        "thousands": ".",
        "bFilter": false,
        "emptyTable": "No data available in table",
        "language": {
            "info": "",
            "lengthMenu": "",
            "infoFiltered": "(Filtrado de _MAX_ total registros)",
            "processing": "<div><img src='~/images/waiting.gif' /></div>",
            "search": "Busca:",
            "loadingRecords": "Aguarde - Carregando Grid ...",
            "zeroRecords": "Não foram licalizados Registros",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            }
        },
        "select": {
            style: 'single',
            info: false
        },
        columns: [
            { title: "Tipo", data: "TipoTelefone" },
            { title: "Uso Telefone", data: "UsoTelefone" },
            { title: "Número", data: "NumTelefone" },
        ]
    });

}
