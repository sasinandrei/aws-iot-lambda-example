# Send message to IoT core topic from lambda function
## Structure
AWSExample.SendMessage - lambda function
AWSExample.SendMessage.Test - test for lambda function (run lambda function to send message)
chat - simple js client to listen topic
## Setup
* Need to setup access key, secret key, service url and topic in environment variables for lambda function. (AccessKey, SecretKey, ServiceURL, Topic)
* Need to setup access key, secret key, service url and topic in AWSExample.SendMessage.Test/SendMessage.cs to run test
* Need to setup access key, secret key, service url and topic in chat/main.js to run js client
##
To run js client:
* Run `npm install` in chat directory
* Run  `./node_modules/.bin/webpack-dev-server --colors`