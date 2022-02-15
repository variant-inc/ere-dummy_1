#!/bin/bash

echo "RUNNING localstack config"
set -e

apk add jq

export AWS_ACCESS_KEY_ID=12345
export AWS_SECRET_ACCESS_KEY=12345
export AWS_DEFAULT_REGION=us-east-1

entityQueue=entity-queue
entityTopic=entity-topic
ticketTopic=tickets-topic
queueTimeOut=5  #5 seconds

# Create Incoming Entity Topic
awslocal sns create-topic --name $entityTopic

# create Incoming Entity queue
awslocal sqs create-queue --queue-name $entityQueue
awslocal sqs set-queue-attributes \
          --queue-url http://localstack:4566/queue/$entityQueue \
          --attributes "{ \"VisibilityTimeout\": \"$queueTimeOut\" }"

# Subscribe entity queue to entity topic
awslocal sns subscribe \
          --topic-arn arn:aws:sns:us-east-1:000000000000:$entityTopic \
          --protocol sqs \
          --notification-endpoint arn:aws:sqs:us-east-1:000000000000:$entityQueue \
          --attributes "RawMessageDelivery=true"

# Create Outgoing Ticket Topic
awslocal sns create-topic --name $ticketTopic

# This is blocked by OPT-2011
# Subscribe the app to the Ticket Topic
#awslocal sns subscribe \
#          --topic-arn arn:aws:sns:us-east-1:000000000000:$ticketTopic \
#          --protocol http \
#          --notification-endpoint http://localhost:4566/ticket-topic \
#          --attributes "RawMessageDelivery=true"

echo "FINISHED localstack config"