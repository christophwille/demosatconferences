var req = require('request');
var ozoneDataUrl = 'http://luft.umweltbundesamt.at/pub/ozonbericht/aktuell.json';

var messpunkteTabelle = tables.getTable('Messpunkt');
var channelTable = tables.getTable('StationPush');

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

    storeAllItems(messpunkte);
}

//
// http://blogs.msdn.com/b/carlosfigueira/archive/2013/01/02/inserting-multiple-items-at-once-in-azure-mobile-services.aspx
//
function storeAllItems(messpunkte) {
    var index = 0;
    var sqlDeleteStmt = "DELETE FROM messpunkt WHERE stationId = ?";

    var insertNext = function () {
        if (index < messpunkte.length) {
            var item = messpunkte[index];

            mssql.query(sqlDeleteStmt, [item.stationId], {
                success: function(results) {
                    messpunkteTabelle.insert(item, {
                        success: function () {
                            index++;
                            sendNotifications(item);
                            insertNext();
                        }
                    });
                },
                error: function(error) {
                    messpunkteTabelle.insert(item, {
                        success: function () {
                            index++;
                            sendNotifications(item);
                            insertNext();
                        }
                    });
                }
            });
        }
    };

    insertNext();
}

function sendNotifications(item) {
    channelTable
      .where({ stationId: item.stationId })
      .read({
        success: function(channels) {
            if (channels.length > 0) {
                console.log("Found " + channels.length + " channels  for station " + item.stationId );
            }
            channels.forEach(function(channel) {
                push.mpns.sendFlipTile(channel.uri, {
                    title: item.ozon1h
                }, {
                    success: function(pushResponse) {
                        console.log("Sent push:", pushResponse);
                    }
                });
            });
        }
    });
}

/* Format of the response JSON array:
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