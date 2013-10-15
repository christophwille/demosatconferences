//
//  Based on http://code.msdn.microsoft.com/Capture-Store-and-Email-34005240
//
var SendGrid = require('sendgrid').SendGrid; 

var config = require('../shared/config.js');
var settings = config.getSettings();

function insert(item, user, request) {     
     request.execute({ 
          success: function() { 
                // After the record has been inserted, send the response immediately to the client 
                request.respond(); 
                // Send the email in the background 
                sendEmail(item); 
          } 
     }); 
 
     function sendEmail(item) { 
          var sendgrid = new SendGrid(settings.email.sgUsername, settings.email.sgPassword); 
 
          sendgrid.send({ 
                to: 'christoph.wille@outlook.com', 
                from: 'christoph.wille@outlook.com', 
                subject: 'Feedback Submitted', 
                text: 'A new feedback has been submitted: ' + item.text 
          }, function(success, message) { 
                // If the email failed to send, log it as an error so we can investigate 
                if (!success) { 
                     console.error(message); 
                } 
          }); 
     } 
} 