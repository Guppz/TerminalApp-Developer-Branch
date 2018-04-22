function prueba() {


        this.excelToJson = function(e){
        var files = e.target.files;
        var i, f;
        for (i = 0, f = files[i]; i !== files.length; ++i) {
            var reader = new FileReader();
            var name = f.name;
            reader.onload = function (e) {
                var data = e.target.result;
                var result;
                var workbook = XLSX.read(data, { type: 'binary' });
                var sheet_name_list = workbook.SheetNames;
                sheet_name_list.forEach(function (y) {
                    var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    if (roa.length > 0) {
                        result = roa;
                    }
                });
                alert(result[0].Nombre);
            };
            reader.readAsArrayBuffer(f);
        }
    }
}

$(document).ready(function () {
        var pruebas  = new prueba();
        $('#files').attr("name", "files");
        var file = $('#files').val();
        $("#files").change(function (File) {
            pruebas.excelToJson(File);
        });

});





    

