#!/bin/bash

echo "RUNNING localstack config"
set -e

apk add jq

export AWS_ACCESS_KEY_ID=12345
export AWS_SECRET_ACCESS_KEY=12345
export AWS_DEFAULT_REGION=us-east-1

entityQueue=entity-queue
entityTopic=entity-topic
#ticketQueue=tickets-queue
#ticketTopic=tickets
queueTimeOut=20  #20 seconds

# Create Outgoing Ticket Topic
#awslocal sns create-topic --name $ticketTopic

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

# create Outgoing Ticket queue
#awslocal sqs create-queue --queue-name $ticketQueue
#
#awslocal sqs set-queue-attributes \
#          --queue-url http://localstack:4566/queue/$ticketQueue \
#          --attributes "{ \"VisibilityTimeout\": \"$queueTimeOut\" }"

# Subscribe ticket queue to ticket topic
#awslocal sns subscribe \
#          --topic-arn arn:aws:sns:us-east-1:000000000000:$ticketTopic \
#          --protocol sqs \
#          --notification-endpoint arn:aws:sqs:us-east-1:000000000000:$ticketQueue \
#          --attributes "RawMessageDelivery=true"

echo "FINISHED localstack config"