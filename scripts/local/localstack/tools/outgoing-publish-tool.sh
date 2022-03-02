#!/bin/bash

awslocal sns publish \
  --topic-arn "arn:aws:sns:us-east-1:000000000000:tickets-topic" \
  --message "Some message"

exit $?