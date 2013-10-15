//
// https://aqidev.azure-mobile.net/api/messpunkt?stationId=03:2101
//
exports.get = function(request, response) {
    var stationId = request.query.stationId;
    
    var sql = "SELECT * FROM messpunkt WHERE stationId = ?";
    
    request.service.mssql.query(sql, [stationId],{
            success: function(results) {
                if (results.length == 1) {
                    response.send(statusCodes.OK, results[0]);
                }
                else {
                    response.send(statusCodes.NOT_FOUND, "Not found");
                }
            },
            error: function(error) {
                console.error(error);
                response.send(statusCodes.INTERNAL_SERVER_ERROR, "Internal Server Error");
            }
        });
};