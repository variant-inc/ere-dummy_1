#!/bin/bash

JSON=$(cat test-queue-message.json)

awslocal sns publish \
  --topic-arn "arn:aws:sns:us-east-1:000000000000:entity-topic" \
  --message "$JSON"

exit $?