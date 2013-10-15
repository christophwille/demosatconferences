var req = require('request');

// Still needed as of 10/12/2013 for custom node modules that were uploaded via Git
// http://social.msdn.microsoft.com/Forums/windowsazure/en-US/d5758bc1-8ae7-4f98-8ff7-22be96569858/nodejs-modules
var _r = require;
var require = function(path) {
try { return _r(path);	}
catch (e) { return _r('../../app_data/config/scripts/table/' + path); }
}

var csv = require('../shared/node_modules/csv');

var pm10DataUrl = 'http://open-data.noe.gv.at/BD4/FeinstaubPM10.csv';

//
// https://aqidev.azure-mobile.net/api/pm10
//
exports.get = function(request, response) {
    req({uri: pm10DataUrl }, function (err, resp, body) {
        if (!err && resp.statusCode == 200) {
            csv()
                .from.string(body, {delimiter: ';', columns: true} )
                .to.array( function(data){
                    response.send(statusCodes.OK,data);
                } );
        }
    });
};