var req = require('request');
var csv = require('csv');

var ozoneDataUrl = 'http://luft.umweltbundesamt.at/pub/ozonbericht/aktuell.json';
var pm10DataUrl = 'http://open-data.noe.gv.at/BD4/FeinstaubPM10.csv';

// scheduledRetrievalOfOzoneData();
getPm10Data();

function scheduledRetrievalOfOzoneData() {
    req( {uri: ozoneDataUrl }, function (err, response, body) {
        if (!err && response.statusCode == 200) {
            eval(body);
        }
    });
}

function jsonpResponse(messpunkte) {
    for (var i = 0; i < messpunkte.length; i++) {
        var element = messpunkte[i];

        element.stationId = element.id;
        delete element.id;
    }
}

function getPm10Data() {
    // Known issue: special characters (a 'speciality' of Austrian OGD data) - use stream-buffers to fix, omitted for simplicity here
    req( {uri: pm10DataUrl }, function (err, resp, body) {
        if (!err && resp.statusCode == 200) {
            csv()
                .from.string(body, {delimiter: ';', columns: true} )
                .to.array( function(data){
                    console.log(data)
                } );
        }
    });
}

/* Format of the jsonpResponse() JSON array:
{
    "id": "03:2101",
    "name": "Wiesmath",
    "timestamp_utc": "Thu, 10 Oct 2013 10:00:00 GMT",
    "lat": 47.6083,
    "lon": 16.2931,
    "height": 738,
    "state": "NÖ",
    "ozon1h": 85,
    "ozon1hTimestamp_utc": "Thu, 10 Oct 2013 10:00:00 GMT",
    "ozon8h": 71,
    "ozon1hMax": 86,
    "ozon1hMaxTimestamp_utc": "Thu, 10 Oct 2013 09:00:00 GMT"
}, */