function insert(item, user, request) {
        var channelTable = tables.getTable('StationPush');

        channelTable
            .where({ uri: item.uri })
            .read({ success: insertChannelIfNotFound });

        function insertChannelIfNotFound(existingChannels) {
            if (existingChannels.length > 0) {
                // TODO: Here, we should update an existing registration!
                request.respond(200, existingChannels[0]);
            } else {
                item.createdAt = new Date();
                request.execute();
            }
        }
    }